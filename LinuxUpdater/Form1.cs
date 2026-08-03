using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LinuxUpdater.Data;
using LinuxUpdater.Models;
using LinuxUpdater.Services;

namespace LinuxUpdater
{
    public partial class Form1 : Form
    {
        private readonly Database _database = new Database();
        private readonly MachineRunner _runner = new MachineRunner();
        private bool _isRunning;

        public Form1()
        {
            InitializeComponent();
            LoadMachines();
        }

        private void LoadMachines()
        {
            var checkedIds = new HashSet<int>();
            foreach (var item in clbMachines.CheckedItems.Cast<Machine>())
            {
                checkedIds.Add(item.Id);
            }

            var selected = clbMachines.SelectedItem as Machine;

            clbMachines.BeginUpdate();
            clbMachines.Items.Clear();

            foreach (var machine in _database.GetMachines())
            {
                var index = clbMachines.Items.Add(machine, checkedIds.Contains(machine.Id));
                if (selected != null && selected.Id == machine.Id)
                {
                    clbMachines.SelectedIndex = index;
                }
            }

            clbMachines.EndUpdate();
            UpdateUiState();
        }

        private void UpdateUiState()
        {
            var hasSelection = clbMachines.SelectedItem != null;
            var hasChecked = clbMachines.CheckedItems.Count > 0;

            btnEdit.Enabled = !_isRunning && hasSelection;
            btnDelete.Enabled = !_isRunning && hasSelection;
            btnRun.Enabled = !_isRunning && hasChecked;
            btnAddLinux.Enabled = !_isRunning;
            btnAddWindows.Enabled = !_isRunning;
            btnViewLogs.Enabled = !_isRunning;
            btnSelectAll.Enabled = !_isRunning && clbMachines.Items.Count > 0;
            btnSelectNone.Enabled = !_isRunning && clbMachines.Items.Count > 0;
            clbMachines.Enabled = !_isRunning;
            progressBar.Visible = _isRunning;
            progressBar.Style = _isRunning ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;
        }

        private void AddMachine(MachineType osType)
        {
            using (var form = new AddMachineForm(osType))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    _database.AddMachine(form.Machine);
                    LoadMachines();
                }
            }
        }

        private void btnAddLinux_Click(object sender, EventArgs e)
        {
            AddMachine(MachineType.Linux);
        }

        private void btnAddWindows_Click(object sender, EventArgs e)
        {
            AddMachine(MachineType.Windows);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!(clbMachines.SelectedItem is Machine machine))
            {
                return;
            }

            using (var form = new AddMachineForm(machine))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    _database.UpdateMachine(form.Machine);
                    LoadMachines();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!(clbMachines.SelectedItem is Machine machine))
            {
                return;
            }

            var result = MessageBox.Show(
                $"Delete machine '{machine.Name}'?",
                "Delete Machine",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            _database.DeleteMachine(machine.Id);
            LoadMachines();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < clbMachines.Items.Count; i++)
            {
                clbMachines.SetItemChecked(i, true);
            }

            UpdateUiState();
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < clbMachines.Items.Count; i++)
            {
                clbMachines.SetItemChecked(i, false);
            }

            UpdateUiState();
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            var selectedMachines = clbMachines.CheckedItems.Cast<Machine>().ToList();
            if (selectedMachines.Count == 0)
            {
                MessageBox.Show(
                    "Select at least one machine.",
                    "Run Commands",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var linuxCount = selectedMachines.Count(m => m.OsType == MachineType.Linux);
            var windowsCount = selectedMachines.Count(m => m.OsType == MachineType.Windows);

            var confirm = MessageBox.Show(
                $"Run stored commands on {selectedMachines.Count} machine(s)?\n\n" +
                $"Linux (SSH): {linuxCount}\nWindows (WinRM): {windowsCount}",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            _isRunning = true;
            UpdateUiState();
            txtStatus.Clear();
            AppendStatus($"Starting updates for {selectedMachines.Count} machine(s)...{Environment.NewLine}");

            foreach (var machine in selectedMachines)
            {
                var protocol = MachineRunner.ProtocolLabel(machine);
                AppendStatus($"[{DateTime.Now:HH:mm:ss}] Connecting to {machine.Name} ({machine.IpAddress}) via {protocol}...");
                AppendStatus($"Command: {machine.Command}");

                var output = await _runner.RunCommandAsync(machine);
                _database.AddLog(machine.Name, output);

                AppendStatus(output);
                AppendStatus(new string('-', 60));
            }

            AppendStatus("Done. Outputs were saved to the log table.");
            _isRunning = false;
            UpdateUiState();
        }

        private void btnViewLogs_Click(object sender, EventArgs e)
        {
            using (var form = new LogsForm(_database))
            {
                form.ShowDialog(this);
            }
        }

        private void clbMachines_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((Action)UpdateUiState);
        }

        private void clbMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUiState();
        }

        private void AppendStatus(string text)
        {
            txtStatus.AppendText(text + Environment.NewLine);
            txtStatus.SelectionStart = txtStatus.TextLength;
            txtStatus.ScrollToCaret();
        }
    }
}
