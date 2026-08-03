using System;
using System.Windows.Forms;
using LinuxUpdater.Models;

namespace LinuxUpdater
{
    public partial class AddMachineForm : Form
    {
        private readonly MachineType _osType;

        public Machine Machine { get; private set; }

        public AddMachineForm(MachineType osType)
            : this(null, osType)
        {
        }

        public AddMachineForm(Machine existing)
            : this(existing, existing?.OsType ?? MachineType.Linux)
        {
        }

        public AddMachineForm(Machine existing, MachineType osType)
        {
            _osType = existing?.OsType ?? osType;
            InitializeComponent();

            if (existing != null)
            {
                Text = _osType == MachineType.Windows ? "Edit Windows Machine" : "Edit Linux Machine";
                btnSave.Text = "Save";
                Machine = new Machine
                {
                    Id = existing.Id,
                    Name = existing.Name,
                    IpAddress = existing.IpAddress,
                    Username = existing.Username,
                    Password = existing.Password,
                    Command = existing.Command,
                    OsType = existing.OsType
                };

                txtName.Text = existing.Name;
                txtIp.Text = existing.IpAddress;
                txtUsername.Text = existing.Username;
                txtPassword.Text = existing.Password;
                txtCommand.Text = existing.Command;
            }
            else
            {
                Text = _osType == MachineType.Windows ? "Add Windows Machine" : "Add Linux Machine";
                txtCommand.Text = GetDefaultCommand(_osType);
            }

            lblProtocol.Text = _osType == MachineType.Windows
                ? "Protocol: WinRM (HTTP :5985)"
                : "Protocol: SSH";
        }

        private static string GetDefaultCommand(MachineType osType)
        {
            if (osType == MachineType.Windows)
            {
                return string.Join(Environment.NewLine, new[]
                {
                    "$session = New-Object -ComObject Microsoft.Update.Session",
                    "$searcher = $session.CreateUpdateSearcher()",
                    "$result = $searcher.Search(\"IsInstalled=0 and Type='Software' and IsHidden=0\")",
                    "if ($result.Updates.Count -eq 0) { 'No updates available.'; return }",
                    "$toDownload = New-Object -ComObject Microsoft.Update.UpdateColl",
                    "foreach ($update in $result.Updates) { [void]$toDownload.Add($update); $update.Title }",
                    "$downloader = $session.CreateUpdateDownloader()",
                    "$downloader.Updates = $toDownload",
                    "$downloader.Download() | Out-Null",
                    "$toInstall = New-Object -ComObject Microsoft.Update.UpdateColl",
                    "foreach ($update in $result.Updates) { if ($update.IsDownloaded) { [void]$toInstall.Add($update) } }",
                    "$installer = $session.CreateUpdateInstaller()",
                    "$installer.Updates = $toInstall",
                    "$installResult = $installer.Install()",
                    "\"ResultCode=$($installResult.ResultCode); RebootRequired=$($installResult.RebootRequired)\""
                });
            }

            return
                "sudo apt-get -y update && " +
                "sudo apt-get -y -o Dpkg::Options::=\"--force-confdef\" -o Dpkg::Options::=\"--force-confold\" upgrade";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtIp.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtCommand.Text))
            {
                MessageBox.Show(
                    "Please fill in all fields.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (Machine == null)
            {
                Machine = new Machine();
            }

            Machine.Name = txtName.Text.Trim();
            Machine.IpAddress = txtIp.Text.Trim();
            Machine.Username = txtUsername.Text.Trim();
            Machine.Password = txtPassword.Text;
            Machine.Command = txtCommand.Text.Trim();
            Machine.OsType = _osType;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
