namespace Asterisk.NET.WinForm
{
	partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.Tray = new System.Windows.Forms.NotifyIcon(this.components);
            this.TrayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerTray = new System.Windows.Forms.Timer(this.components);
            this.TimerRing = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.WatchTimer = new System.Windows.Forms.Timer(this.components);
            this.cmbLogins = new System.Windows.Forms.ComboBox();
            this.tbExtNumb = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TrayMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Tray
            // 
            this.Tray.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            resources.ApplyResources(this.Tray, "Tray");
            this.Tray.ContextMenuStrip = this.TrayMenu;
            // 
            // TrayMenu
            // 
            this.TrayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem1});
            this.TrayMenu.Name = "TrayMenu";
            resources.ApplyResources(this.TrayMenu, "TrayMenu");
            this.TrayMenu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.TrayMenu_Closed);
            this.TrayMenu.Opened += new System.EventHandler(this.TrayMenu_Opened);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            resources.ApplyResources(this.toolStripMenuItem3, "toolStripMenuItem3");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // TimerTray
            // 
            this.TimerTray.Interval = 1;
            this.TimerTray.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // TimerRing
            // 
            this.TimerRing.Interval = 1;
            this.TimerRing.Tick += new System.EventHandler(this.TimerRing_Tick);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // WatchTimer
            // 
            this.WatchTimer.Tick += new System.EventHandler(this.WatchTimer_Tick);
            // 
            // cmbLogins
            // 
            this.cmbLogins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLogins.FormattingEnabled = true;
            resources.ApplyResources(this.cmbLogins, "cmbLogins");
            this.cmbLogins.Name = "cmbLogins";
            this.cmbLogins.SelectedIndexChanged += new System.EventHandler(this.cmbLogins_SelectedIndexChanged);
            // 
            // tbExtNumb
            // 
            resources.ApplyResources(this.tbExtNumb, "tbExtNumb");
            this.tbExtNumb.Name = "tbExtNumb";
            this.tbExtNumb.ReadOnly = true;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Name = "label3";
            // 
            // FormMain
            // 
            this.AcceptButton = this.button1;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbExtNumb);
            this.Controls.Add(this.cmbLogins);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.TrayMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.NotifyIcon Tray;
        private System.Windows.Forms.Timer TimerTray;
        private System.Windows.Forms.Timer TimerRing;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip TrayMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Timer WatchTimer;
        private System.Windows.Forms.ComboBox cmbLogins;
        private System.Windows.Forms.TextBox tbExtNumb;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

	}
}

