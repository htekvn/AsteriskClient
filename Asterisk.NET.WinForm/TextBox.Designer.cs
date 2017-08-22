namespace Asterisk.NET.WinForm
{
    partial class TextBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextBox));
            this.tbPhone = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkVoice = new System.Windows.Forms.CheckBox();
            this.btnVoiceHangUP = new System.Windows.Forms.Button();
            this.cmbExt = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbPhone
            // 
            this.tbPhone.Location = new System.Drawing.Point(95, 58);
            this.tbPhone.Name = "tbPhone";
            this.tbPhone.Size = new System.Drawing.Size(172, 20);
            this.tbPhone.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(45, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Номер:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(135, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Вызов";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(206, 94);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(61, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // checkVoice
            // 
            this.checkVoice.AutoSize = true;
            this.checkVoice.Location = new System.Drawing.Point(95, 9);
            this.checkVoice.Name = "checkVoice";
            this.checkVoice.Size = new System.Drawing.Size(114, 17);
            this.checkVoice.TabIndex = 4;
            this.checkVoice.Text = "голосовой вызов";
            this.checkVoice.UseVisualStyleBackColor = true;
            this.checkVoice.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnVoiceHangUP
            // 
            this.btnVoiceHangUP.Location = new System.Drawing.Point(22, 94);
            this.btnVoiceHangUP.Name = "btnVoiceHangUP";
            this.btnVoiceHangUP.Size = new System.Drawing.Size(71, 23);
            this.btnVoiceHangUP.TabIndex = 5;
            this.btnVoiceHangUP.Text = "Завершить";
            this.btnVoiceHangUP.UseVisualStyleBackColor = true;
            this.btnVoiceHangUP.Visible = false;
            this.btnVoiceHangUP.Click += new System.EventHandler(this.btnVoiceHangUP_Click);
            // 
            // cmbExt
            // 
            this.cmbExt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExt.FormattingEnabled = true;
            this.cmbExt.Location = new System.Drawing.Point(95, 32);
            this.cmbExt.Name = "cmbExt";
            this.cmbExt.Size = new System.Drawing.Size(172, 21);
            this.cmbExt.TabIndex = 6;
            this.cmbExt.SelectedIndexChanged += new System.EventHandler(this.cmbExt_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Справочник:";
            // 
            // TextBox
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(279, 132);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbExt);
            this.Controls.Add(this.btnVoiceHangUP);
            this.Controls.Add(this.checkVoice);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbPhone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TextBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Введите номер";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbPhone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        public System.Windows.Forms.CheckBox checkVoice;
        private System.Windows.Forms.Button btnVoiceHangUP;
        private System.Windows.Forms.ComboBox cmbExt;
        private System.Windows.Forms.Label label2;

    }
}