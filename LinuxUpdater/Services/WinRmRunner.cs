using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using LinuxUpdater.Models;

namespace LinuxUpdater.Services
{
    public class WinRmRunner
    {
        public Task<string> RunCommandAsync(Machine machine)
        {
            return Task.Run(() => RunCommand(machine));
        }

        public string RunCommand(Machine machine)
        {
            var output = new StringBuilder();

            try
            {
                var prep = PrepareClient(machine.IpAddress);
                if (!string.IsNullOrWhiteSpace(prep))
                {
                    output.AppendLine(prep);
                }

                Exception lastError = null;
                foreach (var username in BuildUsernameCandidates(machine))
                {
                    foreach (var auth in new[] { AuthenticationMechanism.Negotiate, AuthenticationMechanism.Basic })
                    {
                        try
                        {
                            var result = InvokeRemote(machine, username, auth);
                            if (output.Length > 0)
                            {
                                output.AppendLine();
                            }

                            output.AppendLine(result);
                            return output.ToString().TrimEnd();
                        }
                        catch (Exception ex)
                        {
                            lastError = ex;
                        }
                    }
                }

                output.AppendLine($"ERROR: {lastError?.Message}");
                if (lastError?.InnerException != null)
                {
                    output.AppendLine(lastError.InnerException.Message);
                }

                output.AppendLine();
                output.AppendLine(BuildTroubleshootingHelp(machine.IpAddress));
            }
            catch (Exception ex)
            {
                output.AppendLine($"ERROR: {ex.Message}");
                if (ex.InnerException != null)
                {
                    output.AppendLine(ex.InnerException.Message);
                }

                output.AppendLine();
                output.AppendLine(BuildTroubleshootingHelp(machine.IpAddress));
            }

            return output.ToString().TrimEnd();
        }

        private static string InvokeRemote(Machine machine, string username, AuthenticationMechanism auth)
        {
            var output = new StringBuilder();
            var credential = new PSCredential(username, ToSecureString(machine.Password));
            var uri = new Uri($"http://{machine.IpAddress}:5985/wsman");

            var connectionInfo = new WSManConnectionInfo(
                uri,
                "http://schemas.microsoft.com/powershell/Microsoft.PowerShell",
                credential)
            {
                // Default/Kerberos fails on workgroup hosts even when TrustedHosts is set.
                AuthenticationMechanism = auth,
                OperationTimeout = 30 * 60 * 1000,
                OpenTimeout = 60 * 1000
            };

            using (var runspace = RunspaceFactory.CreateRunspace(connectionInfo))
            {
                runspace.Open();

                using (var powerShell = PowerShell.Create())
                {
                    powerShell.Runspace = runspace;
                    powerShell.AddScript(machine.Command);

                    Collection<PSObject> results = powerShell.Invoke();

                    output.AppendLine($"Connected as '{username}' via {auth}.");

                    foreach (var item in results)
                    {
                        if (item != null)
                        {
                            output.AppendLine(item.ToString());
                        }
                    }

                    if (powerShell.Streams.Error.Count > 0)
                    {
                        output.AppendLine("--- ERRORS ---");
                        foreach (var error in powerShell.Streams.Error)
                        {
                            output.AppendLine(error.ToString());
                        }
                    }

                    if (powerShell.Streams.Warning.Count > 0)
                    {
                        output.AppendLine("--- WARNINGS ---");
                        foreach (var warning in powerShell.Streams.Warning)
                        {
                            output.AppendLine(warning.Message);
                        }
                    }

                    if (powerShell.HadErrors && results.Count == 0 && powerShell.Streams.Error.Count == 0)
                    {
                        throw new InvalidOperationException("WinRM command failed with no output.");
                    }

                    if (!powerShell.HadErrors && results.Count == 0)
                    {
                        output.AppendLine("Command completed with no output.");
                    }
                }

                runspace.Close();
            }

            return output.ToString().TrimEnd();
        }

        private static string[] BuildUsernameCandidates(Machine machine)
        {
            var user = (machine.Username ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(user))
            {
                return new[] { user };
            }

            if (user.Contains("\\") || user.Contains("@"))
            {
                return new[] { user };
            }

            return new[]
            {
                @".\" + user,
                machine.IpAddress + @"\" + user,
                user
            };
        }

