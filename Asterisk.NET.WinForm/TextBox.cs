using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LogUtils;

namespace Asterisk.NET.WinForm
{
    public partial class TextBox : Form
    {
        public String Phone = "";
        public String ParkExt = "";
        FormMain.Data _Owner = null;
        public TextBox(String parkExt="", FormMain.Data Owner = null)
        {
            InitializeComponent();
            ParkExt = parkExt;
            _Owner = Owner;

            foreach (var it in _Owner._Owner.extnames)
                cmbExt.Items.Add(it);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbPhone.Text != "" && _Owner.isOriginate == false)
            {
                if (checkVoice.Checked) // originate voice
                {
                    _Owner._Owner.Originate(tbPhone.Text, "local");
                    lock (FormMain._lock)
                    {
                        //_Owner.isOriginate = true;
                        //_Owner.OriginateUID = tbPhone.Text;
                        _Owner.isVoiceCall = true;
                    }
                }
                else
                {
                    Phone = tbPhone.Text;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
            tbPhone.Enabled = !checkVoice.Checked;
            cmbExt.Enabled = !checkVoice.Checked;
            tbPhone.Text = checkVoice.Checked ? "331" : "";
            btnVoiceHangUP.Visible = checkVoice.Checked;
        }

        private void btnVoiceHangUP_Click(object sender, EventArgs e)
        {
            if (_Owner._Owner.bShowWarningBeforeHangUp)
            {
                if (System.Windows.Forms.DialogResult.Yes ==
                    MessageBox.Show("Завершить текущий вызов?", "HangUp btnVoiceHangUP_Click", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                {
                    _Owner._Owner.HangUp(_Owner.VoiceChannel);
                }
            }
            else
            {
                _Owner._Owner.HangUp(_Owner.VoiceChannel);
            }

            lock (FormMain._lock)
            {
                 _Owner.isVoiceCall = false;
                 _Owner.VoiceChannel = "";
            }
        }

        private void cmbExt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbExt.SelectedIndex != -1)
            {
                tbPhone.Text = _Owner._Owner.extnumbers[cmbExt.SelectedIndex];
            }
        }
    }
}
