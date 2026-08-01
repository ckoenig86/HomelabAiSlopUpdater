using System;
using System.Windows.Forms;
using LinuxUpdater.Data;
using LinuxUpdater.Models;

namespace LinuxUpdater
{
    public partial class LogsForm : Form
    {
        private readonly Database _database;

        public LogsForm(Database database)
        {
            _database = database;
            InitializeComponent();
            LoadLogs();
        }

        private void LoadLogs()
        {
            var selectedId = lstLogs.SelectedItem is LogEntry selected ? selected.Id : -1;

            lstLogs.BeginUpdate();
            lstLogs.Items.Clear();

            foreach (var log in _database.GetLogs())
            {
                lstLogs.Items.Add(log);
            }

            lstLogs.EndUpdate();

            if (selectedId >= 0)
            {
                for (var i = 0; i < lstLogs.Items.Count; i++)
                {
                    if (((LogEntry)lstLogs.Items[i]).Id == selectedId)
                    {
                        lstLogs.SelectedIndex = i;
                        break;
                    }
                }
            }
            else if (lstLogs.Items.Count > 0)
            {
                lstLogs.SelectedIndex = 0;
            }
            else
            {
                txtOutput.Clear();
            }
        }

        private void lstLogs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstLogs.SelectedItem is LogEntry log)
            {
                lblDetails.Text = $"{log.Timestamp:yyyy-MM-dd HH:mm:ss}  |  {log.MachineName}";
                txtOutput.Text = log.Output;
            }
            else
            {
                lblDetails.Text = string.Empty;
                txtOutput.Clear();
            }
        }

        private void lstLogs_Format(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is LogEntry log)
            {
                e.Value = $"{log.Timestamp:yyyy-MM-dd HH:mm:ss}  -  {log.MachineName}";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadLogs();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Delete all log entries?",
                "Clear Logs",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            _database.ClearLogs();
            LoadLogs();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
