namespace LinuxUpdater
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblMachines = new System.Windows.Forms.Label();
            this.clbMachines = new System.Windows.Forms.CheckedListBox();
            this.panelLeftButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddLinux = new System.Windows.Forms.Button();
            this.btnAddWindows = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.panelRight = new System.Windows.Forms.Panel();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnViewLogs = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.panelLeftButtons.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.SuspendLayout();
            //
            // lblMachines
            //
            this.lblMachines.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMachines.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMachines.Location = new System.Drawing.Point(10, 10);
            this.lblMachines.Name = "lblMachines";
            this.lblMachines.Size = new System.Drawing.Size(300, 24);
            this.lblMachines.TabIndex = 0;
            this.lblMachines.Text = "Machines (check to include)";
            this.lblMachines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // clbMachines
            //
            this.clbMachines.CheckOnClick = true;
            this.clbMachines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbMachines.FormattingEnabled = true;
            this.clbMachines.IntegralHeight = false;
            this.clbMachines.Location = new System.Drawing.Point(10, 34);
            this.clbMachines.Name = "clbMachines";
            this.clbMachines.Size = new System.Drawing.Size(300, 356);
            this.clbMachines.TabIndex = 1;
            this.clbMachines.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clbMachines_ItemCheck);
            this.clbMachines.SelectedIndexChanged += new System.EventHandler(this.clbMachines_SelectedIndexChanged);
            //
            // panelLeftButtons
            //
            this.panelLeftButtons.AutoSize = true;
            this.panelLeftButtons.Controls.Add(this.btnAddLinux);
            this.panelLeftButtons.Controls.Add(this.btnAddWindows);
            this.panelLeftButtons.Controls.Add(this.btnEdit);
            this.panelLeftButtons.Controls.Add(this.btnDelete);
            this.panelLeftButtons.Controls.Add(this.btnSelectAll);
            this.panelLeftButtons.Controls.Add(this.btnSelectNone);
            this.panelLeftButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelLeftButtons.Location = new System.Drawing.Point(10, 356);
            this.panelLeftButtons.Name = "panelLeftButtons";
            this.panelLeftButtons.Size = new System.Drawing.Size(300, 104);
            this.panelLeftButtons.TabIndex = 2;
            //
            // btnAddLinux
            //
            this.btnAddLinux.Location = new System.Drawing.Point(3, 3);
            this.btnAddLinux.Name = "btnAddLinux";
            this.btnAddLinux.Size = new System.Drawing.Size(100, 28);
            this.btnAddLinux.TabIndex = 0;
            this.btnAddLinux.Text = "Add Linux";
            this.btnAddLinux.UseVisualStyleBackColor = true;
            this.btnAddLinux.Click += new System.EventHandler(this.btnAddLinux_Click);
            //
            // btnAddWindows
            //
            this.btnAddWindows.Location = new System.Drawing.Point(109, 3);
            this.btnAddWindows.Name = "btnAddWindows";
            this.btnAddWindows.Size = new System.Drawing.Size(100, 28);
            this.btnAddWindows.TabIndex = 1;
            this.btnAddWindows.Text = "Add Windows";
            this.btnAddWindows.UseVisualStyleBackColor = true;
            this.btnAddWindows.Click += new System.EventHandler(this.btnAddWindows_Click);
            //
            // btnEdit
            //
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(215, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 28);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            //
            // btnDelete
            //
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(3, 37);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 28);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            //
            // btnSelectAll
            //
            this.btnSelectAll.Location = new System.Drawing.Point(79, 37);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(90, 28);
            this.btnSelectAll.TabIndex = 4;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            //
            // btnSelectNone
            //
            this.btnSelectNone.Location = new System.Drawing.Point(175, 37);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(90, 28);
            this.btnSelectNone.TabIndex = 5;
            this.btnSelectNone.Text = "Select None";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            //
            // panelRight
            //
            this.panelRight.Controls.Add(this.txtStatus);
            this.panelRight.Controls.Add(this.lblStatus);
            this.panelRight.Controls.Add(this.panelActions);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(10);
            this.panelRight.Size = new System.Drawing.Size(546, 470);
            this.panelRight.TabIndex = 0;
            //
            // txtStatus
            //
            this.txtStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStatus.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.txtStatus.Location = new System.Drawing.Point(10, 34);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtStatus.Size = new System.Drawing.Size(526, 374);
            this.txtStatus.TabIndex = 1;
            this.txtStatus.WordWrap = false;
            //
            // lblStatus
            //
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(10, 10);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(526, 24);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Run output";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelActions
            //
            this.panelActions.Controls.Add(this.progressBar);
            this.panelActions.Controls.Add(this.btnViewLogs);
            this.panelActions.Controls.Add(this.btnRun);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(10, 408);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(526, 52);
            this.panelActions.TabIndex = 2;
            //
            // progressBar
            //
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(0, 36);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(526, 10);
            this.progressBar.TabIndex = 2;
            this.progressBar.Visible = false;
            //
            // btnViewLogs
            //
            this.btnViewLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewLogs.Location = new System.Drawing.Point(410, 3);
            this.btnViewLogs.Name = "btnViewLogs";
            this.btnViewLogs.Size = new System.Drawing.Size(116, 28);
            this.btnViewLogs.TabIndex = 1;
            this.btnViewLogs.Text = "View Logs";
            this.btnViewLogs.UseVisualStyleBackColor = true;
            this.btnViewLogs.Click += new System.EventHandler(this.btnViewLogs_Click);
            //
            // btnRun
            //
            this.btnRun.Enabled = false;
            this.btnRun.Location = new System.Drawing.Point(0, 3);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(140, 28);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run Selected";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            //
            // splitMain
            //
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            //
            // splitMain.Panel1
            //
            this.splitMain.Panel1.Controls.Add(this.clbMachines);
            this.splitMain.Panel1.Controls.Add(this.panelLeftButtons);
            this.splitMain.Panel1.Controls.Add(this.lblMachines);
            this.splitMain.Panel1.Padding = new System.Windows.Forms.Padding(10);
            //
            // splitMain.Panel2
            //
            this.splitMain.Panel2.Controls.Add(this.panelRight);
            this.splitMain.Size = new System.Drawing.Size(870, 470);
            this.splitMain.SplitterDistance = 320;
            this.splitMain.TabIndex = 0;
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 470);
            this.Controls.Add(this.splitMain);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Homelab Updater";
            this.panelLeftButtons.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel1.PerformLayout();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblMachines;
        private System.Windows.Forms.CheckedListBox clbMachines;
        private System.Windows.Forms.FlowLayoutPanel panelLeftButtons;
        private System.Windows.Forms.Button btnAddLinux;
        private System.Windows.Forms.Button btnAddWindows;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnViewLogs;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.SplitContainer splitMain;
    }
}