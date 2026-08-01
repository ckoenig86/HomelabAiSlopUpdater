namespace LinuxUpdater
{
    partial class LogsForm
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.lstLogs = new System.Windows.Forms.ListBox();
            this.lblDetails = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // splitContainer
            //
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            //
            // splitContainer.Panel1
            //
            this.splitContainer.Panel1.Controls.Add(this.lstLogs);
            this.splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(10, 10, 5, 10);
            //
            // splitContainer.Panel2
            //
            this.splitContainer.Panel2.Controls.Add(this.txtOutput);
            this.splitContainer.Panel2.Controls.Add(this.lblDetails);
            this.splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(5, 10, 10, 10);
            this.splitContainer.Size = new System.Drawing.Size(900, 500);
            this.splitContainer.SplitterDistance = 320;
            this.splitContainer.TabIndex = 0;
            //
            // lstLogs
            //
            this.lstLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstLogs.FormattingEnabled = true;
            this.lstLogs.IntegralHeight = false;
            this.lstLogs.Location = new System.Drawing.Point(10, 10);
            this.lstLogs.Name = "lstLogs";
            this.lstLogs.Size = new System.Drawing.Size(305, 480);
            this.lstLogs.TabIndex = 0;
            this.lstLogs.Format += new System.Windows.Forms.ListControlConvertEventHandler(this.lstLogs_Format);
            this.lstLogs.SelectedIndexChanged += new System.EventHandler(this.lstLogs_SelectedIndexChanged);
            //
            // lblDetails
            //
            this.lblDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDetails.Location = new System.Drawing.Point(5, 10);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblDetails.Size = new System.Drawing.Size(561, 28);
            this.lblDetails.TabIndex = 0;
            this.lblDetails.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // txtOutput
            //
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.txtOutput.Location = new System.Drawing.Point(5, 38);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(561, 452);
            this.txtOutput.TabIndex = 1;
            this.txtOutput.WordWrap = false;
            //
            // panelButtons
            //
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Controls.Add(this.btnClear);
            this.panelButtons.Controls.Add(this.btnRefresh);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 500);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(10);
            this.panelButtons.Size = new System.Drawing.Size(900, 52);
            this.panelButtons.TabIndex = 1;
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(795, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // btnClear
            //
            this.btnClear.Location = new System.Drawing.Point(111, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(95, 28);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "Clear Logs";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            //
            // btnRefresh
            //
            this.btnRefresh.Location = new System.Drawing.Point(10, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(95, 28);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            //
            // LogsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 552);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelButtons);
            this.MinimumSize = new System.Drawing.Size(700, 450);
            this.Name = "LogsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SSH Logs";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListBox lstLogs;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnRefresh;
    }
}