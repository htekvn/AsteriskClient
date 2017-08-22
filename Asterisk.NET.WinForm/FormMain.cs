using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using Asterisk.NET.Manager;
using Asterisk.NET.Manager.Event;
using System.Diagnostics;
using Gajatko.IniFiles;
using System.Data.Odbc;
using System.IO;
using System.Media; 

using System.Net;
using System.Net.Sockets;

//using System.Diagnostics;


using CustomUIControls;

using LogUtils;




namespace Asterisk.NET.WinForm
{
	public partial class FormMain : Form
    {
        #region TypeClients && GroupInfo

        public class GroupsInfoItem
        {
            public long m_id;
            public string m_name;
        }

        public class GroupsInfo
        {
            public long m_id;
            public string m_name;
            public List<GroupsInfoItem> m_vGroupsList = new List<GroupsInfoItem>();
        }

        public class ButtonInfo
        {
            public long m_id;
            public long m_pid;
            public string m_name;
            public List<GroupsInfo> m_vGroups = new List<GroupsInfo>();
        }

        public const int EuromedClinic = 1;      //1) Ïàöèåíò Euromed Clinic – âñå ïàöèåíòû, êîòîðûå âíåñåíû â áàçó “ïàöèåíòû” â èíòðàíåòå, êðîìå íèæåóêàçàííûõ òå êîòîðûõ ÿ âûáèðàþ ñåé÷àñ íî íå VIP
        public const int EuromedInVitro = 2;         //2) Ïàöèåíò Euromed In Vitro – âñå ïàöèåíòû, âíåñåííûå â áàçó “ïàöèåíòû” â èíòðàíåòå, êîòîðûå çàíåñåíû â ðàçäåë “Ñ÷åòà-ïðîãðàìû”. Èëè áûëè âíåñåíû òóäà ðàíåå (íà íàèõ áûëè çàêðûòû êàêèå-òî ïðîãðàììû). Ïëþñ ïàöèåíòû, êîòîðûå âíåñåíû ê òàêèì ïàöèåíòêàì â àíêåòó â êà÷åñòâå ñóïðóãîâ.
        public const int VIP = 3;                   //3) VIP ïàöèåíò - âñå ïàöèåíòû, êîòîðûå âíåñåíû â áàçó “ïàöèåíòû” â èíòðàíåòå è ó íèõ â àíêåòå óêàçàíî, ÷òî îíè VIP òå êîòîðûõ ÿ âûáèðàþ ñåé÷àñ VIP
        public const int NewMobileEuromedClinic = 4; //4) Íîâûé ìîáèëüíûé Euromed Clinic – âñå çâîíêè, êîòîðûå íå èäåíòèôèöèðîâàíû ïî èñòî÷íèêó è ïîñòóïàþò íà íîìåð 327 03 01 ñ ìîáèëüíîãî
        public const int NewPhoneEuromedClinic = 5;  //5) Íîâûé ãîðîäñêîé Euromed Clinic – âñå çâîíêè, êîòîðûå íå èäåíòèôèöèðîâàíû ïî èñòî÷íèêó è ïîñòóïàþò íà íîìåð 327 03 01 ñ ãîðîäñêîãî
        public const int NewOtherCityEuromedClinic = 6;  //6) Íîâûé èíîãîðîäíèé Euromed Clinic – âñå çâîíêè èç äðóãèõ ðåãèîíîâ ÐÔ, ïîñòóïàþùèå íà íîìåð 327 03 01
        public const int NewOtherCityEuromedInVitro = 7; //7) Íîâûé èíîãîðîäíèé Euromed In Vitro – âñå çâîíêè èç äðóãèõ ðåãèîíîâ ÐÔ, ïîñòóïàþùèå íà íîìåð 438 03 43
        public const int NewOtherLand = 8;           //8) Íîâûé ìåæäóíàðîäíûé – âñå çâîíêè, ïîñòóïàþùèå èç-çà ãðàíèöû íà ëþáîé íîìåð òîæå ïîíÿòíî - ÿ òàê ïîíÿë íîìåðà íå äîëæíû íà÷èíàòüñÿ â 8-êè
        public const int NewEuromedInVitro = 9;      //9) Íîâûé Euromed In Vitro – âñå çâîíêè, êîòîðûå íå èäåíòèôèöèðîâàíû ïî èñòî÷íèêó è ïîñòóïàþò íà íîìåð 438 03 43
        public const int NewEuromedExpress = 10;      //10) Íîâûé Euromed Åxpress – âñå çâîíêè, êîòîðûå íå èäåíòèôèöèðîâàíû ïî èñòî÷íèêó è ïîñòóïàþò íà íîìåð 438 03 43
        public const int LPU = 11;                    //11) ËÏÓ – ïî ñïðàâî÷íèêó ïî phonebook?
        public const int InsuranceCompanyRU = 12;     //12) Ñòðàõîâàÿ êîìïàíèÿ – ïî ñïðàâî÷íèêó ïî phonebook?
        public const int InsuranceCompanyENG = 13;   //13) Insurance company – ïî ñïðàâî÷íèêó ïî phonebook?
        public const int Hotel = 14;                  //14) Ãîñòèíèöà – ïî ñïðàâî÷íèêó ïî phonebook?
        public const int Staff = 15;                  //15) Ñîòðóäíèê – ñîãëàñíî çàêëàäêè “ñîòðóäíèêè” íóæåí òâîé êîììåíò (òàáëèöà stuff?)
        public const int Boss = 16;
        public const int Spam = 17;

        public List<ButtonInfo> m_vButtons = new List<ButtonInfo>();
        
        public static string[] NameGroups = new string[] {
"Не определено",                // 0
"Пациент Euromed Clinic",
"Пациент Euromed In Vitro",
"VIP пациент",
"Новый мобильный",
"Новый городской",
"Новый иногородний", // Euromed Clinic",
"Новый иногородний", // Euromed In Vitro",
"Новый международный",
"Новый", // Euromed In Vitro",
"Новый", // Euromed Еxpress",
"ЛПУ",
"Страховая компания",
"Insurance company",
"Гостиница",
"Сотрудник", 
"Управляющий партнер"
};

        #endregion

        #region DataClients
        public class Data
        {
            public Data(string num, string numRec, int groupId, string FIO, DateTime birthday, int QueuePos, FormMain __Owner, int cid=0, string _Comment="") 
            {
                using (new StackTracer("Data", new object[] { num, numRec, groupId, FIO, birthday, QueuePos, __Owner, cid,_Comment }))
                {
                    _Owner = __Owner;
                    Number = num;
                    GroupId = groupId;
                    sFIO = FIO;
                    //dtRcv = DateTime.Now;
                    dtBirthDay = birthday;
                    NumberReceiv = numRec;
                    Index = QueuePos;
                    ClientID = cid;
                    Comment = _Comment;
                    Answer = "";
                    IsParked = false;
                    IsRedirected = false;
                }
            }