        private static string PrepareClient(string host)
        {
            var messages = new StringBuilder();

            try
            {
                RunWinRm("quickconfig -q");
            }
            catch
            {
                // Already configured or needs elevation — continue.
            }

            var allowResult = RunWinRm("set winrm/config/client @{AllowUnencrypted=\"true\"}");
            messages.AppendLine(string.IsNullOrWhiteSpace(allowResult)
                ? "Client AllowUnencrypted=true"
                : "AllowUnencrypted: " + allowResult.Trim());

            using (var powerShell = PowerShell.Create())
            {
                powerShell.AddScript(@"
param($HostName)
$ErrorActionPreference = 'Stop'
try {
    $svc = Get-Service WinRM -ErrorAction SilentlyContinue
    if ($svc -and $svc.Status -ne 'Running') {
        Start-Service WinRM
    }

    $trusted = ''
    try { $trusted = (Get-Item WSMan:\localhost\Client\TrustedHosts).Value } catch { $trusted = '' }

    if ([string]::IsNullOrWhiteSpace($trusted)) {
        Set-Item WSMan:\localhost\Client\TrustedHosts -Value $HostName -Force
    }
    elseif ($trusted -ne '*') {
        $parts = @($trusted -split ',' | ForEach-Object { $_.Trim() } | Where-Object { $_ })
        if (-not ($parts -contains $HostName)) {
            Set-Item WSMan:\localhost\Client\TrustedHosts -Value $HostName -Concatenate -Force
        }
    }

    return (Get-Item WSMan:\localhost\Client\TrustedHosts).Value
}
catch {
    return 'ERROR: ' + $_.Exception.Message
}
");
                powerShell.AddParameter("HostName", host);
                var results = powerShell.Invoke();

                if (powerShell.Streams.Error.Count > 0)
                {
                    messages.AppendLine("WARN: " + string.Join(" | ", powerShell.Streams.Error.Select(e => e.ToString())));
                }
                else
                {
                    messages.AppendLine("TrustedHosts=" + (results.FirstOrDefault()?.ToString() ?? "(empty)"));
                }
            }

            return messages.ToString().TrimEnd();
        }

        private static string RunWinRm(string arguments)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "winrm.cmd",
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            {
                if (process == null)
                {
                    return "Could not start winrm.cmd";
                }

                var stdout = process.StandardOutput.ReadToEnd();
                var stderr = process.StandardError.ReadToEnd();
                process.WaitForExit(15000);

                if (process.ExitCode == 0)
                {
                    return stdout;
                }

                return (stdout + " " + stderr).Trim();
            }
        }

        private static string BuildTroubleshootingHelp(string host)
        {
            var trusted = GetTrustedHosts() ?? "(unknown)";

            return string.Join(Environment.NewLine, new[]
            {
                "WinRM workgroup checklist:",
                "- This PC TrustedHosts is currently: " + trusted,
                "- Run this app once as Administrator.",
                "- On THIS PC (Admin PowerShell):",
                "    Set-Item WSMan:\\localhost\\Client\\TrustedHosts -Value \"" + host + "\" -Concatenate -Force",
                "    winrm set winrm/config/client @{AllowUnencrypted=\"true\"}",
                "- On the TARGET Windows server (Admin PowerShell):",
                "    Enable-PSRemoting -Force",
                "    winrm set winrm/config/service @{AllowUnencrypted=\"true\"}",
                "    winrm set winrm/config/service/auth @{Basic=\"true\"}",
                "- Username tip: use .\\Administrator or SERVERNAME\\Administrator"
            });
        }

        private static string GetTrustedHosts()
        {
            try
            {
                using (var powerShell = PowerShell.Create())
                {
                    powerShell.AddScript("(Get-Item WSMan:\\localhost\\Client\\TrustedHosts).Value");
                    return powerShell.Invoke().FirstOrDefault()?.ToString() ?? "(empty)";
                }
            }
            catch
            {
                return "(unknown)";
            }
        }

        private static SecureString ToSecureString(string value)
        {
            var secure = new SecureString();
            if (!string.IsNullOrEmpty(value))
            {
                foreach (var c in value)
                {
                    secure.AppendChar(c);
                }
            }

            secure.MakeReadOnly();
            return secure;
        }
    }
}
