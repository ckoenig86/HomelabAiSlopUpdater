using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LinuxUpdater.Models;
using Renci.SshNet;

namespace LinuxUpdater.Services
{
    public class SshRunner
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
                using (var client = new SshClient(machine.IpAddress, machine.Username, machine.Password))
                {
                    client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(30);
                    client.Connect();

                    if (!client.IsConnected)
                    {
                        return $"ERROR: Could not connect to {machine.IpAddress}";
                    }

                    // SSH exec has no TTY — force noninteractive apt/debconf and avoid apt CLI warnings.
                    var remoteCommand = BuildNonInteractiveCommand(machine.Command);

                    using (var command = client.CreateCommand(remoteCommand))
                    {
                        command.CommandTimeout = TimeSpan.FromMinutes(30);
                        var result = command.Execute();

                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            output.AppendLine(result.TrimEnd());
                        }

                        if (!string.IsNullOrWhiteSpace(command.Error))
                        {
                            output.AppendLine("--- STDERR ---");
                            output.AppendLine(command.Error.TrimEnd());
                        }

                        output.AppendLine($"Exit code: {command.ExitStatus}");
                    }

                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {
                output.AppendLine($"ERROR: {ex.Message}");
            }

            return output.ToString().TrimEnd();
        }

        internal static string BuildNonInteractiveCommand(string userCommand)
        {
            var prepared = userCommand.Trim();

            // Prefer apt-get in scripts (avoids "apt does not have a stable CLI interface").
            prepared = Regex.Replace(prepared, @"\bapt\b(?!-)", "apt-get");

            // sudo clears most env vars — put DEBIAN_FRONTEND on the sudo command itself.
            prepared = Regex.Replace(
                prepared,
                @"\bsudo\s+(?!DEBIAN_FRONTEND=)",
                "sudo DEBIAN_FRONTEND=noninteractive NEEDRESTART_MODE=a APT_LISTCHANGES_FRONTEND=none ");

            return
                "export DEBIAN_FRONTEND=noninteractive; " +
                "export NEEDRESTART_MODE=a; " +
                "export APT_LISTCHANGES_FRONTEND=none; " +
                "export TERM=dumb; " +
                prepared;
        }
    }
}
