using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using LogUtils;

/*
     id
	UniqueID
	Number
	NumberExt
	ClientID
	Comment
	tIncomingCall
	tBeginCall
	tEndCall
 */
namespace Asterisk.NET.WinForm
{
    public partial class Calling : Form
    {
        public bool isClose = false;
        FormMain.Data _Owner;
        //bool fMinimized = false;
        //bool fCommentView = true;
        bool bIsLastStep = false;
        bool bIsComentFill = false;
        string textComment = "";
        System.Windows.Forms.TextBox textbox = null;


        public class ButttonInfoStep : FormMain.ButtonInfo
        {
            // currently selected id(value) item from combobox with id(key)
            public Dictionary<long, long> m_vSelectedGroups = new Dictionary<long, long>();
            public ButttonInfoStep(FormMain.ButtonInfo data)
            {
                m_id = data.m_id;
                m_name = data.m_name;
                m_pid = data.m_pid;
                m_vGroups = data.m_vGroups;
            }
        };
        List<ButttonInfoStep> vButtonSteps = new List<ButttonInfoStep>();

        const int buttonWidth =200;// 115; // 507 panel
        const int buttonHeight = 26;
        const int buttonOffsetX = 6;
        const int buttonOffsetY = 6;
        public Calling(FormMain.Data mOwner, Bitmap bmpDlg)
        {
            using (new StackTracer("Calling", new object[] { mOwner, bmpDlg }))
            {
                InitializeComponent();
                _Owner = mOwner;
                cmbGroups.SelectedIndex = _Owner.GroupId >= 0 && _Owner.GroupId < cmbGroups.Items.Count ? _Owner.GroupId : 0;
                /*
                 * FormMain.NewMobileEuromedClinic
                    _Owner.NewPhoneEuromedClinic
                    _Owner.NewEuromedInVitro
                */
                //PatientGroup.Text = (_Owner.GroupId == 4 || _Owner.GroupId == 5 || _Owner.GroupId == 9) ? "" : FormMain.NameGroups[_Owner.GroupId];
                FIO.Text = string.IsNullOrEmpty(_Owner.sFIO) ? FormMain.FormatNumber(_Owner.Number) : _Owner.sFIO;
                Text = "Звонок от " + FormMain.FormatNumber(_Owner.Number) + " на " + FormMain.FormatNumber(_Owner.NumberReceiv);
                Note.Text = (DateTime.MinValue != _Owner.dtBirthDay ? _Owner.dtBirthDay.ToShortDateString() + " г.р.\n" : "") +
                    _Owner.Comment;

                /*string[] answers = _Owner.GetListAnswer(cmbGroups.SelectedIndex);
                cmbAnswers.Items.Clear();
                if (answers != null)
                {
                    foreach (var it in answers)
                    {
                        cmbAnswers.Items.Add(it);
                    }
                }*/

                timerClock.Start();
                if (bmpDlg != null)
                    head.BackgroundImage = bmpDlg;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            using (new StackTracer("OnResize", new object[] { e }))
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    Close();
                }
            }
        }

