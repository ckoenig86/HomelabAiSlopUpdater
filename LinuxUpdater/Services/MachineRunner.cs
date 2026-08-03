using System.Threading.Tasks;
using LinuxUpdater.Models;

namespace LinuxUpdater.Services
{
    public class MachineRunner
    {
        private readonly SshRunner _sshRunner = new SshRunner();
        private readonly WinRmRunner _winRmRunner = new WinRmRunner();

        public Task<string> RunCommandAsync(Machine machine)
        {
            if (machine.OsType == MachineType.Windows)
            {
                return _winRmRunner.RunCommandAsync(machine);
            }

            return _sshRunner.RunCommandAsync(machine);
        }

        public static string ProtocolLabel(Machine machine)
        {
            return machine.OsType == MachineType.Windows ? "WinRM" : "SSH";
        }
    }
}