using System;

namespace LinuxUpdater.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string MachineName { get; set; }
        public string Output { get; set; }
    }
}