        private void Calling_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (new StackTracer("Calling_FormClosing", new object[] { sender, e }))
            {
                // если закрываем окно
                if (this.WindowState != FormWindowState.Minimized)
                {
                    if (!bIsComentFill && cmbGroups.SelectedIndex != 15 && !isClose)
                    {
                        //MessageBox.Show("Укажите причину звонка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        setCommentState();
                        
                        /*if (_Owner.Comment == "" && DialogResult.No ==
                            MessageBox.Show("Оставить звнонок без комментария?", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                        {
                            e.Cancel = true;
                            setCommentState();
                            tbNote.Focus();
                            tbNote.SelectAll();
                        }
                        else //if (_Owner.Comment != "") // if exists commen
                        */
                    }
                    else
                    {
                        _Owner.wndShow = false;
                        //if (_Owner.tEndCall != DateTime.MinValue) // if end call
                        {
                            lock (FormMain._lock)
                            {
                                _Owner.is_del = true;
                            }
                        }
                    }
                    /*else
                    {
                        //MessageBox.Show("Не указан комментарий к звонку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        setCommentState();
                        tbNote.Focus();
                        tbNote.SelectAll();
                    }*/
                }
                else
                    _Owner.wndShow = false;
            }
        }

        private void Calling_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        void setCommentState(bool state = true)
        {
            using (new StackTracer("setCommentState", new object[] { state }))
            {
                try
                {
                    groupBoxInfo.Visible = !state;
                    groupBoxActions.Visible = state;

                    /*PatientGroup.Text = state ? "Добавьте коментарии и группу:" : FormMain.NameGroups[_Owner.GroupId];
                    FIO.Visible = !state;
                    Note.Visible = !state;
                    CallState.Visible = !state;
                    btnHold.Visible = !state;
                    btnAddToGroup.Visible = !state;
                    btnRedirect.Visible = !state;
                    //btnAnswer.Enabled = state;
                    btnAnswer.Text = state ? "Отменить" : "Завершить";
                    tbNote.Visible = state;
                    cmbGroups.Visible = state;
                    cmbAnswers.Visible = state;
                    btnAddComment.Text = state ? "   Сохранить" : "   + Комментарий";*/
                    if (state)
                    {
                        vButtonSteps.Clear();
                        panelButtons.Controls.Clear();
                        //tbNote.Text = Note.Text.Replace(_Owner.dtBirthDay.ToShortDateString() + " г.р.\n", "");
                        // delete prev current comment
                        /*if (!string.IsNullOrEmpty(tbNote.Text))
                            tbNote.Text = tbNote.Text.Replace("\n" + tbNote.Text, "");*/

                        // show root buttons and set handler
                        List<FormMain.ButtonInfo> results = _Owner._Owner.m_vButtons.FindAll(
                            delegate(FormMain.ButtonInfo item)
                            {
                                return item.m_pid == 0;
                            }
                        );
                        int i = 0;
                        foreach (FormMain.ButtonInfo button in results)
                        {
                            ShowButton(button, i++);
                        }
                        btnBack.Enabled = true;
                        btnSave.Enabled = false;
                    }
                    else // вышли из режима добавления коментариев
                    {
                        btnBack.Enabled = false;
                        btnSave.Enabled = false;
                        
                        Note.Text = //(DateTime.MinValue != _Owner.dtBirthDay ? _Owner.dtBirthDay.ToShortDateString() + " г.р.\n" : "") + tbNote.Text;
                                    "\n" + textComment;
                        lock (FormMain._lock)
                        {
                            _Owner.GroupId = cmbGroups.SelectedIndex;
                            PatientGroup.Text = FormMain.NameGroups[_Owner.GroupId];

                            _Owner.Comment = textComment;
                            /*if (cmbAnswers.SelectedIndex >= 0)
                            {
                                _Owner.Answer = cmbAnswers.GetItemText(cmbAnswers.Items[cmbAnswers.SelectedIndex]);
                            }*/

                            // _Owner.Comment = _Owner.Comment.Replace(_Owner.dtBirthDay.ToShortDateString() + " г.р.\n", "");
                        }
                        OdbcCommand DbCommand = new OdbcCommand("update Calls set Comment='" + _Owner.Comment + "', GroupID = " +
                            _Owner.GroupId + ", Answer='" + _Owner.Answer + "' where id = " + _Owner.CallID.ToString(), FormMain.DbConnection);
                        DbCommand.ExecuteNonQuery();
                        if (bIsComentFill || _Owner.GroupId == FormMain.Staff || _Owner.GroupId == FormMain.Boss)
                            //Calling_FormClosing(this, null);
                            Close();
                    }
                }
                catch (Exception ex)
                {
                    Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                }
            }
        }

        private void btnAddComment_Click(object sender, EventArgs e)
        {
            using (new StackTracer("btnAddComment_Click", new object[] { sender, e }))
            {
                /*if (fCommentView)
                {
                    setCommentState(false);
                }
                else*/
                // show comment dialog
                setCommentState();
            }
        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            using (new StackTracer("btnAnswer_Click", new object[] { sender, e }))
            {
                /*if (tbNote.Visible)
                {
                    tbNote.Text = "";
                    setCommentState(false);
                }
                else*/
                {
                    lock (FormMain._lockNewChannel)
                    {
                        // incoming call 
                        /*if (_Owner.tIncomingCall != DateTime.MinValue &&
                             _Owner.tBeginCall == DateTime.MinValue &&
                             _Owner.tEndCall == DateTime.MinValue)
                        {
                        

                            // make this call - is my receipt call or false
                            OdbcCommand dbCommand = new OdbcCommand("update Calls set agentID = '" + _Owner._Owner.aThisAgentID +
                                    "' where (agentID='' or agentID is null) and UniqueID='" + _Owner.EventData.UniqueId + "'", FormMain.DbConnection);
                            if (dbCommand.ExecuteNonQuery() > 0)
                            {
                                _Owner._Owner.AnswerCall(_Owner);
                            }
                            else
                            {
                                MessageBox.Show("Вызов принят другим оператором.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            btnAnswer.Text = "Завершить";

                        }
                        else*/
                        /*if (_Owner.tIncomingCall != DateTime.MinValue &&
                      _Owner.tBeginCall != DateTime.MinValue &&
                      _Owner.tEndCall == DateTime.MinValue)*/
                        if (_Owner.IsRedirected == false)
                        {
                            if (_Owner._Owner.bShowWarningBeforeHangUp)
                            {
                                if (System.Windows.Forms.DialogResult.Yes ==
                                    MessageBox.Show("Завершить текущий вызов?", "HangUp btnAnswer_Click", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                                {
                                    _Owner._Owner.HangUp(_Owner.EventData.Channel);
                                }


                            }
                            else
                            {
                                _Owner._Owner.HangUp(_Owner.EventData.Channel);
                            }

                        }
                        Close();
                    }
                }
            }
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            try
            {
                /*string btnName = btnHold.Text.IndexOf("   UNHOLD") == 0 ? "   UNHOLD" : "   HOLD";
                if (_Owner.ParkExt != "")// && btnName == "   UNHOLD")
                {
                    btnHold.Text += " [" + _Owner.ParkExt + "]";
                }*/

                if (_Owner.tEndCall == DateTime.MinValue && _Owner.tBeginCall == DateTime.MinValue)
                {
                    CallState.Text = "Входящий звонок";
                }
                else if (_Owner.tEndCall == DateTime.MinValue && _Owner.tBeginCall != DateTime.MinValue)
                {
                    TimeSpan ts = DateTime.Now - _Owner.tBeginCall;
                    CallState.Text = "Идет разговор " + string.Format("{0:00}:{1:00}:{2:00}", ts.TotalHours, ts.Minutes, ts.Seconds);
                }
                else if (_Owner.tEndCall != DateTime.MinValue && _Owner.tIncomingCall != DateTime.MinValue && _Owner.tBeginCall == DateTime.MinValue)
                {
                    CallState.Text = "Разговор отменен";
                }
                else
                {
                    TimeSpan ts = _Owner.tEndCall - _Owner.tBeginCall;
                    CallState.Text = "Разговор закончен " + string.Format("{0:00}:{1:00}:{2:00}", ts.TotalHours, ts.Minutes, ts.Seconds); ;
                    timerClock.Stop();
                }
            }
            catch (Exception ex)
            {
                Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
            }
        }

        private void cmbGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bIsLastStep)
            {
                btnSave.Enabled = true;
            }

            try
            {

                lock (FormMain._lock)
                {
                    _Owner.GroupId = cmbGroups.SelectedIndex;
                }

                OdbcCommand DbCommand = new OdbcCommand(
                    "update Calls set GroupID = " +
                    _Owner.GroupId.ToString() + 
                    " where id = " + _Owner.CallID.ToString(), 
                    FormMain.DbConnection);
                DbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
            }

            /*string[] answers = _Owner.GetListAnswer(cmbGroups.SelectedIndex);
            cmbAnswers.Items.Clear();
            if (answers != null)
            {
                foreach (var it in answers)
                {
                    cmbAnswers.Items.Add(it);
                }
            }
            else
                cmbAnswers.Enabled = false;*/
        }

        private void btnRedirect_Click(object sender, EventArgs e)
        {

            using (new StackTracer("FormMain_Shown", new object[] { sender, e }))
            {
                try
                {
                    if (_Owner.OriginateChannel != "")
                    {
                        //_Owner._Owner.Bridge(_Owner.ParkChannel, _Owner.OriginateChannel);
                        Tracer.Instance.TraceWL3("WARNING: Use button for redirect");
                        MessageBox.Show("Используйте кнопку переадресация", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        TextBox dlg = new TextBox("", _Owner);
                        if (DialogResult.OK == dlg.ShowDialog(this))
                        {
                            btnAnswer.Enabled = false;
                            lock (FormMain._lockADB)
                            {
                                _Owner._Owner.usersPhone.Clear();
                                _Owner._Owner.usersAgentID.Clear();
                                _Owner._Owner.DbCommand1.CommandText = "select name, agentphone, agentid from queue_agents where state = 'in' order by name";
                                _Owner._Owner.DbReader1 = _Owner._Owner.DbCommand1.ExecuteReader();
                                while (_Owner._Owner.DbReader1.Read())
                                {
                                    _Owner._Owner.usersPhone.Add(_Owner._Owner.DbReader1.GetInt32(1).ToString());
                                    _Owner._Owner.usersAgentID.Add(_Owner._Owner.DbReader1.GetInt32(2).ToString());
                                }
                                _Owner._Owner.DbReader1.Close();
                            }

                            string ext = dlg.Phone;
                            _Owner._Owner.Redirect(_Owner.EventData.Channel, ext);
                            _Owner.IsRedirected = true;
                            foreach (var numb in _Owner._Owner.usersPhone)
                            {
                                if (ext == numb)
                                {
                                    //isClose = true;
                                    lock (FormMain._lock)
                                    {
                                        _Owner.is_del = true;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                }
            }
        }

        /*private void btnHold_Click(object sender, EventArgs e)
        {
            using (new StackTracer("btnHold_Click", new object[] { sender, e }))
            {
                if (_Owner.IsParked)
                {
                    Tracer.Instance.TraceWL3("ACTION: Hold");
                    //btnHold.Text = "   HOLD";
                    _Owner._Owner.Originate(_Owner.ParkExt);
                    _Owner.IsParked = false;
                    _Owner.ParkExt = "";
                    _Owner.ParkChannel = "";
                }
                else
                {
                    Tracer.Instance.TraceWL3("ACTION: UnHold");
                    //btnHold.Text = "   UNHOLD";
                    _Owner._Owner.Park(_Owner.EventData.Channel, _Owner.EventData.Channel);//"SIP/" + _Owner._Owner.aThisExt);
                    _Owner.IsParked = true;
                }
            }
        }*/

       /* private void btnAddToGroup_Click(object sender, EventArgs e)
        {
            using (new StackTracer("btnAddToGroup_Click", new object[] { sender, e }))
            {
                if (_Owner.OriginateChannel != "")// if exists internal call then - direct brigde
                {
                    Tracer.Instance.TraceWL3("ACTION: Bridge ParkChannel: {0}, OrigChannel - {1}",
                        _Owner.ParkChannel, _Owner.OriginateChannel);
                    _Owner._Owner.Bridge(_Owner.ParkChannel, _Owner.OriginateChannel);
                    return;
                }
                if (_Owner.isOriginate)
                {
                    Tracer.Instance.TraceWL3("ACTION: isOriginate -> return");
                    return;
                }
                // текущий вызов на hold
                if (!_Owner.IsParked)
                {
                    Tracer.Instance.TraceWL3("ACTION: isParked -> btnHold_Click");
                    btnHold_Click(null, null);
                }
                // call to 
                TextBox dlg = new TextBox(_Owner.ParkExt, _Owner);
                if (DialogResult.OK == dlg.ShowDialog(this))
                {
                    btnAnswer.Enabled = false;
                    Tracer.Instance.TraceWL3("ACTION: Originate");
                    _Owner._Owner.Originate(dlg.Phone, (dlg.checkVoice.Checked ? "local" : "world"));
                    lock (FormMain._lock)
                    {
                        _Owner.isOriginate = true;
                        _Owner.OriginateUID = dlg.Phone;
                    }
                }
            }
        }
        */
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!bIsLastStep)
                {
                    return;
                }

                textComment = textbox.Text;
                
                // clear perevious data from db
                OdbcCommand DbCommand = new OdbcCommand("delete from CallStatLogs where messid = " + _Owner.CallID, FormMain.DbConnection);
                DbCommand.ExecuteNonQuery();

                int i = 0;
                bool hasSavedSpam = false;
                // save data to db
                foreach (var buttonStep in vButtonSteps)
                {
                    i++;



                    if (!hasSavedSpam && buttonStep.m_id == 17)
                    {
                        hasSavedSpam = true;
                        OdbcCommand DbCommand_in = new OdbcCommand(
                                "insert into phonebook(typename, name, extnumber) values ('СПАМ', '" 
                                    + textComment + "','"
                                    + _Owner.Number + "')"
                                 , FormMain.DbConnection);
                        DbCommand_in.ExecuteNonQuery();
                    }


                    if (buttonStep.m_vSelectedGroups.Count == 0)
                    {
                        OdbcCommand DbCommand_in = new OdbcCommand("insert into CallStatLogs (messid, buttonid, listid, comment) values ("
                            + _Owner.EventData.UniqueId + ","
                            + buttonStep.m_id.ToString() + ",0,'"
                            + (i == vButtonSteps.Count ? textComment : "")
                            + "')", FormMain.DbConnection);
                        DbCommand_in.ExecuteNonQuery();
                    }
                    else
                    {
                        foreach (var selectGroup in buttonStep.m_vSelectedGroups)
                        {
                            
                            OdbcCommand DbCommand_in = new OdbcCommand("insert into CallStatLogs (messid, buttonid, listid, comment) values ("
                            + _Owner.EventData.UniqueId + ","
                            + buttonStep.m_id.ToString() + ","
                            + selectGroup.Value.ToString() +  ",'"
                            + (i == vButtonSteps.Count ? textComment : "")
                            + "')", FormMain.DbConnection);
                            DbCommand_in.ExecuteNonQuery();
                        }
                    }

                }

                // clear all control
                vButtonSteps.Clear();
                panelButtons.Controls.Clear();

                // set state filling comment 
                bIsComentFill = true;
                setCommentState(false);
            }
            catch (Exception ex)
            {
                Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
            }
        }

        // click on dynamic added Buttons
        private void btnComment_Click(object sender, EventArgs e)
        {
            // add sender to vector steps
            Button obj = (Button)sender;
            ButttonInfoStep step = new ButttonInfoStep((FormMain.ButtonInfo)obj.Tag);
            vButtonSteps.Add(step);
            bIsLastStep = true;
            int i = 0;
            // show extended info - combobox(es)
            if (step.m_vGroups.Count > 0)
            {
                foreach (FormMain.GroupsInfo cmb in step.m_vGroups)
                {
                    ShowCombobox(step, cmb, i++);
                    bIsLastStep = false;
                }
            }
            // show next buttons (with pid == current button id)
            else
            {
                List<FormMain.ButtonInfo> results = _Owner._Owner.m_vButtons.FindAll(
                            delegate(FormMain.ButtonInfo item)
                            {
                                return item.m_pid == step.m_id;
                            }
                        );
                foreach (FormMain.ButtonInfo button in results)
                {
                    ShowButton(button, i++);
                    bIsLastStep = false;
                }
            }

            // if last step
            if (bIsLastStep)
            {
                panelButtons.Controls.Clear();
                btnSave.Enabled = true;
                textbox = new System.Windows.Forms.TextBox();
                textbox.Multiline = true;
                textbox.Location = new Point(5, 5);
                textbox.Size = new Size(panelButtons.Size.Width - 10, panelButtons.Size.Height - 10);
                textbox.Visible = true;
                panelButtons.Controls.Add(textbox);
            }
        }

        // selection event on dynamic added Combobox
        private void cmbSelectedIndexChanged(object sender, EventArgs e)
        {
            // check selection on all comboboxes in panel
            bool bIsAllSelected = true;
            foreach (Control ctrl in panelButtons.Controls)
            {
                if (ctrl is ComboBox)
                {
                    ComboBox cmb = (ComboBox)ctrl;
                    bIsAllSelected = bIsAllSelected && (cmb.SelectedIndex != -1 || cmb.Items.Count == 0);
                }
            }

            // if all selected -> show neext buttons
            if (bIsAllSelected)
            {
                FormMain.ButtonInfo buttonInfo = null; // parent button for all comboboxes
                // add selected id (in all cmb) in current step
                foreach (Control ctrl in panelButtons.Controls)
                {
                    if (ctrl is ComboBox)
                    {
                        ComboBox cmb = (ComboBox)ctrl;
                        Dictionary<FormMain.ButtonInfo, FormMain.GroupsInfo> data = (Dictionary<FormMain.ButtonInfo, FormMain.GroupsInfo>)cmb.Tag;
                        // data contrain only one element
                        foreach (var pair in data)
                        {
                            buttonInfo = pair.Key;
                            FormMain.GroupsInfo groupInfo = pair.Value;
                            long groupInfoItemID = -1;
                            if (groupInfo.m_vGroupsList.Count > 0 && cmb.SelectedIndex < groupInfo.m_vGroupsList.Count)
                            {
                                FormMain.GroupsInfoItem groupInfoItem = groupInfo.m_vGroupsList[cmb.SelectedIndex];
                                groupInfoItemID = groupInfoItem.m_id;
                            }

                            if (vButtonSteps.Count > 0)
                            {
                                vButtonSteps[vButtonSteps.Count - 1].m_vSelectedGroups.Add(groupInfo.m_id, groupInfoItemID);
                            }
                        }
                        
                    }
                }
                // show next buttons
                bIsLastStep = true;
                if (buttonInfo != null)
                {
                    int i = 0;
                    List<FormMain.ButtonInfo> results = _Owner._Owner.m_vButtons.FindAll(
                            delegate(FormMain.ButtonInfo item)
                            {
                                return item.m_pid == buttonInfo.m_id;
                            }
                        );
                    foreach (FormMain.ButtonInfo button in results)
                    {
                        ShowButton(button, i++);
                        bIsLastStep = false;
                    }
                }

                // if last step
                if (bIsLastStep)
                {
                    panelButtons.Controls.Clear();
                    btnSave.Enabled = true;
                    textbox = new System.Windows.Forms.TextBox();
                    textbox.Multiline = true;
                    textbox.Location = new Point(5, 5);
                    textbox.Size = new Size(panelButtons.Size.Width - 10, panelButtons.Size.Height - 10);
                    textbox.Visible = true;
                    panelButtons.Controls.Add(textbox);
                }
            }
        }

        private Point CalcButtonLocation(int index)
        {
            bool bIsEven = (0 == ((index + 1) % 2));
            int line = Convert.ToInt32(Math.Floor(Convert.ToDouble(index) / 2.0));

            return new Point(panelButtons.Size.Width / 2 + (bIsEven ? buttonOffsetX : (-buttonWidth - buttonOffsetX)),
                             line * (buttonHeight + buttonOffsetY));
        }

        private void ShowButton(FormMain.ButtonInfo button, int index)
        {
            // hide previous controls
            if (index == 0)
            {
                panelButtons.Controls.Clear();
            }

            

            Size size = new Size(buttonWidth, buttonHeight);
            Point point = CalcButtonLocation(index);
            // show button
            Button btn = new Button();
            btn.Text = button.m_name;
            btn.Tag = button;
            btn.Size = size;
            btn.Location = point;
            btn.Enabled = true;
            btn.Visible = true;
            btn.Click += new EventHandler(btnComment_Click);
            panelButtons.Controls.Add(btn);
        }

        private void ShowCombobox(FormMain.ButtonInfo parentButton, FormMain.GroupsInfo combobox, int index)
        {
            // hide previous controls
            if (index == 0)
            {
                panelButtons.Controls.Clear();
            }
            Size size = new Size(buttonWidth, buttonHeight);
            Point point = CalcButtonLocation(index);

            // show combobox
            Label lbl = new Label();
            ComboBox cmb = new ComboBox();
            //btn.Text = button.m_name;
            Dictionary<FormMain.ButtonInfo, FormMain.GroupsInfo> data = new Dictionary<FormMain.ButtonInfo, FormMain.GroupsInfo>();
            data.Add(parentButton, combobox);
            cmb.Tag = data;
            cmb.Size = size;
            cmb.Location = point;
            cmb.Enabled = true;
            cmb.Visible = true;
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb.Sorted = false;
            cmb.SelectedIndexChanged += new EventHandler(cmbSelectedIndexChanged);
            // fill combobox
            foreach (FormMain.GroupsInfoItem item in combobox.m_vGroupsList)
            {
                cmb.Items.Add(item.m_name);
            }

            panelButtons.Controls.Add(cmb);

            // name combobox
            Label label = new Label();
            label.Text = combobox.m_name + ":";
            label.Location = new Point(point.X - label.Size.Width - 5, point.Y + 2);
            label.Visible = true;
            panelButtons.Controls.Add(label);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // if first step
            if (vButtonSteps.Count == 0)
            {
                bIsComentFill = false;
                setCommentState(false);
                return;
            }

            // other steps
            ButttonInfoStep last_step = vButtonSteps[vButtonSteps.Count - 1];

            // if showed comboboxes 
            if (last_step.m_vSelectedGroups.Count > 0)
            {
                if (last_step.m_vGroups.Count > 0)
                {
                    int i = 0;
                    foreach (FormMain.GroupsInfo cmb in last_step.m_vGroups)
                    {
                        ShowCombobox(last_step, cmb, i++);
                    }
                    last_step.m_vSelectedGroups.Clear();
                }
            }
            // if showed buttons
            else
            {
                // show prev element:buttons
                int i = 0;
                List<FormMain.ButtonInfo> results = _Owner._Owner.m_vButtons.FindAll(
                            delegate(FormMain.ButtonInfo item)
                            {
                                return item.m_pid == last_step.m_pid;
                            }
                        );
                foreach (FormMain.ButtonInfo button in results)
                {
                    ShowButton(button, i++);
                }
                // delete last element
                vButtonSteps.RemoveAt(vButtonSteps.Count - 1);
            }
        }

        private void panelButtons_MouseEnter(object sender, EventArgs e)
        {
            panelButtons.Focus();
        }
   
    }
}