            public void ScrollCalls(int Delta)
            {
                //_Owner.ScrollCalls(Delta);
            }
            /*
            public string[] GetListAnswer(int iGroupId)
            {
                using (new StackTracer("GetListAnswer"))
                {
                    string[] Answers = null;
                    Answers = new string[12] {  "Çâîíîê ìåíåäæåðó",
                                                "Çâîíîê â ëàáîðàòîðèþ/àïòåêó",
                                                "Ïîëó÷åíèå èíôî îá óñëóãàõ",
                                                "Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                "Ïîëó÷åíèå ðåç-òîâ àíàëèçîâ",
                                                "Êîíñ-öèÿ äîêòîðà",
                                                "Âûçîâ/îòìåíà/îæèäàíèå ñêîðîé",
                                                "Çâîíîê ìåíåäæåðó",
                                                "Ñïàì",
                                                "Çâîíîê íå ñîñòîÿëñÿ",
                                                "Ôàêñ",
                                                "äðóãîå"};
    ///*
                    switch (iGroupId)
                    {
                        case EuromedClinic: Answers = new string[7] {"Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                            "Ïîëó÷åíèå ðåçóëüòàòîâ àíàëèçîâ",
                                                            "Êîíñóëüòàöèÿ äîêòîðà",
                                                            "Âûçîâ/îòìåíà/îæèäàíèå ñêîðîé",
                                                            "Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                    
                        case EuromedClinic: Answers = new string[7] {"Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                            "Ïîëó÷åíèå ðåçóëüòàòîâ àíàëèçîâ",
                                                            "Êîíñóëüòàöèÿ äîêòîðà",
                                                            "Âûçîâ/îòìåíà/îæèäàíèå ñêîðîé",
                                                            "Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        case EuromedInVitro: Answers = new string[7] {"Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                            "Ïîëó÷åíèå ðåçóëüòàòîâ àíàëèçîâ",
                                                            "Êîíñóëüòàöèÿ äîêòîðà",
                                                            "Âûçîâ/îòìåíà/îæèäàíèå ñêîðîé",
                                                            "Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        case VIP: Answers = new string[7] {"Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                            "Ïîëó÷åíèå ðåçóëüòàòîâ àíàëèçîâ",
                                                            "Êîíñóëüòàöèÿ äîêòîðà",
                                                            "Âûçîâ/îòìåíà/îæèäàíèå ñêîðîé",
                                                            "Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        case NewMobileEuromedClinic:  Answers = new string[9] {"Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                            "Ïîëó÷åíèå ðåçóëüòàòîâ àíàëèçîâ",
                                                            "Êîíñóëüòàöèÿ äîêòîðà",
                                                            "Âûçîâ/îòìåíà/îæèäàíèå ñêîðîé",
                                                            "Çâîíîê ìåíåäæåðó",
                                                            "Ñïàì",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        case NewPhoneEuromedClinic:  Answers = new string[9] {"Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                            "Ïîëó÷åíèå ðåçóëüòàòîâ àíàëèçîâ",
                                                            "Êîíñóëüòàöèÿ äîêòîðà",
                                                            "Âûçîâ/îòìåíà/îæèäàíèå ñêîðîé",
                                                            "Çâîíîê ìåíåäæåðó",
                                                            "Ñïàì",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        case NewOtherCityEuromedClinic: Answers = new string[7] {"Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Çâîíîê ìåíåäæåðó",
                                                            "Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                            "Êîíñóëüòàöèÿ äîêòîðà",
                                                            "Âûçîâ/îòìåíà/îæèäàíèå ñêîðîé",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        case NewOtherCityEuromedInVitro: Answers = new string[7] {"Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Çâîíîê ìåíåäæåðó",
                                                            "Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                            "Êîíñóëüòàöèÿ äîêòîðà",
                                                            "Âûçîâ/îòìåíà/îæèäàíèå ñêîðîé",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        case NewOtherLand: Answers = new string[7] {"Çâîíîê ïî êåéñó îò ÈíÑÊ",
                                                            "Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                            "Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Çâîíîê ìåíåäæåðó",
                                                            "Âûçîâ/îòìåíà/îæèäàíèå ñêîðîé",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        case NewEuromedInVitro: Answers = new string[8] {"Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                            "Ïîëó÷åíèå ðåçóëüòàòîâ àíàëèçîâ",
                                                            "Êîíñóëüòàöèÿ äîêòîðà",
                                                            "Çâîíîê ìåíåäæåðó",
                                                            "Ñïàì",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        case NewEuromedExpress: Answers = new string[12] {"Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Âûçîâ/îòìåíà/îæèäàíèå ñêîðîé",
                                                            "Çàêàç ìåäèöèíñêîé òðàíñïîðòèðîâêè",
                                                            "Çàêàç áðèãàäû ñêîðîé ïîìîùè íà ìåðîïðèÿòèå",
                                                            "Íàïðàâëåíèå ïàðòíåðàìè ïàöèåíòà íà ãîñïèòàëèçàöèþ",
                                                            "Ïåðåíàïðàâëåííûé âûçîâ ñêîðîé îò ïàðòíåðîâ",
                                                            "Ïîëó÷åíèå ðåçóëüòàòîâ àíàëèçîâ",
                                                            "Êîíñóëüòàöèÿ äîêòîðà",
                                                            "Çâîíîê ìåíåäæåðó",
                                                            "Ñïàì",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        case LPU: Answers = new string[6] {"Ïîëó÷åíèå/ïðåäîñòàâëåíèå èíôîðìàöèè ïî ïàöèåíòó",
                                                            "Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                            "Çâîíîê ìåíåäæåðó",
                                                            "Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        case InsuranceCompanyRU: Answers = new string[7] {"Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                            "Ïîäòâåðæäåíèå/ñîãëàñîâàíèå ëå÷åíèÿ",
                                                            "Ïîëó÷åíèå ðåçóëüòàòîâ àíàëèçîâ",
                                                            "Êîíñóëüòàöèÿ äîêòîðà",
                                                            "Çâîíîê ìåíåäæåðó",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        case InsuranceCompanyENG: Answers = new string[6] {"Çâîíîê ïî êåéñó",
                                                            "Çàïèñü/îòìåíà/ïåðåíîñ âèçèòà",
                                                            "Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Çâîíîê ìåíåäæåðó",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        case Hotel: Answers = new string[9] {"Âûçîâ/îòìåíà/îæèäàíèå ñêîðîé",
                                                            "Ïîëó÷åíèå èíôîðìàöèè ïî ïàöèåíòó",
                                                            "Êîíñóëüòàöèÿ äîêòîðà",
                                                            "Çâîíîê ìåíåäæåðó",
                                                            "Çàêàç êðåñëà-êàòàëêè",
                                                            "Çàêàç òðåíèíãà ïî îêàçàíèþ ïåðâîé ïîìîùè",
                                                            "Ïîëó÷åíèå èíôîðìàöèè îá óñëóãàõ è öåíàõ",
                                                            "Çâîíîê íå ñîñòîÿëñÿ",
                                                            "Äðóãîå"}; break;
                        //case Staff: string[] Answers = null; break;

                    }*-/
                    return Answers;
                }
            }
    */
            public void CreatetbCalling() // !!!! call only from basic thread !!!!
            {
                using (new StackTracer("CreatetbCalling"))
                {
                    try
                    {
                        Bitmap bmp = Properties.Resources.tb_client;
                        bmpDlg = Properties.Resources._1__Пациент_Euromed_Clinic;
                        switch (GroupId)
                        {
                            case EuromedClinic: bmp = Properties.Resources.tb_client; bmpDlg = Properties.Resources._2__Пациент_Euromed_In_Vitro; break;
                            case EuromedInVitro: bmp = Properties.Resources.tb_invitro; bmpDlg = Properties.Resources._1__Пациент_Euromed_Clinic; break;
                            case VIP: bmp = Properties.Resources.пациент_VIP; bmpDlg = Properties.Resources._3__Пациент_Euromed_Clinic_VIP; break;
                            case NewMobileEuromedClinic: bmp = Properties.Resources.новый_мобильный; bmpDlg = Properties.Resources._4__Новый_мобильный_Euromed_Clinic; break;
                            case NewPhoneEuromedClinic: bmp = Properties.Resources.новый_городской; bmpDlg = Properties.Resources._5__Новый_городской_Euromed_Clinic; break;
                            case NewOtherCityEuromedClinic: bmp = Properties.Resources.новый_иногородний; bmpDlg = Properties.Resources._6__Новый_иногородний_Euromed_Clinic; break;
                            case NewOtherCityEuromedInVitro: bmp = Properties.Resources.новый_иногородний_инвитро; bmpDlg = Properties.Resources._7__Новый_иногородний_Euromed_In_Vitro; break;
                            case NewOtherLand: bmp = Properties.Resources.новый_международный; bmpDlg = Properties.Resources._8__Новый_международный; break;
                            case NewEuromedInVitro: bmp = Properties.Resources.новый_инвитро; bmpDlg = Properties.Resources._9__Новый_Euromed_In_Vitro; break;
                            case NewEuromedExpress: bmp = Properties.Resources.экспресс; bmpDlg = Properties.Resources._10__Новый_Euromed_Express; break;
                            case LPU: bmp = Properties.Resources.tb_lpu; bmpDlg = Properties.Resources._11__ЛПУ; break;
                            case InsuranceCompanyRU: bmp = Properties.Resources.страховая_русская; bmpDlg = Properties.Resources._12__Страховая; break;
                            case InsuranceCompanyENG: bmp = Properties.Resources.страховая_иностранная; bmpDlg = Properties.Resources._13__Insurance; break;
                            case Hotel: bmp = Properties.Resources.tb_hotel; bmpDlg = Properties.Resources._14__Гостиница; break;
                            case Staff: bmp = Properties.Resources.сотрудник; bmpDlg = Properties.Resources._15__Сотрудник; break;
                            case Boss: bmp = Properties.Resources.сотрудник_sa; bmpDlg = Properties.Resources._15__Сотрудник_sa; break;
                            case Spam: bmp = Properties.Resources.tb_spam; bmpDlg = Properties.Resources.spam; break;
 
                        }
                        //int Index = vCalls.Count > 0 ? vCalls[vCalls.Count - 1].Index + 1 : 0;
                        taskbarCalling = new TaskbarNotifier(Index, this);
                        taskbarCalling.SetBackgroundBitmap(new Bitmap(bmp), Color.FromArgb(255, 0, 255));
#if LIGHT
#else
                        taskbarCalling.ContentClick += new EventHandler(ContentClick);
#endif

                        //wndCalling = new Calling(this);

                        /*
                         * taskbarNotifier1.ContentClickable=checkBoxContentClickable.Checked;
                    taskbarNotifier1.EnableSelectionRectangle=checkBoxSelectionRectangle.Checked;
                         * 
                         */
                    }
                    catch (Exception e)
                    {
                        Tracer.Instance.TraceWL3("Exception: {0}", e.Message);
                    }
                }
            }
            
