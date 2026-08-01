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

        public override string ToString()
        {
            return $"{Name} ({IpAddress})";
        }
    }
}
