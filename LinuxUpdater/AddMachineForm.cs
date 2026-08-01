using System;
using System.Windows.Forms;
using LinuxUpdater.Models;

namespace LinuxUpdater
{
    public partial class AddMachineForm : Form
    {
        public Machine Machine { get; private set; }

        public AddMachineForm()
            : this(null)
        {
        }

        public AddMachineForm(Machine existing)
        {
            InitializeComponent();

            if (existing != null)
            {
                Text = "Edit Machine";
                btnSave.Text = "Save";
                Machine = new Machine
                {
                    Id = existing.Id,
                    Name = existing.Name,
                    IpAddress = existing.IpAddress,
                    Username = existing.Username,
                    Password = existing.Password,
                    Command = existing.Command
                };

                txtName.Text = existing.Name;
                txtIp.Text = existing.IpAddress;
                txtUsername.Text = existing.Username;
                txtPassword.Text = existing.Password;
                txtCommand.Text = existing.Command;
            }
            else
            {
                Text = "Add Machine";
                txtCommand.Text =
                    "sudo apt-get -y update && " +
                    "sudo apt-get -y -o Dpkg::Options::=\"--force-confdef\" -o Dpkg::Options::=\"--force-confold\" upgrade";
            }
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
