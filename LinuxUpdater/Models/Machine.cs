namespace LinuxUpdater.Models
{
    public class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Command { get; set; }
        public MachineType OsType { get; set; } = MachineType.Linux;

        public override string ToString()
        {
            var tag = OsType == MachineType.Windows ? "Win" : "Linux";
            return $"[{tag}] {Name} ({IpAddress})";
        }
    }
}