            void ContentClick(object obj, EventArgs ea)
            {
                using (new StackTracer("CreatetbCaContentClicklling", obj, ea))
                {
                    try
                    {
                        if (!wndShow)
                        {
                            Calling wndCalling = new Calling(this, bmpDlg);
                            wndCalling.Show();
                            wndShow = true;
                            this.wndCalling = wndCalling;
                        }
                        else //if (wndCalling)
                            this.wndCalling.TopMost = true;

                        /* lock (_lockNewChannel)
                         {
                             // incoming call 
                             if (tIncomingCall != DateTime.MinValue &&
                                  tBeginCall == DateTime.MinValue &&
                                  tEndCall == DateTime.MinValue)
                             {
                        

                                 // make this call - is my receipt call or false
                                 OdbcCommand dbCommand = new OdbcCommand("update Calls set agentID = '" + _Owner.aThisAgentID + 
                                         "' where (agentID='' or agentID is null) and UniqueID='" + EventData.UniqueId + "'", DbConnection);
                                 if (dbCommand.ExecuteNonQuery() > 0)
                                 {
                                     _Owner.AnswerCall(this);
                                 }
                                 else
                                 {
                                     MessageBox.Show("Âûçîâ ïðèíÿò äðóãèì îïåðàòîðîì.", "Ïðåäóïðåæäåíèå", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                 }
                            

                             }
                         }*/
                        // incoming call 
                        if (tIncomingCall != DateTime.MinValue &&
                             tBeginCall == DateTime.MinValue &&
                             tEndCall == DateTime.MinValue)
                        {
                            /*if (_Owner.ActiveDialCall == null)
                                _Owner.AnswerCall(this);
                            else MessageBox.Show("Error: phone busy");*/

                            // make this call - is my receipt call or false
                            OdbcCommand dbCommand = new OdbcCommand("update Calls set agentID = '" + _Owner.aThisAgentID +
                                    "' where (agentID='' or agentID is null) and UniqueID='" + EventData.UniqueId + "'", DbConnection);
                            if (dbCommand.ExecuteNonQuery() > 0)
                            {
                                _Owner.AnswerCall(this);
                            }
                            else
                            {
                                MessageBox.Show("Вызов принят другим оператором.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            //btnAnswer.Text = "Çàâåðøèòü";

                        }
                    }
                    catch (Exception e)
                    {
                        Tracer.Instance.TraceWL3("Exception: {0}", e.Message);
                    }
                }
                    
            }

            public Color getGroupFontColor()
            {
                Color color = Color.WhiteSmoke;
                switch (GroupId)
                {
                    case EuromedClinic: color = Color.WhiteSmoke; break;
                    case EuromedInVitro: color = Color.WhiteSmoke; break;
                    case VIP: color = Color.WhiteSmoke; break;
                    case NewMobileEuromedClinic: color = Color.WhiteSmoke; break;
                    case NewPhoneEuromedClinic: color = Color.WhiteSmoke; break;
                    case NewOtherCityEuromedClinic: color = Color.WhiteSmoke; break;
                    case NewOtherCityEuromedInVitro: color = Color.WhiteSmoke; break;
                    case NewOtherLand: color = Color.WhiteSmoke; break;
                    case NewEuromedInVitro: color = Color.WhiteSmoke; break;
                    case NewEuromedExpress: color = Color.WhiteSmoke; break;
                    case LPU: color = Color.Black; break;
                    case InsuranceCompanyRU: color = Color.Black; break;
                    case InsuranceCompanyENG: color = Color.WhiteSmoke; break;
                    case Hotel: color = Color.Black; break;
                    case Staff: color = Color.Black; break;
                    case Boss: color = Color.DarkRed; break;   

                }

                return color;
            }

            public string NumberReceiv;
            public string Number;
            public int GroupId;
            public string sFIO;
            public DateTime tIncomingCall = DateTime.MinValue;
            public DateTime tBeginCall = DateTime.MinValue;
            public DateTime tEndCall = DateTime.MinValue;
            public DateTime dtBirthDay;
            public string Comment = "";
            public string Answer = "";
            public int Index;
            public int ClientID = 0;
            public int CallID = 0;
            public bool IsParked = false;
            public Bitmap bmpDlg = null;
            public string ParkExt = "";
            public FormMain _Owner = null;
            public bool IsRedirected = false;

            public bool is_del = false;

            public bool isJoined = false;
            public bool isOriginate = false;
            public bool isVoiceCall = false;
            public string OriginateUID = "";
            public string OriginateChannel = "";
            public string ParkChannel = "";
            public string VoiceChannel = "";

            public TaskbarNotifier taskbarCalling = null;
            public bool wndShow = false;
            public Calling wndCalling = null;
            public NewChannelEvent EventData = null;

        };
        #endregion

        #region FormMain Data Members


        string sDSN = "euromed";
        string sDSN1 = "asterisk";
        string aHost = "192.168.0.9";
        int aPort = 5038;
        string aLogin = "admin";//"intranet";
        string aPassw = "fkg7HM23Qy";
        public bool bShowWarningBeforeHangUp = false;

        public static OdbcConnection DbConnection = null;
        public static OdbcConnection DbConnection1 = null;
        OdbcCommand DbCommand = null;
        OdbcDataReader DbReader = null;
        OdbcDataReader DbReaderWatch = null;
        public OdbcCommand DbCommand1 = null;
        public OdbcDataReader DbReader1 = null;
        OdbcCommand DbCommand2 = null;
        OdbcDataReader DbReader2 = null;
        private ManagerConnection manager = null;

        Dictionary<string, Data> qCalls = new  Dictionary<string, Data>();

        SoundPlayer player = new SoundPlayer(); 
        bool iconChange = true;
        //List<TaskbarNotifier> vCalls = new List<TaskbarNotifier>();

        public static object _lock = new object();
        public static object _lockADB = new object();
        public static object _lockNewChannel = new object();
        // private Data DataAdd;
        // private Data DataDel;

        Socket clientSocket = null;
        public List<string> usersPhone = null;
        public List<string> usersAgentID = null;

        public List<string> extnames = null;
        public List<string> extnumbers = null;

        public Data ActiveDialCall = null; // äàííûå î âõîäÿùåì çâîíêå ïðè ïðîãðàììíîì ïîäíÿòèè òðóáêè
        //public string NewChannel = null;
        public string aThisExt = "408"; // âíóòðåííèé íîìåð òåêóùåãî ïîëüçîâàòåëÿ
        public string aThisAgentID = "100"; // åãî agentID èç asterisk.queue_agents

        public bool fAgentConnect = false;

        #endregion
        
        public FormMain()
		{
            using (new StackTracer("FormMain"))
            {
                InitializeComponent();
                //System.Media.SystemSounds.Beep.Play();
            }
		}
        ~FormMain()
        {

        }

        public static string FormatNumber(string sNumber)
        {
            using (new StackTracer("FormatNumber"))
            {
                string sFormattedNumber = "";
                int L = sNumber.Length;
                for (int i = L - 1; i >= 0; i--)
                {
                    sFormattedNumber = sFormattedNumber.Insert(0, sNumber.Substring(i, 1));
                    switch (L - i)
                    {
                        case 2:
                        case 4: sFormattedNumber = sFormattedNumber.Insert(0, "-"); break;
                        case 7: if (i > 0) sFormattedNumber = sFormattedNumber.Insert(0, ") "); break;
                        case 10: sFormattedNumber = sFormattedNumber.Insert(0, " ("); break;
                    }
                }
                return sFormattedNumber;
            }
        }

        #region Private Function
        // check DB conneect
        private bool CheckDB()
        {
            //using (new StackTracer("CheckDB"))
            {
                if (DbConnection.State != ConnectionState.Open &&
                    DbConnection.State != ConnectionState.Fetching &&
                    DbConnection.State != ConnectionState.Executing &&
                    DbConnection.State != ConnectionState.Connecting)
                {
                    try
                    {
                        DbConnection.Open();
                        DbCommand = DbConnection.CreateCommand();
                    }
                    catch (OdbcException ex)
                    {
                        Tracer.Instance.TraceWL3("Exception: [CheckDB] {0}", ex.Message);
                        /*MessageBox.Show("Connection to the DSN '" + sDSN + "' failed.\n" +
                                "The OdbcConnection returned the following message\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);*/
                        return false;
                    }
                }

                if (DbConnection1.State != ConnectionState.Open &&
                    DbConnection1.State != ConnectionState.Fetching &&
                    DbConnection1.State != ConnectionState.Executing &&
                    DbConnection1.State != ConnectionState.Connecting)
                {
                    try
                    {
                        DbConnection1.Open();
                        DbCommand1 = DbConnection1.CreateCommand();
                        DbCommand2 = DbConnection1.CreateCommand();
                    }
                    catch (OdbcException ex)
                    {
                        Tracer.Instance.TraceWL3("Exception: [CheckDB] {0}", ex.Message);
                       /* MessageBox.Show("Connection to the DSN '" + sDSN1 + "' failed.\n" +
                                "The OdbcConnection returned the following message\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);*/
                        return false;
                    }
                }

                return true;
            }
        }

        // connect to asterisk
        private void Connect()
        {
            using (new StackTracer("Connect"))
            {
                try
                {
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(aHost), aPort);

                    clientSocket.Connect(serverEndPoint);
                    // Login to the server; manager.conf needs to be setup with matching credentials.
                    clientSocket.Send(Encoding.ASCII.GetBytes("Action: Login\r\nUsername: " + aLogin +
                        "\r\nSecret: " + aPassw +
                        "\r\nActionID: AstClientLogin\r\nEvents: off\r\n\r\n"));
                }
                catch (SocketException ex)
                {
                    Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                    MessageBox.Show("Connection to Asterisk Server (" + aHost + ":" + aPort.ToString() + ").\nDescription: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cursor = Cursors.Arrow;
                }

                try
                {
                    manager = new ManagerConnection(aHost, aPort, aLogin, aPassw);
                    // manager.UnhandledEvent += new ManagerEventHandler(OnEvents);
                    manager.NewChannel += new NewChannelEventHandler(OnNewChanel);

                    //manager.Dial += new DialEventHandler(OnDial);
                    //manager.Link += new LinkEventHandler(OnLink);
                    manager.Hangup += new HangupEventHandler(OnHangUp);
                    manager.Join += new JoinEventHandler(OnJoin);
                    manager.NewState += new NewStateEventHandler(OnNewState);
                    manager.AgentLogoff += new AgentLogoffEventHandler(OnAgentLogOff);
                    manager.ParkedCall += new ParkedCallEventHandler(OnParkedCall);

                    manager.OriginateResponse += new OriginateResponseEventHandler(OnOriginate);

                    //manager.Link += new LinkEventHandler(OnLink);


                    // manager.AgentCalled += new AgentCalledEventHandler(manager_called);

                    // Uncomment next 2 line comments to Disable timeout (debug mode)
                    manager.DefaultResponseTimeout = 0;
                    manager.DefaultEventTimeout = 0;
                    manager.Login();
                }
                catch (Exception ex)
                {
                    Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                    MessageBox.Show("Error connect\n" + ex.Message);
                    manager.Logoff();
                    this.Close();
                }
            }
        }

       /* private void QueueCallsRefresh()
        {

            foreach (var it in qCalls)
            // foreach (int i = 0; i < qCalls)
            {
                if (it.Value.taskbarCalling != null)
                    //it.Value.taskbarCalling.Show("", it.Value.sFIO + "\n");// + NameGroups[it.Value.GroupId]);
                    it.Value.taskbarCalling.Show("1", "2");
            }

            TimerTray.Enabled = qCalls.Count > 0;
            TimerRing.Enabled = qCalls.Count > 0;
            TimerRing.Interval = 100;
            TimerTray.Interval = 100;
        }*/

        private Data getDataByPhone(string number, string numberReceiv)  // mobile = 9211111111 // city = 3254589
        {
            using (new StackTracer("getDataByPhone", new object[] {number, numberReceiv}))
            {
                int Index = -1;
                foreach (var it in qCalls)
                {
                    if (Index < it.Value.taskbarCalling.Index)
                        Index = it.Value.taskbarCalling.Index;
                }

                Index++;
                Tracer.Instance.TraceWL3("Index: {0}", Index);
                int ClientID = 0;

                //int Index = qCalls.Count > 0 ? qCalls. .Index + 1 : 0;
                try
                {
                    int groupId = 0;
                    int priorGroupID = 0; // have more priority that groupId
                    string FIO = "";
                    string Comment = "";
                    DateTime birtday = DateTime.MinValue;

                    // 1. [groupId] Boss by number
                    if (number.Equals("8129602911") || number.Equals("9219602911") /*||
                        number.Equals("8129130279") || number.Equals("9219130279")*/)
                    {
                        groupId = Boss;
                        FIO = "Абдин Александр Александрович";
                    }

                    /*DbCommand.CommandText = "select cl.Comment, cl.GroupID, IFNULL(c.id,0), c.birthday, concat(surname, ' ', name, ' ', fname)  " +
                                            "from Calls cl left outer join clients c on cl.ClientID=c.id " +
                                            "where cl.Number like '%" + number + "%' and cl.Comment <> '' order by `tIncomingCall` desc limit 1";*/

                    // 2. [Comment] Get concated comments from Calls
                    DbCommand.CommandText = "select GROUP_CONCAT(DISTINCT IFNULL(Comment,'???') separator '\n') from Calls where Number like '%" + number + "%' and Comment <> ''";

                    DbReader = DbCommand.ExecuteReader();
                    if (DbReader.Read() && 
                        false == DbReader.IsDBNull(0))
                    {
                        Comment = DbReader.GetString(0);
                    }
                    DbReader.Close();

                    // 3. [priorGroupID] Get group id from Calls
                    DbCommand.CommandText = "select IFNULL(cl.GroupID,0) from Calls cl where cl.Number like '%" + number + "%' order by `tIncomingCall` desc limit 1";

                    DbReader = DbCommand.ExecuteReader();
                    if (DbReader.Read())
                    {
                        priorGroupID = DbReader.GetInt32(0);
                    }
                    DbReader.Close();

                    if (Boss == groupId)
                    {
                        priorGroupID = Boss;
                    }

                    // 4. [groupId] Get typename and set group id 
                    DbCommand.CommandText = "select IFNULL(typename,'???'), IFNULL(name,'???') from phonebook " +
                                   "where del=0 and (extnumber like ('%" + number + "%') or extranumbers like ('%" + number + "%'))";

                    DbReader = DbCommand.ExecuteReader();
                    if (DbReader.Read())
                    {
                        switch (DbReader.GetString(0))
                        {
                            case "ЛПУ": groupId = LPU; break;
                            case "Страховые": groupId = InsuranceCompanyRU; break;
                            case "СПАМ":
                            case "Спам":
                            case "спам":
                                groupId = Spam; break;
                            case "Аптека":
                            case "Ассистенты врача":
                            case "Водитель, представитель клиники":
                            case "Врачи-консультанты":
                            case "Выездная бригада":
                            case "Лаборатория":
                            case "Лечащие врачи":
                            case "Менеджмент":
                            case "Регистратура":
                            case "Техническое обеспечение":
                            case "Травматологи":
                            case "Транспортный отдел": groupId = Staff; break;
                        }

                        FIO = DbReader.GetString(1);
                        DbReader.Close();
                        return new Data(number, numberReceiv, priorGroupID > 0 ? priorGroupID : groupId, FIO, birtday, Index, this, 0, Comment);
                    }
                    DbReader.Close();

                    // 5. Get data (about patient) from clients, r_police, programs_clients
                    DbCommand.CommandText = "select concat(IFNULL(surname,'???'), ' ', IFNULL(name,'???'), ' ', IFNULL(fname,'???')), IFNULL(r.policeid,0), c.birthday, IFNULL(pc.programid,0), c.id from clients c " +
                                   "left outer join r_police r on r.clientid=c.id " +
                                   "left outer join programs_clients pc on pc.clientid=c.id " +
                                   "where concat(mobile_city, mobile_number) like '%" + number +
                                   "%' or concat(phone_city, phone_number) like '%" + number +
                                   "%' or '" + number + "' like mobile or '" + number + "' like phone";
                    // 1, 2, 3
                    DbReader = DbCommand.ExecuteReader();
                    if (DbReader.Read())
                    {
                        FIO = DbReader.GetString(0);
                        groupId = (DbReader.GetInt32(1) == VIP ? VIP :
                                  (DbReader.GetInt32(3) > 0 ? EuromedInVitro : EuromedClinic));//r.policeid==3 - VIP
                        birtday = DbReader.GetDateTime(2);
                        ClientID = DbReader.GetInt32(4);
                        DbReader.Close();
                        return new Data(number, numberReceiv, priorGroupID > 0 ? priorGroupID : groupId, FIO, birtday, Index, this, ClientID, Comment);
                    }
                    DbReader.Close();


                    /*InsuranceCompanyENG*/
                    DbCommand.CommandText = "select IFNULL(name,'???') from insur where del=0 and is_insur>0 and (gop_request like '%" + number +
                                                "%' or payment_clarification like '%" + number +
                                                "%' or chiefs_contacts like '%" + number + "%') ";
                    DbReader = DbCommand.ExecuteReader();
                    if (DbReader.Read())
                    {
                        FIO = DbReader.GetString(0);
                        groupId = Boss == priorGroupID ? Boss : InsuranceCompanyENG;
                        DbReader.Close();
                        return new Data(number, numberReceiv, priorGroupID > 0 ? priorGroupID : groupId, FIO, birtday, Index, this, 0, Comment);
                    }
                    DbReader.Close();
                    if (priorGroupID == 0)
                    {

                        // 4,5,6
                        if (numberReceiv == "3270301")
                        {
                            if ((number[0] == '9' && number.Length == 10) || // format like 9111116120
                                (number[0] == '7' && number.Length == 11) || // format like 79111116120
                                (number[0] == '8' && number.Length == 11))   // format like 89111116120
                                groupId = NewMobileEuromedClinic;
                            else if ((number.IndexOf("812") == 0 && number.Length == 10) ||
                                    (number.IndexOf("8812") == 0 && number.Length == 11) || (number.IndexOf("7812") == 0 && number.Length == 11) ||
                                    number.Length == 7) // format like 3471274 or 7[8]123334465
                                groupId = NewPhoneEuromedClinic;
                            else if (number.Length > 10 && number[0] != '8' && number[0] != '7')
                                groupId = NewOtherLand;
                            else
                                groupId = NewOtherCityEuromedClinic;
                        }
                        // 7 , 9 , 10
                        if (numberReceiv == "4380343")
                        {
                            //if ((number.IndexOf("812") == 0 && number.Length == 10) || number.Length == 7 ||
                            //   ((number[0] == '9' && number.Length == 10)))
                            if ((number[0] == '9' && number.Length == 10) || // format like 9111116120
                                (number[0] == '7' && number.Length == 11) || // format like 79111116120
                                (number[0] == '8' && number.Length == 11)   // format like 89111116120
                                ||
                                (number.IndexOf("812") == 0 && number.Length == 10) ||
                                (number.IndexOf("8812") == 0 && number.Length == 11) || (number.IndexOf("7812") == 0 && number.Length == 11) ||
                                 number.Length == 7)// format like  3471274 or 7[8]123334465
                                groupId = NewEuromedInVitro;
                            else if (number.Length > 10 && number[0] != '8' && number[0] != '7')
                                groupId = NewOtherLand;
                            else
                                groupId = NewOtherCityEuromedInVitro;
                        }
                        // 8 ?
                        if (numberReceiv == "4380313")
                        {
                            groupId = NewEuromedExpress;
                        }
                    }
                    else groupId = priorGroupID;

                    return new Data(number, numberReceiv, groupId, FIO, birtday, Index, this, 0, Comment);


                    /*
                                enum ClientsGroups : int 
                            {
                                EuromedClinic = 1,      //1) Ïàöèåíò Euromed Clinic – âñå ïàöèåíòû, êîòîðûå âíåñåíû â áàçó “ïàöèåíòû” â èíòðàíåòå, êðîìå íèæåóêàçàííûõ òå êîòîðûõ ÿ âûáèðàþ ñåé÷àñ íî íå VIP
                                EuromedInVitro,         //2) Ïàöèåíò Euromed In Vitro – âñå ïàöèåíòû, âíåñåííûå â áàçó “ïàöèåíòû” â èíòðàíåòå, êîòîðûå çàíåñåíû â ðàçäåë “Ñ÷åòà-ïðîãðàìû”. Èëè áûëè âíåñåíû òóäà ðàíåå (íà íàèõ áûëè çàêðûòû êàêèå-òî ïðîãðàììû). Ïëþñ ïàöèåíòû, êîòîðûå âíåñåíû ê òàêèì ïàöèåíòêàì â àíêåòó â êà÷åñòâå ñóïðóãîâ.
                                VIP,                    //3) VIP ïàöèåíò - âñå ïàöèåíòû, êîòîðûå âíåñåíû â áàçó “ïàöèåíòû” â èíòðàíåòå è ó íèõ â àíêåòå óêàçàíî, ÷òî îíè VIP òå êîòîðûõ ÿ âûáèðàþ ñåé÷àñ VIP
                                NewMobileEuromedClinic, //4) Íîâûé ìîáèëüíûé Euromed Clinic – âñå çâîíêè, êîòîðûå íå èäåíòèôèöèðîâàíû ïî èñòî÷íèêó è ïîñòóïàþò íà íîìåð 327 03 01 ñ ìîáèëüíîãî
                                NewPhoneEuromedClinic,  //5) Íîâûé ãîðîäñêîé Euromed Clinic – âñå çâîíêè, êîòîðûå íå èäåíòèôèöèðîâàíû ïî èñòî÷íèêó è ïîñòóïàþò íà íîìåð 327 03 01 ñ ãîðîäñêîãî
                                NewOtherCityEuromedClinic,  //6) Íîâûé èíîãîðîäíèé Euromed Clinic – âñå çâîíêè èç äðóãèõ ðåãèîíîâ ÐÔ, ïîñòóïàþùèå íà íîìåð 327 03 01
                                NewOtherCityEuromedInVitro, //7) Íîâûé èíîãîðîäíèé Euromed In Vitro – âñå çâîíêè èç äðóãèõ ðåãèîíîâ ÐÔ, ïîñòóïàþùèå íà íîìåð 438 03 43
                                NewOtherLand,           //8) Íîâûé ìåæäóíàðîäíûé – âñå çâîíêè, ïîñòóïàþùèå èç-çà ãðàíèöû íà ëþáîé íîìåð òîæå ïîíÿòíî - ÿ òàê ïîíÿë íîìåðà íå äîëæíû íà÷èíàòüñÿ â 8-êè
                                NewEuromedInVitro,      //9) Íîâûé Euromed In Vitro – âñå çâîíêè, êîòîðûå íå èäåíòèôèöèðîâàíû ïî èñòî÷íèêó è ïîñòóïàþò íà íîìåð 438 03 43
                                NewEuromedExpress,      //10) Íîâûé Euromed Åxpress – âñå çâîíêè, êîòîðûå íå èäåíòèôèöèðîâàíû ïî èñòî÷íèêó è ïîñòóïàþò íà íîìåð 438 03 43
                                LPU,                    //11) ËÏÓ – ïî ñïðàâî÷íèêó ïî phonebook?
                                InsuranceCompanyRU,     //12) Ñòðàõîâàÿ êîìïàíèÿ – ïî ñïðàâî÷íèêó ïî phonebook?
                                InsuranceCompanyENG,    //13) Insurance company – ïî ñïðàâî÷íèêó ïî phonebook?
                                Hotel,                  //14) Ãîñòèíèöà – ïî ñïðàâî÷íèêó ïî phonebook?
                                Staff                   //15) Ñîòðóäíèê – ñîãëàñíî çàêëàäêè “ñîòðóäíèêè” íóæåí òâîé êîììåíò (òàáëèöà stuff?)
                            }
                             */
                }
                catch (Exception e)
                {
                    if (false == DbReader.IsClosed)
                    {
                        DbReader.Close();
                    }
                    Tracer.Instance.TraceWL3("Exception: {0}", e.Message);
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new Data("", "", 0, "", DateTime.MinValue, Index, this);
                }
            }
        }
        
        public void ScrollCalls(int Delta)
        {
            using (new StackTracer("ScrollCalls", Delta))
            {
                foreach (var call in qCalls)
                {
                    if (call.Value.taskbarCalling != null)
                    {
                        call.Value.taskbarCalling.Top += Delta / 10;
                    }
                }
            }
        }

        // move down all mini window with calls 
        private void MovingCalls(int index)
        {
            using (new StackTracer("MovingCalls", index))
            {
                //for (int i = 0; i < vCalls.Count; i++)
                foreach (var it in qCalls)
                {
                    if (it.Value.taskbarCalling.Index > index)
                    {
                        it.Value.taskbarCalling.MovingDown();
                        //System.Threading.Thread.Sleep(200);
                    }
                }
            }
        }
        #endregion


        #region ATS Events

        void OnAgentLogOff(object sender, AgentLogoffEvent e)
        {
            Tracer.Instance.TraceWL3("OnAgentLogOff: {0}", e);
        }
       /* void OnEvents(object sender, ManagerEvent e)
        {
           // if (e.GetType() is BridgeEvent)
            {
                Debug.Write("#######\n");
                Debug.Write(e);
                Debug.Write("#######\n");

            }
        }*/

        public void Park(string ParkedChannel, string CallerChannel)
        {
            using (new StackTracer("Park", new object[] { ParkedChannel, CallerChannel }))
            {
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Send(Encoding.ASCII.GetBytes(
                        "Action: Park\r\nChannel: " + ParkedChannel + "\r\nChannel2: " + CallerChannel + "\r\nTimeout: 3600000\r\n\r\n"));
                    }
                }
                catch (Exception ex)
                {
                    Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Originate(string Exten, string context = "parkedcalls")
        {
            using (new StackTracer("Originate", new object[] { Exten, context }))
            try
            {
                if (clientSocket.Connected)
                {
                    clientSocket.Send(Encoding.ASCII.GetBytes(
                    "Action: Originate\r\nChannel: Local/1" + aThisExt +
                    "@local\r\nContext: " + context + "\r\nExten: " + Exten + "\r\nPriority: 1\r\nCallerID: " + aThisExt + "\r\nAsync: true\r\n\r\n"));
                }
            }
            catch (Exception ex)
            {
                Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        public void Bridge(string ch1, string ch2)
        {
            using (new StackTracer("Bridge", new object[] { ch1, ch2 }))
            {
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Send(Encoding.ASCII.GetBytes(
                        "Action: Bridge\r\nChannel1: " + ch1 + "\r\nChannel2: " + ch2 + "\r\nTone: yes\r\n\r\n"));
                    }
                }
                catch (Exception ex)
                {
                    Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void HangUp(string Channel)
        {
            using (new StackTracer("HangUp", new object[] { Channel }))
            {
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Send(Encoding.ASCII.GetBytes(
                        "Action: Hangup\r\nChannel: " + Channel + "\r\n\r\n"));
                    }
                }
                catch (Exception ex)
                {
                    Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void Redirect(string SourceChannel, string DestExten)
        {
            using (new StackTracer("Redirect", new object[] { SourceChannel, DestExten }))
            {
                try
                {
                    if (clientSocket.Connected)
                    {
                        clientSocket.Send(Encoding.ASCII.GetBytes(
                        "Action: Redirect\r\nChannel: " + SourceChannel + "\r\nExten: " + DestExten + "\r\nContext: world\r\nPriority: 1\r\n\r\n"));
                    }
                }
                catch (Exception ex)
                {
                    Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        // [ActiveCall] status = InQue(Îæèäàíèå)|CONNECT(ðàçãîâîð) by callId = uniqID.
        public void AnswerCall(Data data)
        {
            using (new StackTracer("AnswerCall", new object[] { data }))
            {
                Redirect(data.EventData.Channel, "0" + aThisExt);
            }
        }

        public void OnParkedCall(object sender, ParkedCallEvent e)
        {
#if LIGHT
            if (fAgentConnect) return;
#endif            
            lock (_lock)
            {
                using (new StackTracer("OnParkedCall", new object[] { sender, e }))
                {
                    foreach (var v in qCalls)
                    {
                        if (v.Value.EventData.Channel == e.Channel)
                        {
                            qCalls[v.Key].ParkExt = e.Exten;
                            qCalls[v.Key].ParkChannel = e.Channel;
                            break;
                        }
                    }
                }
            }
        }

        public void OnOriginate(object sender, OriginateResponseEvent e)
        {
            /*lock (_lock)
            {
                foreach (var v in qCalls)
                {
                    if (v.Value.isOriginate)
                    {
                        qCalls[v.Key].OriginateUID = e.UniqueId;
                        break;
                    }
                }
            }*/
        }
        
        void OnJoin(object sender, JoinEvent e)
        {
            using (new StackTracer("OnJoin", new object[] { sender, e }))
            {
                lock (_lock)
                {
                    if (qCalls.ContainsKey(e.UniqueId))
                    {
                        qCalls[e.UniqueId].isJoined = true;
                    }
                }
            }
        }
        
        void OnNewChanel(object sender, NewChannelEvent e)
        {
#if LIGHT
            if (fAgentConnect) return;
#endif
            using (new StackTracer("OnNewChanel", new object[] { sender, e }))
            {
                //return;
                /* Event NewChannel: 
                CallerIdNum:9219986349
                Attributes:   exten: 3270301
                context: incoming*/
                try
                {

                    /*if (ActiveDialCall != null && e.Channel.IndexOf("SIP/" + aThisExt) == 0 && e.CallerIdNum == null && e.CallerIdName == null)
                    {
                        lock (_lockNewChannel)
                        {
                            // bridge new channel and incoming channel
                            clientSocket.Send(Encoding.ASCII.GetBytes("Action: Bridge\r\nChannel1: " + e.Channel +
                                "\r\nChannel2: " + ActiveDialCall.EventData.Channel + "\r\n\r\n"));
                            ActiveDialCall = null;
                        }
                    }
                    else */
                    if (e != null && e.Attributes != null && e.Attributes["context"] != null && e.Attributes["context"] == "incoming"
                        && e.CallerIdNum != null && e.CallerIdNum.Trim() != "" && !qCalls.ContainsKey(e.CallerIdNum))
                    {

                        lock (_lock)
                        {
                            //if (e.CallerIdNum.IndexOf('7') == 0)
                            if (e.Attributes["exten"].Length == 11)
                                e.CallerIdNum = e.CallerIdNum.Remove(0, 1);
                            Data dt = getDataByPhone(e.CallerIdNum, e.Attributes["exten"]);
                            dt.EventData = e;
                            dt.tIncomingCall = e.DateReceived;

                            try
                            {
                                int newID = 0;

                                OdbcCommand dbCommand = new OdbcCommand("SELECT id FROM Calls WHERE UniqueID = '" + e.UniqueId + "'", DbConnection);
                                OdbcDataReader dbReader = dbCommand.ExecuteReader();
                                if (dbReader.Read())
                                {
                                    newID = dbReader.GetInt32(0);
                                    dbReader.Close();
                                }
                                else
                                {
                                    dbReader.Close();
                                    dbCommand.CommandText = "insert into Calls (UniqueID, Number, NumberExt, ClientID, tIncomingCall, GroupID) values ('" +
                                            e.UniqueId + "', '" + e.CallerIdNum + "', '" + e.Attributes["exten"] + "'," +
                                            (dt.ClientID > 0 ? dt.ClientID.ToString() : "null") + ", now(), " + dt.GroupId.ToString() + ")";
                                    dbCommand.ExecuteNonQuery();
                                    dbCommand.CommandText = "select max(id) from Calls";
                                    dbReader = dbCommand.ExecuteReader();
                                    if (dbReader.Read())
                                        newID = dbReader.GetInt32(0);
                                    dbReader.Close();
                                }
                                
                                dt.CallID = newID;
                                qCalls.Add(e.UniqueId, dt);
                            }
                            catch (Exception ex)
                            {
                                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                            }

                        }
                    }
                    //Tray.Text = (e.CallerIdNum != null ? e.CallerIdNum  : "") + " - "+ tst.ToString();

                }
                catch (Exception ex)
                {
                    Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /*
        void OnLeave(object sender, LeaveEvent e)
        {
            int i = 0;
            Debug.Write("########## OnLeave");
            Debug.Write(e);
            Debug.Write("##########");

          
        }*/

        void OnNewState(object sender, NewStateEvent e)
        {
#if LIGHT
            if (fAgentConnect) return;
#endif
            if (e.ChannelStateDesc == "Up")
            {
                if (qCalls.ContainsKey(e.UniqueId))
                {
                    lock (_lock)
                    {
                        using (new StackTracer("OnNewState UP", new object[] { sender, e }))
                        {
                            qCalls[e.UniqueId].tBeginCall = e.DateReceived;
                            qCalls[e.UniqueId].taskbarCalling.StartTime();
                            try
                            {
                                string iCurrAgentID = "";
                                // check agentId current call
                                DbCommand2.CommandText = "select memberId from  ActiveCall where status='CONNECT' and callId = " + e.UniqueId;
                                DbReader2 = DbCommand2.ExecuteReader();
                                if (DbReader2.Read())
                                    iCurrAgentID = DbReader2.GetInt32(0).ToString();
                                DbReader2.Close();

                                if (iCurrAgentID == "")
                                    iCurrAgentID = aThisAgentID;

                                // îáíîâèì çàïèñü òîëüêî åñëè ýòî íàø çâîíîê èëè åñëè ýòîò çâîíîê íå áûë âçÿò ïðîãðàììíî
                                OdbcCommand dbCommand = new OdbcCommand("update Calls set tBeginCall=now(),agentID = '" + iCurrAgentID +
                                    "' where (agentID = '" + aThisAgentID + "' or agentID='' or agentID is null) and id = " +
                                    qCalls[e.UniqueId].CallID.ToString(), DbConnection);
                                dbCommand.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    lock (_lock)
                    {
                        using (new StackTracer("OnNewState Originate or Voice", new object[] { sender, e }))
                        {
                            foreach (var v in qCalls)
                            {
                                /*if (v.Value.OriginateUID!="" && v.Value.OriginateUID == e.UniqueId)
                                {
                                    qCalls[v.Key].OriginateChannel = e.Channel;
                                    break;
                                }*/
                                if (e.CallerIdNum == v.Value.OriginateUID && v.Value.OriginateUID != "")
                                {
                                    qCalls[v.Key].OriginateChannel = e.Channel;
                                    Tracer.Instance.TraceWL3("Originate Channel: {0}", e.Channel);
                                    break;
                                }

                                if (v.Value.isVoiceCall && e.Channel.IndexOf(aThisExt) > 0)
                                {
                                    qCalls[v.Key].VoiceChannel = e.Channel;
                                    Tracer.Instance.TraceWL3("VoiceCall Channel: {0}", e.Channel);
                                    break;
                                }
                            }
                        }
                        
                    }

                }
            }
        }
        
        void OnHangUp(object sender, HangupEvent e)
        {
#if LIGHT
            if (fAgentConnect) return;
#endif            
            if (qCalls.ContainsKey(e.UniqueId))
            {
                lock (_lock)
                {
                    using (new StackTracer("OnHangUp", new object[] { sender, e }))
                    {
                        try
                        {
                            qCalls[e.UniqueId].tEndCall = e.DateReceived;
                            qCalls[e.UniqueId].taskbarCalling.StopTime();

                            // îáíîâèì çàïèñü òîëüêî åñëè ýòî íàø çâîíîê èëè åñëè ýòîò çâîíîê íå áûë âçÿò ïðîãðàììíî
                            OdbcCommand dbCommand = new OdbcCommand("update Calls set tEndCall=now() where " + (fAgentConnect?"":" tEndCall is null or ") + "(agentID = '" + aThisAgentID +
                                    "' and id = " + qCalls[e.UniqueId].CallID.ToString() + ")", DbConnection);

                            dbCommand.ExecuteNonQuery();

                            /*if (qCalls[e.UniqueId].Comment != "")
                            {
                                qCalls[e.UniqueId].is_del = true;
                            }*/
                            }
                        catch (Exception ex)
                        {
                            Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
               
            }
            return;
        }

        #endregion


        #region Form Events Handlers
        // ïîñëå îòîáðàæåíèÿ ôîðìû 
        private void FormMain_Shown(object sender, EventArgs e)
        {
            using (new StackTracer("FormMain_Shown", new object[] { sender, e }))
            {
                try
                {
                    // check run already
                    Process process = Process.GetCurrentProcess();
                    Process[] pl = Process.GetProcesses();
                    foreach (var p in pl)
                    {
                        Console.WriteLine(p.ProcessName);
                        if (p.ProcessName == process.ProcessName && p.Id != process.Id)
                        {
                            Tracer.Instance.TraceWL3("Kill already process: {0}", p.StartInfo.FileName);
                            p.Kill();
                        }
                    }

                    // read ini file
                    FileInfo info = new FileInfo(process.MainModule.FileName);
                    if (File.Exists(Path.GetDirectoryName(info.FullName) + "\\Settings.ini"))
                    {
                        IniFile file = IniFile.FromFile(Path.GetDirectoryName(info.FullName) + "\\Settings.ini");

                        sDSN = file["ODBC"]["DSN"];
                        sDSN1 = file["ODBC"]["DSN1"];
                        aHost = file["Asterisk"]["host"];
                        aPort = Int32.Parse(file["Asterisk"]["port"]);
                        aLogin = file["Asterisk"]["login"];
                        aPassw = file["Asterisk"]["passw"];
                        aThisExt = file["Asterisk"]["ext"];

                        int number;
                        bool result = Int32.TryParse(file["Asterisk"]["ShowWarningBeforeHangUp"], out number);
                        if (result)
                        {
                            bShowWarningBeforeHangUp = number == 1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("File '" + Path.GetDirectoryName(info.FullName) + "\\Settings.ini" + "' is not exists.\n",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                        return;
                    }

                    // db connect
                    DbConnection = new OdbcConnection("DSN=" + sDSN);
                    DbConnection1 = new OdbcConnection("DSN=" + sDSN1);
                    if (!CheckDB())
                    {
                        MessageBox.Show("DataBase connection error.\n",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                        return;
                    }

                    usersPhone = new List<string>();
                    usersAgentID = new List<string>();

                    extnames = new List<string>();
                    extnumbers = new List<string>();

                    // get users list
                    DbCommand1.CommandText = "select name, agentphone, agentid from queue_agents where state = 'in' order by name";
                    // 1, 2, 3
                    DbReader1 = DbCommand1.ExecuteReader();
                    while (DbReader1.Read())
                    {
#if LIGHT
                            if (DbReader1.GetString(0) != "Òåñòèðîâàíèå") fAgentConnect = true;
#endif
                        cmbLogins.Items.Add(DbReader1.GetString(0));
                        usersPhone.Add(DbReader1.GetInt32(1).ToString());
                        usersAgentID.Add(DbReader1.GetInt32(2).ToString());
                    }
                    DbReader1.Close();

                    // get users list
                    DbCommand.CommandText = "select name, ext from ext_numbers order by name";
                    // 1, 2, 3
                    DbReader = DbCommand.ExecuteReader();
                    while (DbReader.Read())
                    {
                        extnames.Add(DbReader.GetString(0));
                        extnumbers.Add(DbReader.GetString(1));
                    }
                    DbReader.Close();

                    // fill GroupInfo data
                    //DbCommand.CommandText = "select id, pid, name from CallStatButtons where del=0 and pid='0' order by sort, name";
                    DbCommand.CommandText = "select id, pid, ifnull(name,'Buttons.noname') from CallStatButtons where del=0 order by sort, name";
                    DbReader = DbCommand.ExecuteReader();

                    OdbcCommand DbCommand_in = DbConnection.CreateCommand();
                    while (DbReader.Read())
                    {
                        ButtonInfo buttonInfo = new ButtonInfo();
                        buttonInfo.m_id = DbReader.GetInt32(0);
                        buttonInfo.m_pid = DbReader.GetInt32(1);
                        buttonInfo.m_name = DbReader.GetString(2);

                        DbCommand_in.CommandText = "select ifnull(CallStatGroups.name,'Groups.noname') 'groupname', CallStatGroups.id 'groupid'," +
                                                 "ifnull(CallStatGroupLists.name,'GroupLists.noname') 'listname', CallStatGroupLists.id 'listid' " +
                                                 "from CallStatGL " +
                                                 "LEFT OUTER JOIN CallStatGroups ON (CallStatGL.groupid=CallStatGroups.id) " +
                                                 "LEFT OUTER JOIN CallStatGroupLists ON (CallStatGL.groupid=CallStatGroupLists.groupid) " +
                                                 "where  CallStatGL.buttonid=" + buttonInfo.m_id.ToString() +
                                                 " order by CallStatGroupLists.sort, CallStatGroupLists.name";
                        DbReader1 = DbCommand_in.ExecuteReader();
                        long currGroupId = -1;
                        GroupsInfo currGroupInfo = null;
                        while (DbReader1.Read())
                        {
                            long groupId = DbReader1.GetInt32(1);
                            // if new group
                            if (groupId != currGroupId)
                            {
                                // add previous group to the current button
                                /*if (null != currGroupInfo)
                                {
                                    buttonInfo.m_vGroups.Add(currGroupInfo);
                                }*/

                                currGroupId = groupId;
                                currGroupInfo = new GroupsInfo();
                                currGroupInfo.m_id = groupId;
                                currGroupInfo.m_name = DbReader1.GetString(0);
                                buttonInfo.m_vGroups.Add(currGroupInfo);
                            }

                            // add to the current group item
                            if (null != currGroupInfo)
                            {
                                GroupsInfoItem groupItem = new GroupsInfoItem();
                                groupItem.m_id = DbReader1.GetInt32(3);
                                groupItem.m_name = DbReader1.GetString(2);
                                currGroupInfo.m_vGroupsList.Add(groupItem);
                            }
                        }
                        DbReader1.Close();
                        m_vButtons.Add(buttonInfo);
                    }
                    DbReader.Close();

                }
                catch (OdbcException ex)
                {
                    Tracer.Instance.TraceWL3("OdbcException: {0}", ex.Message);
                    MessageBox.Show("Возникла ошибка при инициализации приложения\nDesc: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                catch (Exception ex)
                {
                    Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                    MessageBox.Show("Возникла ошибка при инициализации приложения\nDesc: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                #if LIGHT
                    aThisExt = "0";
                    aThisAgentID = Environment.MachineName + "\\" + Environment.UserName;
                    Tray.Text = "Euromed Asterisk Viewer";
                    AutorizationSuccess();
                #endif
            }

        }

        private void AutorizationSuccess()
        {
        /* MessageBox.Show(FormatNumber("9115698524"));
            MessageBox.Show(FormatNumber("89115698524"));
            MessageBox.Show(FormatNumber("5698524"));
            MessageBox.Show(FormatNumber("+79115698524"));*/
            using (new StackTracer("AutorizationSuccess"))
            {
                Cursor = Cursors.WaitCursor;
                //Refresh();
                try
                {
                
                    player.Stream = Properties.Resources.ring;

                    //connect to Asterisk
                    Connect();

                }
                catch (Exception ex)
                {
                    Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
                    MessageBox.Show("Возникла ошибка при инициализации приложения\nDesc: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Cursor = Cursors.Default;
                // to task bar forever
                this.ShowInTaskbar = false;
                this.Visible = false;
                //TimerRing.Enabled = TimerTray.Enabled = true;
                WatchTimer.Enabled = true;


                // add test
                /*qCalls["9115896324"] = new Data("9115896324", "3256987", 3, "mr. Nepster", DateTime.MinValue, 0);
                qCalls["9115896325"] = new Data("9115896325", "3256987", 5, "Åðìóøèí Ñåðãèé", DateTime.Now, 1);
                QueueCallsRefresh();*/
                /*Data dt = getDataByPhone("9213253878", "1236954");
                dt.tIncomingCall = DateTime.Now;
                dt.tBeginCall = DateTime.Now;
                qCalls.Add("1", dt);*/
            }
        }
        
        /*private void btnDisconnect_Click(object sender, EventArgs e)
		{
			btnConnect.Enabled = true;
			if (this.manager != null)
			{
				manager.Logoff();
				this.manager = null;
			}
			btnDisconnect.Enabled = false;
		}*/

        
       /* private void Tray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = true;
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                
                //Timer.Enabled = qCalls.Count > 0;
                
                TimerTray.Enabled = false;
                TimerRing.Enabled = false;
                Tray.Icon = Properties.Resources.asterisk_orange;
                player.Stop();

                // ÷òîáû head îòðèñîâàëñÿ
                FIO.Text = " " + FIO.Text;
                FIO.Text = FIO.Text.Remove(0, 1);
            }
        }*/

        /*
        protected override void OnResize(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.Visible = false;
                //Tray.ShowBalloonTip(2000);
                //Timer.Enabled = qCalls.Count > 0;
                QueueCallsRefresh();
            }
        }*/

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (new StackTracer("FormMain_Shown", new object[] { sender, e }))
            {
                if (DbReader != null) DbReader.Close();
                if (DbCommand != null) DbCommand.Dispose();
                if (DbConnection != null) DbConnection.Close();
                if (DbReader1 != null) DbReader1.Close();
                if (DbCommand1 != null) DbCommand1.Dispose();
                if (DbReader2 != null) DbReader2.Close();
                if (DbCommand2 != null) DbCommand2.Dispose();
                if (DbConnection1 != null) DbConnection1.Close();
                //btnConnect.Enabled = true;
                if (this.manager != null)
                {
                    manager.Logoff();
                    this.manager = null;
                }
                //btnDisconnect.Enabled = false;
                if (clientSocket != null) if (clientSocket.Connected)
                        clientSocket.Disconnect(false);
            }
        }

        // icon change
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                TimerTray.Interval = 1000;
                Tray.Icon = iconChange ? Properties.Resources.alarm :
#if LIGHT
                        Tray.Icon = Properties.Resources.asterisk_red;
#else
                        Tray.Icon = Properties.Resources.asterisk_orange;
#endif
                iconChange = !iconChange;

                /*****
                if (vCalls.Count > 1)
                {
                    vCalls[1].Hidden();
                    vCalls.RemoveAt(0);
                }
                MovingCalls(0);*/
            }
            catch (Exception ex)
            {
                Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
            }
            
        }

        // play ring
        private void TimerRing_Tick(object sender, EventArgs e)
        {
            try{
                TimerRing.Interval = 28000;
                player.Play();
            }
            catch (Exception ex)
            {
                Tracer.Instance.TraceWL3("Exception: {0}", ex.Message);
            }
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
           /* head.Invalidate();
            head.Refresh();
            head.Show();*/

            /*int Index = vCalls.Count > 0 ? vCalls[vCalls.Count - 1].Index + 1 : 0;
            TaskbarNotifier taskbarCalling = new TaskbarNotifier(Index);
            taskbarCalling.SetBackgroundBitmap(new Bitmap(Properties.Resources.call), Color.FromArgb(255, 0, 255));
            taskbarCalling.TitleRectangle = new Rectangle(150, 57, 125, 28);
            vCalls.Add(taskbarCalling);*/
            
            /*taskbarCalling.SetCloseBitmap(new Bitmap(GetType(), "close.bmp"), Color.FromArgb(255, 0, 255), new Point(280, 57));
            taskbarCalling.TitleRectangle = new Rectangle(150, 57, 125, 28);
            taskbarCalling.ContentRectangle = new Rectangle(75, 92, 215, 55);
            /*taskbarCalling.TitleClick += new EventHandler(TitleClick);
            taskbarCalling.ContentClick += new EventHandler(ContentClick);
            taskbarCalling.CloseClick += new EventHandler(CloseClick);*/
           // vCalls[vCalls.Count - 1].Show(vCalls[vCalls.Count - 1].Index.ToString(), "", 1, 1, 1);
            
        }
       
        private void btnRedirect_Click(object sender, EventArgs e)
        {
           /* if (vCalls.Count > 2)
            {
                vCalls[1].Hidden();
                vCalls.RemoveAt(1);
            }
            MovingCalls(1);*/
            // taskbarCalling.Hidden();
          //  taskbarCalling1.Hidden();
        }

        // context close
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TrayMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            TimerTray.Enabled = qCalls.Count > 0;
        }

        private void TrayMenu_Opened(object sender, EventArgs e)
        {
            TimerTray.Enabled = false;
        }

        // context settings
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            // add test
           /* Data dt = getDataByPhone("test "+qCalls.Count.ToString(), "1236954");
            dt.tIncomingCall = DateTime.Now;
            dt.tBeginCall = DateTime.Now;
            dt.tEndCall = DateTime.Now;
            qCalls.Add(DateTime.Now.ToLongTimeString(), dt);*/
            MessageBox.Show("Настройки программы находятся в файле Settings.ini",
  "Настройки", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void WatchTimer_Tick(object sender, EventArgs e) // check data from ATS Events
        {
            // check correct login user
            // delete calls by check db
            try
            {
                lock (_lockADB)
                {
                    // check DB connections
                    CheckDB();
#if LIGHT
                    fAgentConnect = false;
                    DbCommand1.CommandText = "select name, agentphone, agentid from queue_agents where state = 'in' order by name";
                    DbReader1 = DbCommand1.ExecuteReader();
                    
                    while (DbReader1.Read())
                    {
                        if (DbReader1.GetString(0) != "Òåñòèðîâàíèå")
                            fAgentConnect = true;
                    }
                    DbReader1.Close();

                    if (fAgentConnect)
                    {
                        // hide all calls
                        foreach (var it in qCalls)
                        {
                            it.Value.taskbarCalling.Hidden();
                        }
                        qCalls.Clear();
                    }
#else
                    
                    DbCommand1.CommandText = "select * from queue_agents where agentID = '" + aThisAgentID +
                                "' and agentphone = '" + aThisExt + "'";

                    DbReader1 = DbCommand1.ExecuteReader();
                    if (!DbReader1.Read())
                    {
                        DbReader1.Close();

                        // stop all back handlers
                        WatchTimer.Enabled = false;
                        Tray.Icon = Properties.Resources.asterisk_orange;
                        player.Stop();
                        TimerTray.Stop();
                        TimerRing.Stop();

                        // hide all calls
                        foreach (var it in qCalls)
                        {
                            it.Value.taskbarCalling.Hidden();
                            if (it.Value.wndShow) it.Value.wndCalling.Close();
                        }
                        qCalls.Clear();

                        // reload auth data
                        usersPhone.Clear();
                        usersAgentID.Clear();
                        cmbLogins.Items.Clear();
                        cmbLogins.SelectedIndex = -1;
                        cmbLogins.Text = "";
                        tbExtNumb.Text = "";
                        Tray.Text = "Клиентский модуль АТС Asterisk [Авторизация]";
                        DbCommand1.CommandText = "select name, agentphone, agentid from queue_agents where state = 'in' order by name";
                        DbReader1 = DbCommand1.ExecuteReader();
                        while (DbReader1.Read())
                        {
                            cmbLogins.Items.Add(DbReader1.GetString(0));
                            usersPhone.Add(DbReader1.GetInt32(1).ToString());
                            usersAgentID.Add(DbReader1.GetInt32(2).ToString());
                        }
                        DbReader1.Close();

                        //MessageBox.Show("Àâòîðèçèðóéòå", );

                        // show autorization
                        Visible = true;

                        return;
                    }
                    DbReader1.Close();
#endif
                }

                        
                lock (_lock)
                {
                    bool bRinging = false;
                    List<string> vForDel = new List<string>();
                    // check new call
                    foreach (var call in qCalls)
                    {
                        if (call.Value.isJoined)
                        {
                            if (call.Value.taskbarCalling == null) // show new calls
                            {
                                call.Value.CreatetbCalling();
                                //call.Value.taskbarCalling.Show("", call.Value.sFIO != "" ? call.Value.sFIO : FormatNumber(call.Value.Number));
                                call.Value.taskbarCalling.Show(call.Value.sFIO, FormatNumber(call.Value.Number));

                                TimerRing.Interval = 100;
                                TimerTray.Interval = 100;
                            }
                            else
                                call.Value.taskbarCalling.Refresh();
                        }
                        // delete calls by check db
                        bool isNotMyCall = false;
                        try
                        {
#if LIGHT
                            DbCommand.CommandText = "select * from Calls where tEndCall is not null and UniqueID = '" + call.Key + "'";
#else
                        
                            DbCommand.CommandText = "select * from Calls where ( (agentID != '' and agentID is not null) or tBeginCall is not null) and agentID != '" + aThisAgentID +
                                        "' and UniqueID = '" + call.Key + "'";
#endif
                            DbReaderWatch = DbCommand.ExecuteReader();
                            isNotMyCall = DbReaderWatch.Read(); // åñëè åñòü çàïèñü î òîì ÷òî äðãîé þçåð âçÿë òðóáêó òî óäàëèì çâîíîê ó òåêóùåãî þçåðà
                            DbReaderWatch.Close();

                        }
                        catch (OdbcException exc)
                        {
                            Trace.Write("Error check Euromed.Calls. Desc:" + exc.Message);
                        }
                

                        // delete calls by event user
                        if ((call.Value.is_del /*&& call.Value.tEndCall != DateTime.MinValue*/) || isNotMyCall) // delete after end of my call or if this call is not my
                        {
                            vForDel.Add(call.Key);
                        }

                        bRinging = call.Value.tIncomingCall != DateTime.MinValue &&
                                    call.Value.tBeginCall == DateTime.MinValue &&
                                    call.Value.tEndCall == DateTime.MinValue;


                    }

                    TimerTray.Enabled = bRinging;
                    TimerRing.Enabled = bRinging;
                    if (!bRinging)
                    {
#if LIGHT
                        Tray.Icon = Properties.Resources.asterisk_red;
#else
                        Tray.Icon = Properties.Resources.asterisk_orange;
#endif
                        player.Stop();
                        TimerTray.Stop();
                        TimerRing.Stop();
                    }
                    else
                    {
                        TimerTray.Start();
                        TimerRing.Start();
                    }
                
                    foreach (var it in vForDel)
                    {
                        qCalls[it].taskbarCalling.Hidden();

                        if (qCalls[it].wndShow)
                        {
                            qCalls[it].wndCalling.isClose = true;
                            qCalls[it].wndCalling.Close();
                        }

                        MovingCalls(qCalls[it].taskbarCalling.Index);
                        qCalls.Remove(it);
                    }

                
                }
            }
            catch (OdbcException exc)
            {
                Tracer.Instance.TraceWL3("Exception: {0}", exc.Message);
                Trace.Write("Error check Euromed.Calls. Desc:" + exc.Message);
            }

        }

        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (new StackTracer("button1_Click", new object[] { sender, e }))
            {
                if (cmbLogins.SelectedIndex >= 0)
                {

                    aThisExt = usersPhone[cmbLogins.SelectedIndex];
                    aThisAgentID = usersAgentID[cmbLogins.SelectedIndex];
                    Tray.Text = "Euromed Asterisk Client [" + cmbLogins.Items[cmbLogins.SelectedIndex].ToString() + " - " + aThisExt + "]";
                    AutorizationSuccess();
                }
                else cmbLogins.Focus();
            }
        }

        private void cmbLogins_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (new StackTracer("cmbLogins_SelectedIndexChanged", new object[] { sender, e }))
            {
                if (cmbLogins.SelectedIndex >= 0)
                {
                    tbExtNumb.Text = usersPhone[cmbLogins.SelectedIndex];
                }
            }
        }

    }
}
