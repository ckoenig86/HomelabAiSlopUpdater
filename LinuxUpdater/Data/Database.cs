using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using LinuxUpdater.Models;

namespace LinuxUpdater.Data
{
    public class Database
    {
        private readonly string _connectionString;

        public Database()
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "linuxupdater.db");
            _connectionString = $"Data Source={dbPath};Version=3;";
            Initialize();
        }

        private SQLiteConnection CreateConnection()
        {
            var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            return connection;
        }

        private void Initialize()
        {
            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Machines (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        IpAddress TEXT NOT NULL,
                        Username TEXT NOT NULL,
                        Password TEXT NOT NULL,
                        Command TEXT NOT NULL,
                        OsType TEXT NOT NULL DEFAULT 'Linux'
                    );

                    CREATE TABLE IF NOT EXISTS Logs (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Timestamp TEXT NOT NULL,
                        MachineName TEXT NOT NULL,
                        Output TEXT NOT NULL
                    );";
                command.ExecuteNonQuery();
            }

            EnsureOsTypeColumn();
        }

        private void EnsureOsTypeColumn()
        {
            using (var connection = CreateConnection())
            {
                var hasOsType = false;
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "PRAGMA table_info(Machines);";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (string.Equals(reader.GetString(1), "OsType", StringComparison.OrdinalIgnoreCase))
                            {
                                hasOsType = true;
                                break;
                            }
                        }
                    }
                }

                if (!hasOsType)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "ALTER TABLE Machines ADD COLUMN OsType TEXT NOT NULL DEFAULT 'Linux';";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public List<Machine> GetMachines()
        {
            var machines = new List<Machine>();

            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText =
                    "SELECT Id, Name, IpAddress, Username, Password, Command, OsType FROM Machines ORDER BY Name;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        machines.Add(new Machine
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IpAddress = reader.GetString(2),
                            Username = reader.GetString(3),
                            Password = reader.GetString(4),
                            Command = reader.GetString(5),
                            OsType = ParseOsType(reader.IsDBNull(6) ? "Linux" : reader.GetString(6))
                        });
                    }
                }
            }

            return machines;
        }

        public void AddMachine(Machine machine)
        {
            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    INSERT INTO Machines (Name, IpAddress, Username, Password, Command, OsType)
                    VALUES (@Name, @IpAddress, @Username, @Password, @Command, @OsType);";
                command.Parameters.AddWithValue("@Name", machine.Name);
                command.Parameters.AddWithValue("@IpAddress", machine.IpAddress);
                command.Parameters.AddWithValue("@Username", machine.Username);
                command.Parameters.AddWithValue("@Password", machine.Password);
                command.Parameters.AddWithValue("@Command", machine.Command);
                command.Parameters.AddWithValue("@OsType", machine.OsType.ToString());
                command.ExecuteNonQuery();
            }
        }

        public void UpdateMachine(Machine machine)
        {
            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    UPDATE Machines
                    SET Name = @Name,
                        IpAddress = @IpAddress,
                        Username = @Username,
                        Password = @Password,
                        Command = @Command,
                        OsType = @OsType
                    WHERE Id = @Id;";
                command.Parameters.AddWithValue("@Id", machine.Id);
                command.Parameters.AddWithValue("@Name", machine.Name);
                command.Parameters.AddWithValue("@IpAddress", machine.IpAddress);
                command.Parameters.AddWithValue("@Username", machine.Username);
                command.Parameters.AddWithValue("@Password", machine.Password);
                command.Parameters.AddWithValue("@Command", machine.Command);
                command.Parameters.AddWithValue("@OsType", machine.OsType.ToString());
                command.ExecuteNonQuery();
            }
        }

        public void DeleteMachine(int id)
        {
            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Machines WHERE Id = @Id;";
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }

        public void AddLog(string machineName, string output)
        {
            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = @"
                    INSERT INTO Logs (Timestamp, MachineName, Output)
                    VALUES (@Timestamp, @MachineName, @Output);";
                command.Parameters.AddWithValue("@Timestamp", DateTime.Now.ToString("o"));
                command.Parameters.AddWithValue("@MachineName", machineName);
                command.Parameters.AddWithValue("@Output", output ?? string.Empty);
                command.ExecuteNonQuery();
            }
        }

        public List<LogEntry> GetLogs()
        {
            var logs = new List<LogEntry>();

            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT Id, Timestamp, MachineName, Output FROM Logs ORDER BY Timestamp DESC;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logs.Add(new LogEntry
                        {
                            Id = reader.GetInt32(0),
                            Timestamp = DateTime.Parse(reader.GetString(1)),
                            MachineName = reader.GetString(2),
                            Output = reader.GetString(3)
                        });
                    }
                }
            }

            return logs;
        }

        public void ClearLogs()
        {
            using (var connection = CreateConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "DELETE FROM Logs;";
                command.ExecuteNonQuery();
            }
        }

        private static MachineType ParseOsType(string value)
        {
            return string.Equals(value, "Windows", StringComparison.OrdinalIgnoreCase)
                ? MachineType.Windows
                : MachineType.Linux;
        }
    }
}
