namespace Tms.LocalService
{
    partial class frmLocalService
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocalService));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuService = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnStartService = new System.Windows.Forms.ToolStripMenuItem();
            this.btnStopService = new System.Windows.Forms.ToolStripMenuItem();
            this.btnChangeProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.btnQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuService.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuService;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "TMS.Local.Service";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuService
            // 
            this.contextMenuService.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStartService,
            this.btnStopService,
            this.btnChangeProfile,
            this.btnQuit});
            this.contextMenuService.Name = "contextMenuStrip1";
            this.contextMenuService.Size = new System.Drawing.Size(153, 92);
            // 
            // btnStartService
            // 
            this.btnStartService.Name = "btnStartService";
            this.btnStartService.Size = new System.Drawing.Size(152, 22);
            this.btnStartService.Text = "Start Service";
            // 
            // btnStopService
            // 
            this.btnStopService.Name = "btnStopService";
            this.btnStopService.Size = new System.Drawing.Size(152, 22);
            this.btnStopService.Text = "Stop Service";
            // 
            // btnChangeProfile
            // 
            this.btnChangeProfile.Name = "btnChangeProfile";
            this.btnChangeProfile.Size = new System.Drawing.Size(152, 22);
            this.btnChangeProfile.Text = "Change Profile";
            // 
            // btnQuit
            // 
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(152, 22);
            this.btnQuit.Text = "Exit";
            // 
            // frmLocalService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "frmLocalService";
            this.Text = "Local Services";
            this.contextMenuService.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuService;
        private System.Windows.Forms.ToolStripMenuItem btnStartService;
        private System.Windows.Forms.ToolStripMenuItem btnStopService;
        private System.Windows.Forms.ToolStripMenuItem btnChangeProfile;
        private System.Windows.Forms.ToolStripMenuItem btnQuit;
    }
}

