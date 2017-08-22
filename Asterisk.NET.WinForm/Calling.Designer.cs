namespace Asterisk.NET.WinForm
{
    partial class Calling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calling));
            this.head = new System.Windows.Forms.PictureBox();
            this.groupBoxInfo = new System.Windows.Forms.GroupBox();
            this.CallState = new System.Windows.Forms.Label();
            this.btnAnswer = new System.Windows.Forms.Button();
            this.btnAddComment = new System.Windows.Forms.Button();
            this.btnHold = new System.Windows.Forms.Button();
            this.btnAddToGroup = new System.Windows.Forms.Button();
            this.btnRedirect = new System.Windows.Forms.Button();
            this.Note = new System.Windows.Forms.TextBox();
            this.FIO = new System.Windows.Forms.Label();
            this.PatientGroup = new System.Windows.Forms.Label();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.groupBoxActions = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbGroups = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.head)).BeginInit();
            this.groupBoxInfo.SuspendLayout();
            this.groupBoxActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // head
            // 
            this.head.Dock = System.Windows.Forms.DockStyle.Top;
            this.head.InitialImage = null;
            this.head.Location = new System.Drawing.Point(0, 0);
            this.head.Name = "head";
            this.head.Size = new System.Drawing.Size(520, 91);
            this.head.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.head.TabIndex = 18;
            this.head.TabStop = false;
            // 
            // groupBoxInfo
            // 
            this.groupBoxInfo.Controls.Add(this.CallState);
            this.groupBoxInfo.Controls.Add(this.btnAnswer);
            this.groupBoxInfo.Controls.Add(this.btnAddComment);
            this.groupBoxInfo.Controls.Add(this.btnHold);
            this.groupBoxInfo.Controls.Add(this.btnAddToGroup);
            this.groupBoxInfo.Controls.Add(this.btnRedirect);
            this.groupBoxInfo.Controls.Add(this.Note);
            this.groupBoxInfo.Controls.Add(this.FIO);
            this.groupBoxInfo.Controls.Add(this.PatientGroup);
            this.groupBoxInfo.Location = new System.Drawing.Point(0, 91);
            this.groupBoxInfo.Name = "groupBoxInfo";
            this.groupBoxInfo.Size = new System.Drawing.Size(519, 217);
            this.groupBoxInfo.TabIndex = 27;
            this.groupBoxInfo.TabStop = false;
            // 
            // CallState
            // 
            this.CallState.AutoSize = true;
            this.CallState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CallState.ForeColor = System.Drawing.Color.Red;
            this.CallState.Location = new System.Drawing.Point(17, 167);
            this.CallState.Name = "CallState";
            this.CallState.Size = new System.Drawing.Size(0, 13);
            this.CallState.TabIndex = 36;
            // 
            // btnAnswer
            // 
            this.btnAnswer.Location = new System.Drawing.Point(226, 158);
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.Size = new System.Drawing.Size(107, 30);
            this.btnAnswer.TabIndex = 34;
            this.btnAnswer.Text = "Завершить";
            this.btnAnswer.UseVisualStyleBackColor = true;
            this.btnAnswer.Click += new System.EventHandler(this.btnAnswer_Click);
            // 
            // btnAddComment
            // 
            this.btnAddComment.Image = ((System.Drawing.Image)(resources.GetObject("btnAddComment.Image")));
            this.btnAddComment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddComment.Location = new System.Drawing.Point(344, 158);
            this.btnAddComment.Name = "btnAddComment";
            this.btnAddComment.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnAddComment.Size = new System.Drawing.Size(154, 30);
            this.btnAddComment.TabIndex = 33;
            this.btnAddComment.Text = "   + Комментарий";
            this.btnAddComment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddComment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddComment.UseVisualStyleBackColor = true;
            this.btnAddComment.Click += new System.EventHandler(this.btnAddComment_Click);
            // 
            // btnHold
            // 
            this.btnHold.Image = ((System.Drawing.Image)(resources.GetObject("btnHold.Image")));
            this.btnHold.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHold.Location = new System.Drawing.Point(344, 113);
            this.btnHold.Name = "btnHold";
            this.btnHold.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnHold.Size = new System.Drawing.Size(154, 30);
            this.btnHold.TabIndex = 32;
            this.btnHold.Text = "   HOLD";
            this.btnHold.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHold.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHold.UseVisualStyleBackColor = true;
            this.btnHold.Visible = false;
            // 
            // btnAddToGroup
            // 
            this.btnAddToGroup.Image = ((System.Drawing.Image)(resources.GetObject("btnAddToGroup.Image")));
            this.btnAddToGroup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddToGroup.Location = new System.Drawing.Point(344, 68);
            this.btnAddToGroup.Name = "btnAddToGroup";
            this.btnAddToGroup.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnAddToGroup.Size = new System.Drawing.Size(154, 30);
            this.btnAddToGroup.TabIndex = 31;
            this.btnAddToGroup.Text = "   Переадресация";
            this.btnAddToGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddToGroup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddToGroup.UseVisualStyleBackColor = true;
            this.btnAddToGroup.Visible = false;
            // 
            // btnRedirect
            // 
            this.btnRedirect.Image = ((System.Drawing.Image)(resources.GetObject("btnRedirect.Image")));
            this.btnRedirect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRedirect.Location = new System.Drawing.Point(344, 23);
            this.btnRedirect.Name = "btnRedirect";
            this.btnRedirect.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnRedirect.Size = new System.Drawing.Size(154, 30);
            this.btnRedirect.TabIndex = 30;
            this.btnRedirect.Text = "   Прямая переадр.";
            this.btnRedirect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRedirect.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRedirect.UseVisualStyleBackColor = true;
            this.btnRedirect.Visible = false;
            this.btnRedirect.Click += new System.EventHandler(this.btnRedirect_Click);
            // 
            // Note
            // 
            this.Note.BackColor = System.Drawing.SystemColors.Control;
            this.Note.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Note.Enabled = false;
            this.Note.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Note.Location = new System.Drawing.Point(13, 75);
            this.Note.Multiline = true;
            this.Note.Name = "Note";
            this.Note.ReadOnly = true;
            this.Note.Size = new System.Drawing.Size(290, 71);
            this.Note.TabIndex = 29;
            this.Note.Text = "31.02.1963 г., Хороший человек, несколько слов в качестве комментария о пациенте," +
    " думаю трех строк будет достаточно.";
            // 
            // FIO
            // 
            this.FIO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.FIO.AutoEllipsis = true;
            this.FIO.AutoSize = true;
            this.FIO.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FIO.Location = new System.Drawing.Point(13, 54);
            this.FIO.Name = "FIO";
            this.FIO.Size = new System.Drawing.Size(199, 19);
            this.FIO.TabIndex = 28;
            this.FIO.Text = "Иванов Иван Иванович";
            this.FIO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PatientGroup
            // 
            this.PatientGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PatientGroup.AutoEllipsis = true;
            this.PatientGroup.AutoSize = true;
            this.PatientGroup.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PatientGroup.Location = new System.Drawing.Point(13, 18);
            this.PatientGroup.Name = "PatientGroup";
            this.PatientGroup.Size = new System.Drawing.Size(217, 22);
            this.PatientGroup.TabIndex = 27;
            this.PatientGroup.Text = "Пациент Euromed Clinic";
            this.PatientGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.PatientGroup.Visible = false;
            // 
            // timerClock
            // 
            this.timerClock.Interval = 900;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // groupBoxActions
            // 
            this.groupBoxActions.Controls.Add(this.btnSave);
            this.groupBoxActions.Controls.Add(this.btnBack);
            this.groupBoxActions.Controls.Add(this.panelButtons);
            this.groupBoxActions.Controls.Add(this.label1);
            this.groupBoxActions.Controls.Add(this.cmbGroups);
            this.groupBoxActions.Location = new System.Drawing.Point(0, 91);
            this.groupBoxActions.Name = "groupBoxActions";
            this.groupBoxActions.Size = new System.Drawing.Size(519, 217);
            this.groupBoxActions.TabIndex = 28;
            this.groupBoxActions.TabStop = false;
            this.groupBoxActions.Visible = false;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(406, 182);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(107, 30);
            this.btnSave.TabIndex = 44;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnBack
            // 
            this.btnBack.Enabled = false;
            this.btnBack.Location = new System.Drawing.Point(295, 182);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(107, 30);
            this.btnBack.TabIndex = 35;
            this.btnBack.Text = "Назад";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.AutoScroll = true;
            this.panelButtons.Location = new System.Drawing.Point(6, 46);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(507, 134);
            this.panelButtons.TabIndex = 43;
            this.panelButtons.MouseEnter += new System.EventHandler(this.panelButtons_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(241, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Группа:";
            // 
            // cmbGroups
            // 
            this.cmbGroups.AccessibleDescription = "";
            this.cmbGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroups.FormattingEnabled = true;
            this.cmbGroups.Items.AddRange(new object[] {
            "Не определено",
            "Пациент Euromed Clinic",
            "Пациент Euromed In Vitro",
            "VIP пациен",
            "Новый мобильный Euromed Clinic",
            "Новый городской Euromed Clinic",
            "Новый иногородний Euromed Clinic",
            "Новый иногородний Euromed In Vitro",
            "Новый международный",
            "Новый Euromed In Vitro",
            "Новый Euromed Еxpress",
            "ЛПУ",
            "Страховая компания",
            "Insurance company",
            "Гостиница",
            "Сотрудник"});
            this.cmbGroups.Location = new System.Drawing.Point(292, 18);
            this.cmbGroups.Name = "cmbGroups";
            this.cmbGroups.Size = new System.Drawing.Size(221, 21);
            this.cmbGroups.TabIndex = 38;
            this.cmbGroups.SelectedIndexChanged += new System.EventHandler(this.cmbGroups_SelectedIndexChanged);
            // 
            // Calling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 308);
            this.Controls.Add(this.groupBoxActions);
            this.Controls.Add(this.groupBoxInfo);
            this.Controls.Add(this.head);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Calling";
            this.ShowInTaskbar = false;
            this.Text = "Calling";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Calling_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Calling_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.head)).EndInit();
            this.groupBoxInfo.ResumeLayout(false);
            this.groupBoxInfo.PerformLayout();
            this.groupBoxActions.ResumeLayout(false);
            this.groupBoxActions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox head;
        private System.Windows.Forms.GroupBox groupBoxInfo;
        private System.Windows.Forms.Button btnAnswer;
        private System.Windows.Forms.Button btnAddComment;
        private System.Windows.Forms.Button btnHold;
        private System.Windows.Forms.Button btnAddToGroup;
        private System.Windows.Forms.Button btnRedirect;
        private System.Windows.Forms.TextBox Note;
        private System.Windows.Forms.Label FIO;
        private System.Windows.Forms.Label PatientGroup;
        private System.Windows.Forms.Label CallState;
        public System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.GroupBox groupBoxActions;
        private System.Windows.Forms.ComboBox cmbGroups;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Label label1;
    }
}