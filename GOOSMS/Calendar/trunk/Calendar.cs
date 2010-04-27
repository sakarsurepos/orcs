using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Calendar;
using System.Resources;
using LumiSoft.Net;
using LumiSoft.Net.Log;
using LumiSoft.Net.MIME;
using LumiSoft.Net.Mail;
using LumiSoft.Net.IMAP;
using LumiSoft.Net.IMAP.Client;
using System.Threading;
using System.Timers;
using System.Deployment;
using System.Deployment.Application;

namespace SampleApp
{
    /// <summary>
    /// Summary description for Calendar.
    /// </summary>
    public class Calendar : System.Windows.Forms.Form
    {
        private System.Windows.Forms.TextBox CalendarURI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Button Go;
        private System.Windows.Forms.MonthCalendar calendarControl;
        private IContainer components;
        private System.Windows.Forms.ListView DayEvents;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private Button button1;
        public int ip = 1;
        public int sub1 = 0;
        public int sub2 = 10;
        public string mainstring;
        public string oldmainstring;
        public string substring;
        public int stringresult;
        public static string pageSource;
        public static string oldpageSource;
        public static string newpageSource;
        public mshtml.HTMLDocument objHtmlDoc;
        public HtmlElement hElement1;
        public HtmlElement hElement2;
        public HtmlElement hElement3;
        Thread vl1;
        Thread vl2;
        delegate void vl1d();
        delegate void vl1d2();
        private ArrayList entryList;
        public string older;
        public string older1;
        public bool first = true;

        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private Button button2;
        private Label labelLastCheck;
        public string newer;
        private Label labelRedeem;
        private Button button4;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private PictureBox pictureBox1;
        private Button button5;
        public string newer1;
        private ListView m_pTabPageMail_Messages;
        private ColumnHeader collumnSubject;
        private ColumnHeader collumnFrom;
        private ColumnHeader collumnReceive;
        private ColumnHeader collumnSize;

        public IMAP_Client imap;// = new IMAP_Client();
        public IMAP_Client_FetchHandler fetchHandler; // = new IMAP_Client_FetchHandler();
        public IMAP_SequenceSet seqSet;

        private ColumnHeader collumnNumber;
        private Label LabelMail;
        private GroupBox groupBox1;
        private Button button6;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private TextBox textBoxPage2;
        private TextBox textBoxPage1;
        private Label label5;
        private Label label4;
        private GroupBox groupBox4;
        private Button button7;
        private Label label7;
        private Label label6;
        private TextBox textBoxStrEnd;
        private TextBox textBoxStrStart;
        private Label label8;
        private TextBox textBoxGmailCheckStr;
        private GroupBox groupBox5;
        private CheckBox checkBox1;
        private ComboBox comboBox1;
        private TextBox textBoxSMS;
        private TextBox textBoxPass;
        private TextBox textBoxOUT2;
        private TextBox textBoxOUT1;
        private TextBox textBoxTime;
        private Label label9;
        private Label labelRes;
        private CheckBox checkBoxGmail;
        private CheckBox checkBoxWebPage;
        private Button button3;
        private Button button9;
        public WebBrowser webBrowser1;
        private Label label11;
        private Label label10;
        private TextBox textBox1;
        private TextBox textBoxBrowerPage;
        private TextBox textBox9;
        private TextBox textBoxWebBrowserStr;
        private Label label14;
        private Label label15;
        private TextBox textBox6;
        private TextBox textBox7;
        private Label label12;
        private Label label13;
        private TextBox textBoxBrowserEd;
        private TextBox textBoxBrowserSt;
        private TextBox textBoxBrowserLogin;
        private TextBox textBoxBrowserPassword;
        private Label label19;
        private Label label18;
        private Label label17;
        private Label label16;
        private Label label22;
        private Label label23;
        private TextBox textBox11;
        private Label label20;
        private Label label21;
        private TextBox textBox12;
        private Button button8;
        private Label label25;
        private TextBox textBoxJSPass;
        private Label label24;
        private TextBox textBoxJSLogin;
        private Label label27;
        private TextBox textBoxJSSubmit;
        private Label label26;
        private TextBox textBoxJSForm;
        private Label labelBrowser;
        private CheckBox checkBoxWebPage4;
        private TextBox textBoxTime2;
        private Button button10;
        public ListViewItem item;

        public Calendar()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            Calendar.CheckForIllegalCrossThreadCalls = false;
            
            //Version Check
            Version version2 = new Version();
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) 
            {
            System.Deployment.Application.ApplicationDeployment ad = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
            version2 = ad.CurrentVersion;
            }

            //Assembly Check
            Version vrs = new Version(Application.ProductVersion);

            this.Text = "FreeLifeSMS WebPage and Gmail Checker / AssemblyBuid " + vrs.Major + "." + vrs.Minor + "." + vrs.Build + "." + vrs.Revision +" Publish Version  " + PublishVersion().ToString() + " ClickOnce RC" + version2.Revision.ToString(); //String.Format("ClickOnce Version {0}.{1}.{2}.{3}", version.Major, version.Minor, version.Revision, version.Build);
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
            this.entryList = new ArrayList();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new Calendar());
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

#region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calendar));
            this.calendarControl = new System.Windows.Forms.MonthCalendar();
            this.CalendarURI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.Go = new System.Windows.Forms.Button();
            this.DayEvents = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.labelLastCheck = new System.Windows.Forms.Label();
            this.labelRedeem = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button5 = new System.Windows.Forms.Button();
            this.m_pTabPageMail_Messages = new System.Windows.Forms.ListView();
            this.collumnNumber = new System.Windows.Forms.ColumnHeader();
            this.collumnSize = new System.Windows.Forms.ColumnHeader();
            this.collumnReceive = new System.Windows.Forms.ColumnHeader();
            this.collumnSubject = new System.Windows.Forms.ColumnHeader();
            this.collumnFrom = new System.Windows.Forms.ColumnHeader();
            this.LabelMail = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.textBoxJSSubmit = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.textBoxJSForm = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.textBoxJSPass = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.textBoxJSLogin = new System.Windows.Forms.TextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBoxBrowserPassword = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBoxWebBrowserStr = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxBrowserEd = new System.Windows.Forms.TextBox();
            this.textBoxBrowserSt = new System.Windows.Forms.TextBox();
            this.textBoxBrowserLogin = new System.Windows.Forms.TextBox();
            this.textBoxBrowerPage = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBoxOUT2 = new System.Windows.Forms.TextBox();
            this.textBoxOUT1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxStrEnd = new System.Windows.Forms.TextBox();
            this.textBoxStrStart = new System.Windows.Forms.TextBox();
            this.textBoxPage2 = new System.Windows.Forms.TextBox();
            this.textBoxPage1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelRes = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxGmailCheckStr = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxTime2 = new System.Windows.Forms.TextBox();
            this.checkBoxWebPage4 = new System.Windows.Forms.CheckBox();
            this.checkBoxGmail = new System.Windows.Forms.CheckBox();
            this.checkBoxWebPage = new System.Windows.Forms.CheckBox();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxSMS = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.labelBrowser = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // calendarControl
            // 
            this.calendarControl.Location = new System.Drawing.Point(9, 16);
            this.calendarControl.Name = "calendarControl";
            this.calendarControl.ShowTodayCircle = false;
            this.calendarControl.TabIndex = 0;
            this.calendarControl.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calendarControl_DateSelected);
            // 
            // CalendarURI
            // 
            this.CalendarURI.Location = new System.Drawing.Point(216, 16);
            this.CalendarURI.Name = "CalendarURI";
            this.CalendarURI.Size = new System.Drawing.Size(427, 20);
            this.CalendarURI.TabIndex = 1;
            this.CalendarURI.Text = "http://www.google.com/calendar/feeds/default/private/full";
            this.CalendarURI.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(178, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL:";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(178, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "User:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(378, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password:";
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(216, 38);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(146, 20);
            this.UserName.TabIndex = 5;
            this.UserName.Text = "kamil.zidek@gmail.com";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(441, 38);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(57, 20);
            this.Password.TabIndex = 6;
            this.Password.Text = "joneson55";
            // 
            // Go
            // 
            this.Go.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Go.Location = new System.Drawing.Point(525, 38);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(118, 20);
            this.Go.TabIndex = 7;
            this.Go.Text = "&Read Data";
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // DayEvents
            // 
            this.DayEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.DayEvents.FullRowSelect = true;
            this.DayEvents.GridLines = true;
            this.DayEvents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.DayEvents.LabelWrap = false;
            this.DayEvents.Location = new System.Drawing.Point(181, 63);
            this.DayEvents.Name = "DayEvents";
            this.DayEvents.Size = new System.Drawing.Size(462, 109);
            this.DayEvents.TabIndex = 8;
            this.DayEvents.UseCompatibleStateImageBehavior = false;
            this.DayEvents.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Event";
            this.columnHeader1.Width = 170;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Author";
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Start";
            this.columnHeader3.Width = 58;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "End";
            this.columnHeader4.Width = 58;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Reminder";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Type";
            this.columnHeader6.Width = 39;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(5, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "AutoChk";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(166, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(64, 20);
            this.button2.TabIndex = 10;
            this.button2.Text = "Fast SMS";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelLastCheck
            // 
            this.labelLastCheck.AutoSize = true;
            this.labelLastCheck.Location = new System.Drawing.Point(70, 94);
            this.labelLastCheck.Name = "labelLastCheck";
            this.labelLastCheck.Size = new System.Drawing.Size(46, 13);
            this.labelLastCheck.TabIndex = 11;
            this.labelLastCheck.Text = "LastChk";
            // 
            // labelRedeem
            // 
            this.labelRedeem.AutoSize = true;
            this.labelRedeem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.labelRedeem.ForeColor = System.Drawing.Color.Red;
            this.labelRedeem.Location = new System.Drawing.Point(60, 20);
            this.labelRedeem.Name = "labelRedeem";
            this.labelRedeem.Size = new System.Drawing.Size(64, 13);
            this.labelRedeem.TabIndex = 13;
            this.labelRedeem.Text = "No Redeem";
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(74, 65);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(48, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "Stop";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "FreeLifeSMS";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItem1.Text = "Restore";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItem2.Text = "Exit";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Calendar.Resource1.sms3;
            this.pictureBox1.Location = new System.Drawing.Point(668, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(9, 19);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(114, 23);
            this.button5.TabIndex = 17;
            this.button5.Text = "Gmail Connect Mail";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // m_pTabPageMail_Messages
            // 
            this.m_pTabPageMail_Messages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.collumnNumber,
            this.collumnSize,
            this.collumnReceive,
            this.collumnSubject,
            this.collumnFrom});
            this.m_pTabPageMail_Messages.FullRowSelect = true;
            this.m_pTabPageMail_Messages.GridLines = true;
            this.m_pTabPageMail_Messages.Location = new System.Drawing.Point(9, 46);
            this.m_pTabPageMail_Messages.Name = "m_pTabPageMail_Messages";
            this.m_pTabPageMail_Messages.Size = new System.Drawing.Size(773, 95);
            this.m_pTabPageMail_Messages.TabIndex = 18;
            this.m_pTabPageMail_Messages.UseCompatibleStateImageBehavior = false;
            this.m_pTabPageMail_Messages.View = System.Windows.Forms.View.Details;
            // 
            // collumnNumber
            // 
            this.collumnNumber.Text = "#";
            this.collumnNumber.Width = 25;
            // 
            // collumnSize
            // 
            this.collumnSize.DisplayIndex = 4;
            this.collumnSize.Text = "Size";
            // 
            // collumnReceive
            // 
            this.collumnReceive.DisplayIndex = 3;
            this.collumnReceive.Text = "Receive";
            this.collumnReceive.Width = 116;
            // 
            // collumnSubject
            // 
            this.collumnSubject.DisplayIndex = 1;
            this.collumnSubject.Text = "From";
            this.collumnSubject.Width = 250;
            // 
            // collumnFrom
            // 
            this.collumnFrom.DisplayIndex = 2;
            this.collumnFrom.Text = "Subject";
            this.collumnFrom.Width = 300;
            // 
            // LabelMail
            // 
            this.LabelMail.AutoSize = true;
            this.LabelMail.Location = new System.Drawing.Point(128, 31);
            this.LabelMail.Name = "LabelMail";
            this.LabelMail.Size = new System.Drawing.Size(72, 13);
            this.LabelMail.TabIndex = 19;
            this.LabelMail.Text = "From+Subject";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UserName);
            this.groupBox1.Controls.Add(this.Go);
            this.groupBox1.Controls.Add(this.Password);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CalendarURI);
            this.groupBox1.Controls.Add(this.calendarControl);
            this.groupBox1.Controls.Add(this.DayEvents);
            this.groupBox1.Location = new System.Drawing.Point(7, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(655, 181);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SMS History";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(6, 40);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(49, 23);
            this.button6.TabIndex = 21;
            this.button6.Text = "Unlock";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.textBoxJSSubmit);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.textBoxJSForm);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.textBoxJSPass);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.textBoxJSLogin);
            this.groupBox2.Controls.Add(this.button8);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.textBox11);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.textBox12);
            this.groupBox2.Controls.Add(this.textBoxBrowserPassword);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.textBox9);
            this.groupBox2.Controls.Add(this.textBoxWebBrowserStr);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.textBox6);
            this.groupBox2.Controls.Add(this.textBox7);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.textBoxBrowserEd);
            this.groupBox2.Controls.Add(this.textBoxBrowserSt);
            this.groupBox2.Controls.Add(this.textBoxBrowserLogin);
            this.groupBox2.Controls.Add(this.textBoxBrowerPage);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.button9);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.textBoxOUT2);
            this.groupBox2.Controls.Add(this.textBoxOUT1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxStrEnd);
            this.groupBox2.Controls.Add(this.textBoxStrStart);
            this.groupBox2.Controls.Add(this.textBoxPage2);
            this.groupBox2.Controls.Add(this.textBoxPage1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(7, 184);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(547, 123);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "WebCheck";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(398, 102);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(42, 13);
            this.label27.TabIndex = 63;
            this.label27.Text = "Submit:";
            // 
            // textBoxJSSubmit
            // 
            this.textBoxJSSubmit.Location = new System.Drawing.Point(441, 99);
            this.textBoxJSSubmit.Name = "textBoxJSSubmit";
            this.textBoxJSSubmit.Size = new System.Drawing.Size(100, 20);
            this.textBoxJSSubmit.TabIndex = 62;
            this.textBoxJSSubmit.Text = "submit";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(264, 102);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(33, 13);
            this.label26.TabIndex = 61;
            this.label26.Text = "Form:";
            // 
            // textBoxJSForm
            // 
            this.textBoxJSForm.Location = new System.Drawing.Point(297, 99);
            this.textBoxJSForm.Name = "textBoxJSForm";
            this.textBoxJSForm.Size = new System.Drawing.Size(100, 20);
            this.textBoxJSForm.TabIndex = 60;
            this.textBoxJSForm.Text = "login-form";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(109, 102);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(56, 13);
            this.label25.TabIndex = 59;
            this.label25.Text = "Password:";
            // 
            // textBoxJSPass
            // 
            this.textBoxJSPass.Location = new System.Drawing.Point(163, 99);
            this.textBoxJSPass.Name = "textBoxJSPass";
            this.textBoxJSPass.Size = new System.Drawing.Size(100, 20);
            this.textBoxJSPass.TabIndex = 58;
            this.textBoxJSPass.Text = "password-password";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(11, 102);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(36, 13);
            this.label24.TabIndex = 57;
            this.label24.Text = "Login:";
            // 
            // textBoxJSLogin
            // 
            this.textBoxJSLogin.Location = new System.Drawing.Point(48, 99);
            this.textBoxJSLogin.Name = "textBoxJSLogin";
            this.textBoxJSLogin.Size = new System.Drawing.Size(61, 20);
            this.textBoxJSLogin.TabIndex = 56;
            this.textBoxJSLogin.Text = "email-email";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(515, 75);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(24, 23);
            this.button8.TabIndex = 55;
            this.button8.Text = "R";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click_1);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(294, 80);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(20, 13);
            this.label22.TabIndex = 54;
            this.label22.Text = "Str";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(294, 58);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(20, 13);
            this.label23.TabIndex = 53;
            this.label23.Text = "Str";
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(249, 55);
            this.textBox11.Name = "textBox11";
            this.textBox11.PasswordChar = '*';
            this.textBox11.Size = new System.Drawing.Size(45, 20);
            this.textBox11.TabIndex = 52;
            this.textBox11.Text = "indiana";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(234, 58);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(17, 13);
            this.label20.TabIndex = 51;
            this.label20.Text = "P:";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(147, 58);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(16, 13);
            this.label21.TabIndex = 50;
            this.label21.Text = "L:";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(162, 55);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(70, 20);
            this.textBox12.TabIndex = 49;
            this.textBox12.Text = "kamil.zidek@gmail.com";
            // 
            // textBoxBrowserPassword
            // 
            this.textBoxBrowserPassword.Location = new System.Drawing.Point(249, 77);
            this.textBoxBrowserPassword.Name = "textBoxBrowserPassword";
            this.textBoxBrowserPassword.PasswordChar = '*';
            this.textBoxBrowserPassword.Size = new System.Drawing.Size(45, 20);
            this.textBoxBrowserPassword.TabIndex = 48;
            this.textBoxBrowserPassword.Text = "indiana";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(234, 80);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(17, 13);
            this.label19.TabIndex = 47;
            this.label19.Text = "P:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(147, 80);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(16, 13);
            this.label18.TabIndex = 46;
            this.label18.Text = "L:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(294, 36);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(20, 13);
            this.label17.TabIndex = 45;
            this.label17.Text = "Str";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(294, 14);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 13);
            this.label16.TabIndex = 44;
            this.label16.Text = "Str";
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(314, 55);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(46, 20);
            this.textBox9.TabIndex = 43;
            // 
            // textBoxWebBrowserStr
            // 
            this.textBoxWebBrowserStr.Location = new System.Drawing.Point(314, 77);
            this.textBoxWebBrowserStr.Name = "textBoxWebBrowserStr";
            this.textBoxWebBrowserStr.Size = new System.Drawing.Size(46, 20);
            this.textBoxWebBrowserStr.TabIndex = 42;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(407, 58);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(20, 13);
            this.label14.TabIndex = 41;
            this.label14.Text = "Ed";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(361, 58);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 13);
            this.label15.TabIndex = 40;
            this.label15.Text = "St";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(427, 55);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(30, 20);
            this.textBox6.TabIndex = 39;
            this.textBox6.Text = "262";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(377, 55);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(30, 20);
            this.textBox7.TabIndex = 38;
            this.textBox7.Text = "0";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(407, 80);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(20, 13);
            this.label12.TabIndex = 37;
            this.label12.Text = "Ed";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(361, 80);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 13);
            this.label13.TabIndex = 36;
            this.label13.Text = "St";
            // 
            // textBoxBrowserEd
            // 
            this.textBoxBrowserEd.Location = new System.Drawing.Point(427, 77);
            this.textBoxBrowserEd.Name = "textBoxBrowserEd";
            this.textBoxBrowserEd.Size = new System.Drawing.Size(30, 20);
            this.textBoxBrowserEd.TabIndex = 35;
            this.textBoxBrowserEd.Text = "50";
            // 
            // textBoxBrowserSt
            // 
            this.textBoxBrowserSt.Location = new System.Drawing.Point(377, 77);
            this.textBoxBrowserSt.Name = "textBoxBrowserSt";
            this.textBoxBrowserSt.Size = new System.Drawing.Size(30, 20);
            this.textBoxBrowserSt.TabIndex = 34;
            this.textBoxBrowserSt.Text = "2412";
            // 
            // textBoxBrowserLogin
            // 
            this.textBoxBrowserLogin.Location = new System.Drawing.Point(162, 77);
            this.textBoxBrowserLogin.Name = "textBoxBrowserLogin";
            this.textBoxBrowserLogin.Size = new System.Drawing.Size(70, 20);
            this.textBoxBrowserLogin.TabIndex = 33;
            this.textBoxBrowserLogin.Text = "kamil.zidek@gmail.com";
            // 
            // textBoxBrowerPage
            // 
            this.textBoxBrowerPage.Location = new System.Drawing.Point(48, 77);
            this.textBoxBrowerPage.Name = "textBoxBrowerPage";
            this.textBoxBrowerPage.Size = new System.Drawing.Size(100, 20);
            this.textBoxBrowerPage.TabIndex = 32;
            this.textBoxBrowerPage.Text = "http://lockerz.com";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(48, 55);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 31;
            this.textBox1.Text = "http://lockerz.com";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 80);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(39, 13);
            this.label11.TabIndex = 30;
            this.label11.Text = "Web4:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 58);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(39, 13);
            this.label10.TabIndex = 29;
            this.label10.Text = "Web3:";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(458, 75);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(58, 23);
            this.button9.TabIndex = 28;
            this.button9.Text = "Browser";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(458, 53);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(81, 23);
            this.button3.TabIndex = 26;
            this.button3.Text = "Webrequest ";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_2);
            // 
            // textBoxOUT2
            // 
            this.textBoxOUT2.Location = new System.Drawing.Point(314, 33);
            this.textBoxOUT2.Name = "textBoxOUT2";
            this.textBoxOUT2.Size = new System.Drawing.Size(127, 20);
            this.textBoxOUT2.TabIndex = 24;
            // 
            // textBoxOUT1
            // 
            this.textBoxOUT1.Location = new System.Drawing.Point(313, 11);
            this.textBoxOUT1.Name = "textBoxOUT1";
            this.textBoxOUT1.Size = new System.Drawing.Size(226, 20);
            this.textBoxOUT1.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(488, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Ed";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(441, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "St";
            // 
            // textBoxStrEnd
            // 
            this.textBoxStrEnd.Location = new System.Drawing.Point(509, 34);
            this.textBoxStrEnd.Name = "textBoxStrEnd";
            this.textBoxStrEnd.Size = new System.Drawing.Size(30, 20);
            this.textBoxStrEnd.TabIndex = 20;
            this.textBoxStrEnd.Text = "262";
            // 
            // textBoxStrStart
            // 
            this.textBoxStrStart.Location = new System.Drawing.Point(458, 34);
            this.textBoxStrStart.Name = "textBoxStrStart";
            this.textBoxStrStart.Size = new System.Drawing.Size(30, 20);
            this.textBoxStrStart.TabIndex = 19;
            this.textBoxStrStart.Text = "0";
            // 
            // textBoxPage2
            // 
            this.textBoxPage2.Location = new System.Drawing.Point(47, 33);
            this.textBoxPage2.Name = "textBoxPage2";
            this.textBoxPage2.Size = new System.Drawing.Size(247, 20);
            this.textBoxPage2.TabIndex = 18;
            this.textBoxPage2.Text = "http://lockerzchecker.ismywebsite.com/index.php";
            // 
            // textBoxPage1
            // 
            this.textBoxPage1.Location = new System.Drawing.Point(46, 11);
            this.textBoxPage1.Name = "textBoxPage1";
            this.textBoxPage1.Size = new System.Drawing.Size(248, 20);
            this.textBoxPage1.TabIndex = 17;
            this.textBoxPage1.Text = "http://www.kaar.sebsoft.com/";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Web2:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Web1:";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(16, 460);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(780, 352);
            this.webBrowser1.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Time";
            // 
            // textBoxTime
            // 
            this.textBoxTime.Location = new System.Drawing.Point(31, 91);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.Size = new System.Drawing.Size(14, 20);
            this.textBoxTime.TabIndex = 25;
            this.textBoxTime.Text = "1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelRes);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBoxGmailCheckStr);
            this.groupBox3.Controls.Add(this.m_pTabPageMail_Messages);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.LabelMail);
            this.groupBox3.Location = new System.Drawing.Point(7, 307);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(789, 147);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Gmail check with SMS Notification";
            // 
            // labelRes
            // 
            this.labelRes.AutoSize = true;
            this.labelRes.Location = new System.Drawing.Point(129, 12);
            this.labelRes.Name = "labelRes";
            this.labelRes.Size = new System.Drawing.Size(37, 13);
            this.labelRes.TabIndex = 22;
            this.labelRes.Text = "Result";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(573, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Subject Check String";
            // 
            // textBoxGmailCheckStr
            // 
            this.textBoxGmailCheckStr.Location = new System.Drawing.Point(685, 9);
            this.textBoxGmailCheckStr.Name = "textBoxGmailCheckStr";
            this.textBoxGmailCheckStr.Size = new System.Drawing.Size(100, 20);
            this.textBoxGmailCheckStr.TabIndex = 20;
            this.textBoxGmailCheckStr.Text = "ockerz";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxTime2);
            this.groupBox4.Controls.Add(this.checkBoxWebPage4);
            this.groupBox4.Controls.Add(this.checkBoxGmail);
            this.groupBox4.Controls.Add(this.checkBoxWebPage);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.textBoxTime);
            this.groupBox4.Controls.Add(this.textBoxPass);
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Controls.Add(this.button4);
            this.groupBox4.Controls.Add(this.labelRedeem);
            this.groupBox4.Controls.Add(this.button6);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.labelLastCheck);
            this.groupBox4.Location = new System.Drawing.Point(668, 35);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(128, 148);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Lockerz";
            // 
            // textBoxTime2
            // 
            this.textBoxTime2.Location = new System.Drawing.Point(46, 91);
            this.textBoxTime2.Name = "textBoxTime2";
            this.textBoxTime2.Size = new System.Drawing.Size(24, 20);
            this.textBoxTime2.TabIndex = 29;
            this.textBoxTime2.Text = "15";
            // 
            // checkBoxWebPage4
            // 
            this.checkBoxWebPage4.AutoSize = true;
            this.checkBoxWebPage4.Checked = true;
            this.checkBoxWebPage4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWebPage4.Location = new System.Drawing.Point(5, 128);
            this.checkBoxWebPage4.Name = "checkBoxWebPage4";
            this.checkBoxWebPage4.Size = new System.Drawing.Size(57, 17);
            this.checkBoxWebPage4.TabIndex = 29;
            this.checkBoxWebPage4.Text = "Page4";
            this.checkBoxWebPage4.UseVisualStyleBackColor = true;
            // 
            // checkBoxGmail
            // 
            this.checkBoxGmail.AutoSize = true;
            this.checkBoxGmail.Checked = true;
            this.checkBoxGmail.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxGmail.Location = new System.Drawing.Point(70, 112);
            this.checkBoxGmail.Name = "checkBoxGmail";
            this.checkBoxGmail.Size = new System.Drawing.Size(52, 17);
            this.checkBoxGmail.TabIndex = 28;
            this.checkBoxGmail.Text = "Gmail";
            this.checkBoxGmail.UseVisualStyleBackColor = true;
            // 
            // checkBoxWebPage
            // 
            this.checkBoxWebPage.AutoSize = true;
            this.checkBoxWebPage.Location = new System.Drawing.Point(5, 112);
            this.checkBoxWebPage.Name = "checkBoxWebPage";
            this.checkBoxWebPage.Size = new System.Drawing.Size(66, 17);
            this.checkBoxWebPage.TabIndex = 27;
            this.checkBoxWebPage.Text = "Page1,2";
            this.checkBoxWebPage.UseVisualStyleBackColor = true;
            // 
            // textBoxPass
            // 
            this.textBoxPass.Location = new System.Drawing.Point(58, 41);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.PasswordChar = '?';
            this.textBoxPass.Size = new System.Drawing.Size(64, 20);
            this.textBoxPass.TabIndex = 22;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(6, 15);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(54, 23);
            this.button7.TabIndex = 14;
            this.button7.Text = "Lockerz";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxSMS);
            this.groupBox5.Controls.Add(this.comboBox1);
            this.groupBox5.Controls.Add(this.checkBox1);
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Location = new System.Drawing.Point(560, 184);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(236, 100);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "SMS";
            // 
            // textBoxSMS
            // 
            this.textBoxSMS.Location = new System.Drawing.Point(6, 38);
            this.textBoxSMS.Multiline = true;
            this.textBoxSMS.Name = "textBoxSMS";
            this.textBoxSMS.Size = new System.Drawing.Size(223, 56);
            this.textBoxSMS.TabIndex = 12;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Hedka",
            "Marek",
            "Oco",
            "Mamka",
            "Denka",
            "Matka",
            "Fodor",
            "Zupa",
            "Feriancik"});
            this.comboBox1.Location = new System.Drawing.Point(73, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(91, 21);
            this.comboBox1.TabIndex = 11;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(6, 16);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(68, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "SMS ON";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // labelBrowser
            // 
            this.labelBrowser.AutoSize = true;
            this.labelBrowser.Location = new System.Drawing.Point(563, 287);
            this.labelBrowser.Name = "labelBrowser";
            this.labelBrowser.Size = new System.Drawing.Size(62, 13);
            this.labelBrowser.TabIndex = 28;
            this.labelBrowser.Text = "CompareStr";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(741, 6);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(55, 23);
            this.button10.TabIndex = 23;
            this.button10.Text = "Update";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // Calendar
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(802, 817);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.labelBrowser);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Calendar";
            this.Text = "FreeLifeSMS WebPage and Gmail Checker";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Calendar_FormClosing);
            this.Resize += new System.EventHandler(this.Calendar_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
#endregion

        private void Go_Click(object sender, System.EventArgs e)
        {
            RefreshFeed(); 
        }


        private void RefreshFeed() 
        {
            string calendarURI = this.CalendarURI.Text;
            string userName =    this.UserName.Text;
            string passWord =    this.Password.Text;

            this.entryList = new ArrayList(50); 
            ArrayList dates = new ArrayList(50); 
            EventQuery query = new EventQuery();
            CalendarService service = new CalendarService("CalendarSampleApp");

            if (userName != null && userName.Length > 0)
            {
                service.setUserCredentials(userName, passWord);
            }

            // only get event's for today - 1 month until today + 1 year

            query.Uri = new Uri(calendarURI);

            query.StartTime = DateTime.Now.AddDays(-28); 
            query.EndTime = DateTime.Now.AddMonths(6);


            EventFeed calFeed = service.Query(query) as EventFeed;

            // now populate the calendar
            while (calFeed != null && calFeed.Entries.Count > 0)
            {
                // look for the one with dinner time...
                foreach (EventEntry entry in calFeed.Entries)
                {
                    this.entryList.Add(entry); 
                    if (entry.Times.Count > 0)
                    {
                        foreach (When w in entry.Times) 
                        {
                            dates.Add(w.StartTime); 
                        }
                    }
                }
                // just query the same query again.
                if (calFeed.NextChunk != null)
                {
                    query.Uri = new Uri(calFeed.NextChunk); 
                    calFeed = service.Query(query) as EventFeed;
                }
                else
                    calFeed = null;
            }

            DateTime[] aDates = new DateTime[dates.Count]; 

            int i =0; 
            foreach (DateTime d in dates) 
            {
                aDates[i++] = d; 
            }


            this.calendarControl.BoldedDates = aDates;
        }


        private void calendarControl_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {

            this.DayEvents.Items.Clear();

            ArrayList results = new ArrayList(5); 
            foreach (EventEntry entry in this.entryList) 
            {
                // let's find the entries for that date

                if (entry.Times.Count > 0)
                {
                    foreach (When w in entry.Times) 
                    {
                        if (e.Start.Date == w.StartTime.Date ||
                            e.Start.Date == w.EndTime.Date)
                        {
                            results.Add(entry); 
                            break;
                        }
                    }
                }
            }

            foreach (EventEntry entry in results) 
            {
                ListViewItem item = new ListViewItem(entry.Title.Text); 
                item.SubItems.Add(entry.Authors[0].Name); 
                if (entry.Times.Count > 0)
                {
                    item.SubItems.Add(entry.Times[0].StartTime.TimeOfDay.ToString()); 
                    item.SubItems.Add(entry.Times[0].EndTime.TimeOfDay.ToString()); 
                    item.SubItems.Add(entry.Times[0].Reminders.Count.ToString()); // .GetType());
                    //item.SubItems.Add(entry.Reminder.Method.ToString());
                }

                this.DayEvents.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                first = true;
                button4.Enabled = true;
                button1.Enabled = false;
                //Start Check
                    try
                    {
                //GMAIL
                if (checkBoxGmail.Checked == true) 
                {
                    gmailcheck();
                    oldmainstring = mainstring;
                }
                //PAGE4
                if (checkBoxWebPage4.Checked == true)
                {
                    Page4checkfcn();
                }

                //PAGE1,2
                if (checkBoxWebPage.Checked == true)
                {

                    //////////////////////////
                    //Check 0 original Lockerz

                        // Create a request for the URL. 	PAGE1	
                        WebRequest request = WebRequest.Create(textBoxPage1.Text); //http://ptzplace.lockerz.com/
                        // If required by the server, set the credentials.
                        request.Credentials = CredentialCache.DefaultCredentials;
                        // Get the response.
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        // Display the status.
                        Console.WriteLine(response.StatusDescription);
                        // Get the stream containing content returned by the server.
                        Stream dataStream = response.GetResponseStream();
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader = new StreamReader(dataStream);
                        // Read the content.
                        string responseFromServer = reader.ReadToEnd();
                        // Save to newer.
                        older = responseFromServer;
                        // Cleanup the streams and the response.
                        reader.Close();
                        dataStream.Close();
                        response.Close();
                        textBoxOUT1.Text = older;

                        //////////////////////////
                        //Check 1 LockerzNews PAGE2

                        // Create a request for the URL. 		
                        WebRequest request1 = WebRequest.Create(textBoxPage2.Text);
                        // If required by the server, set the credentials.
                        request1.Credentials = CredentialCache.DefaultCredentials;
                        // Get the response.
                        HttpWebResponse response1 = (HttpWebResponse)request1.GetResponse();
                        // Display the status.
                        Console.WriteLine(response1.StatusDescription);
                        // Get the stream containing content returned by the server.
                        Stream dataStream1 = response1.GetResponseStream();
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader1 = new StreamReader(dataStream1);
                        // Read the content.
                        string responseFromServer1 = reader1.ReadToEnd();
                        // Save to newer.
                        older1 = responseFromServer1;
                        // Cleanup the streams and the response.
                        reader1.Close();
                        dataStream1.Close();
                        response1.Close();
                        older1 = older1.Substring(int.Parse(textBoxStrStart.Text), int.Parse(textBoxStrEnd.Text));
                        textBoxOUT2.Text = older1;
                    }
                    
                }
                    catch
                    {
                        MessageBox.Show("WebPage NotExist or No connection");
                        //timer50.Stop();
                        button4.Enabled = false;
                        button1.Enabled = true;
                    }
        vl2 = new Thread(vl2fcn);
        vl2.Start();    
        }

        public void googlecalendarSMSreminder(string sendstring)
        {
            CalendarService service = new CalendarService("exampleCo-exampleApp-1");
            service.setUserCredentials("kamil.zidek@gmail.com", "joneson55");

            EventEntry entry = new EventEntry();

            // Set the title and content of the entry.
            entry.Title.Text = sendstring;
            entry.Content.Content = "Lockerz Login Page Check.";
            // Set a location for the event.
            Where eventLocation = new Where();
            eventLocation.ValueString = "Lockerz Login";
            entry.Locations.Add(eventLocation);

            When eventTime = new When(DateTime.Now.AddMinutes(3), DateTime.Now.AddHours(1));
            entry.Times.Add(eventTime);

            if (checkBox1.Checked == true)  //Reminder ON/OFF
            {
                //Add SMS Reminder
                Reminder fiftyMinReminder = new Reminder();
                fiftyMinReminder.Minutes = 1;
                fiftyMinReminder.Method = Reminder.ReminderMethod.sms;
                entry.Reminders.Add(fiftyMinReminder);
            }
            else
            {
            }

            Uri postUri = new Uri("http://www.google.com/calendar/feeds/default/private/full");

            // Send the request and receive the response:
            AtomEntry insertedEntry = service.Insert(postUri, entry);
        }

        public void vl2fcn()
        {
            int incr = 0;
            while (true)
            {
                incr = incr + 1;
                Thread.Sleep(int.Parse(textBoxTime.Text) * 1000 * 60);
                    
                    //Page4 Check
                    if (checkBoxWebPage4.Checked == true)
                    {
                        if (incr == int.Parse(textBoxTime2.Text))
                        {
                            Page4checkfcn();
                            Thread.Sleep(10000);
                            //int x = pageSource.IndexOf("Featured Episodes");
                            //newpageSource = pageSource;
                            labelBrowser.Text = newpageSource;
                            int fail = pageSource.IndexOf("download");

                            if (oldpageSource != newpageSource && fail == -1) // && oldpageSource != null)
                            {
                                oldpageSource = newpageSource;
                                googlecalendarSMSreminder(newpageSource);
                            }
                            incr = 0;
                        }
                    }

                    //Gmail Check
                    if (checkBoxGmail.Checked == true)
                    {
                        //button5_Click(sender, e);
                        ip = 1;
                        imap = new IMAP_Client();
                        fetchHandler = new IMAP_Client_FetchHandler();
                        try
                        {
                            m_pTabPageMail_Messages.Items.Clear();
                            imap.Connect("imap.gmail.com", 993, true);
                            imap.Login(UserName.Text, Password.Text);
                            imap.SelectFolder("INBOX");
                            LoadMessages();
                            LabelMail.Text = m_pTabPageMail_Messages.Items[ip - 2].SubItems[3].Text + m_pTabPageMail_Messages.Items[ip - 2].SubItems[4].Text;
                            mainstring = m_pTabPageMail_Messages.Items[ip - 2].SubItems[3].Text + m_pTabPageMail_Messages.Items[ip - 2].SubItems[4].Text;
                            substring = textBoxGmailCheckStr.Text;
                            stringresult = mainstring.IndexOf(substring);
                            imap.CloseFolder();
                            imap.UnsubscribeFolder("INBOX");
                            imap.Disconnect();
                            imap.Dispose();

                            if (stringresult != -1)
                            {
                                labelRes.Text = "Found";
                            }
                            else
                            {
                                labelRes.Text = "None";
                            }
                        }
                        catch
                        {
                            MessageBox.Show("No internet Connection");
                        }

                        if (mainstring != oldmainstring && stringresult != -1)
                        {
                            googlecalendarSMSreminder(m_pTabPageMail_Messages.Items[ip - 2].SubItems[4].Text);       
                            oldmainstring = mainstring;
                        }

                    }

                    //WebPage Check 1,2
                    if (checkBoxWebPage.Checked == true)
                    {
                        //////////////////////////
                        //Check 0 original Lockerz PAGE1

                        // Create a request for the URL. 		
                        WebRequest request3 = WebRequest.Create(textBoxPage1.Text); //http://ptzplace.lockerz.com/
                        // If required by the server, set the credentials.
                        request3.Credentials = CredentialCache.DefaultCredentials;
                        // Get the response.
                        HttpWebResponse response3 = (HttpWebResponse)request3.GetResponse();
                        // Display the status.
                        Console.WriteLine(response3.StatusDescription);
                        // Get the stream containing content returned by the server.
                        Stream dataStream3 = response3.GetResponseStream();
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader3 = new StreamReader(dataStream3);
                        // Read the content.
                        string responseFromServer3 = reader3.ReadToEnd();
                        // Save to newer.
                        newer = responseFromServer3;
                        // Cleanup the streams and the response.
                        reader3.Close();
                        dataStream3.Close();
                        response3.Close();

                        //////////////////////////
                        //Check 1 Lockerznews PAGE2

                        // Create a request for the URL. 		
                        WebRequest request4 = WebRequest.Create(textBoxPage2.Text);
                        // If required by the server, set the credentials.
                        request4.Credentials = CredentialCache.DefaultCredentials;
                        // Get the response.
                        HttpWebResponse response4 = (HttpWebResponse)request4.GetResponse();
                        // Display the status.
                        Console.WriteLine(response4.StatusDescription);
                        // Get the stream containing content returned by the server.
                        Stream dataStream4 = response4.GetResponseStream();
                        // Open the stream using a StreamReader for easy access.
                        StreamReader reader4 = new StreamReader(dataStream4);
                        // Read the content.
                        string responseFromServer4 = reader4.ReadToEnd();
                        // Save to newer.
                        newer1 = responseFromServer4;
                        // Cleanup the streams and the response.
                        reader4.Close();
                        dataStream4.Close();
                        response4.Close();
                        newer1 = newer1.Substring(int.Parse(textBoxStrStart.Text), int.Parse(textBoxStrEnd.Text));

                        // Compare String
                        if (older != newer || older1 != newer1)
                        {
                            googlecalendarSMSreminder("Redemption begins");
                            // Save to older.
                            older = newer;
                            older1 = newer1;

                            labelRedeem.Text = "Reedem started";
                            labelRedeem.ForeColor = Color.Green;

                            //Notify Icon Change
                            notifyIcon1.Icon = SystemIcons.Exclamation;
                            notifyIcon1.BalloonTipTitle = "Lockerz";
                            notifyIcon1.BalloonTipText = "Redemption Start.";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                            //this.Click += new EventHandler(Form1_Click);
                            notifyIcon1.ShowBalloonTip(30);
                        }
                    }
                labelLastCheck.Text = DateTime.Now.TimeOfDay.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            googlecalendarSMSreminder("TestSMS");
            MessageBox.Show("SMS will be send in 3 Minutes");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxPage1.Text = "http://www.kaar.sebsoft.com/";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button1.Enabled = true;
            vl1.Abort();
            vl2.Abort();         
        }

        private void Calendar_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        //GMail Check fcn
        void gmailcheck()
        {
            ip = 1;
            imap = new IMAP_Client();
            fetchHandler = new IMAP_Client_FetchHandler();
            try
            {
                m_pTabPageMail_Messages.Items.Clear();
                imap.Connect("imap.gmail.com", 993, true);
                imap.Login(UserName.Text, Password.Text);
                imap.SelectFolder("INBOX");
                LoadMessages();
                LabelMail.Text = m_pTabPageMail_Messages.Items[ip - 2].SubItems[3].Text + m_pTabPageMail_Messages.Items[ip - 2].SubItems[4].Text;
                mainstring = m_pTabPageMail_Messages.Items[ip - 2].SubItems[3].Text + m_pTabPageMail_Messages.Items[ip - 2].SubItems[4].Text;
                substring = textBoxGmailCheckStr.Text;
                stringresult = mainstring.IndexOf(substring);
                imap.CloseFolder();
                imap.UnsubscribeFolder("INBOX");
                imap.Disconnect();
                imap.Dispose();

                if (stringresult != -1)
                {
                    labelRes.Text = "Found";
                }
                else
                {
                    labelRes.Text = "None";
                }
            }
            catch
            {
                MessageBox.Show("No internet Connection");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            gmailcheck();
        }
 
        public void LoadMessages()
        {
            //item = m_pTabPageMail_Messages;
            
            
            fetchHandler.Envelope += new EventHandler<EventArgs<IMAP_Envelope>>(delegate(object s, EventArgs<IMAP_Envelope> e)
            {
                IMAP_Envelope envelope = e.Value;

                string from = "";
                if (envelope.From != null)
                {
                    for (int i = 0; i < envelope.From.Length; i++)
                    {
                        // Don't add ; for last item
                        if (i == envelope.From.Length - 1)
                        {
                            from += envelope.From[i].ToString();
                        }
                        else
                        {
                            from += envelope.From[i].ToString() + ";";
                        }
                    }
                }
                else
                {
                    from = "<none>";
                }
                
                item.SubItems.Add(from);
                item.SubItems.Add(envelope.Subject != null ? envelope.Subject : "<none>");
                //m_pTabPageMail_Messages.Items[0].Text = from;
                //m_pTabPageMail_Messages.Items[1].Text = envelope.Subject != null ? envelope.Subject : "<none>";
            });

            fetchHandler.Flags += new EventHandler<EventArgs<string[]>>(delegate(object s, EventArgs<string[]> e)
            {
            });

            fetchHandler.InternalDate += new EventHandler<EventArgs<DateTime>>(delegate(object s, EventArgs<DateTime> e)
            {

                //item = m_pTabPageMail_Messages.Items.Add("x");
                item.SubItems.Add(e.Value.ToString());
                //m_pTabPageMail_Messages.Items[2].Text = e.Value.ToString();
            });

            fetchHandler.Rfc822Size += new EventHandler<EventArgs<int>>(delegate(object s, EventArgs<int> e)
            {
                item = m_pTabPageMail_Messages.Items.Add((ip++).ToString());
                //item = m_pTabPageMail_Messages.Items.Add("x1");
                item.SubItems.Add(((decimal)(e.Value / (decimal)1000)).ToString("f2") + " kb");
                //m_pTabPageMail_Messages.Items[3].Text = ((decimal)(e.Value / (decimal)1000)).ToString("f2") + " kb";
            });

            fetchHandler.UID += new EventHandler<EventArgs<long>>(delegate(object s, EventArgs<long> e)
            {
                m_pTabPageMail_Messages.Tag = e.Value;
            });

            seqSet = new IMAP_SequenceSet();
            seqSet.Parse("5:*");
            imap.Fetch(false, seqSet, new IMAP_Fetch_DataItem[]{
                        new IMAP_Fetch_DataItem_Envelope(),
                        new IMAP_Fetch_DataItem_Flags(),
                        new IMAP_Fetch_DataItem_InternalDate(),
                        new IMAP_Fetch_DataItem_Rfc822Size(),
                        new IMAP_Fetch_DataItem_Uid()
                    },
                fetchHandler
            );
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBoxPage1.Text = "http://ptzplace.lockerz.com/"; 
            textBoxPage2.Text = "http://lockerzchecker.ismywebsite.com/index.php";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(textBoxPass.Text == "joneson56")
            {
            textBoxPass.Text = "";
            checkBox1.Enabled = true;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ip = 1;
            imap.Connect("imap.gmail.com", 993, true);
            //imap.SelectFolder("INBOX");
            m_pTabPageMail_Messages.Items.Clear();
            LoadMessages();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            string strLoginName = "kamil.zidek@gmail.com";
            string strPassword = "indiana";
            
            try
            {
            string strURL = "http://lockerz.com";
            string strPostData = String.Format("email-email={0}@password-password={1}",
            strLoginName.Trim(), strPassword.Trim());

            // Setup the http request.
            HttpWebRequest webReq = WebRequest.Create(strURL) as HttpWebRequest;
            webReq.Method = "POST";
            webReq.KeepAlive = true;
            //webReq.AllowAutoRedirect = true;
            //webReq.CookieContainer = cookie;
            //webReq.ContentType = "multipart/form-data; boundary=" + boundary;
            //webReq.ContentLength = buffer.Length;
            webReq.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0)";
            webReq.Headers.Set("Accept-Language", "en-us");
            webReq.ContentLength = strPostData.Length;
            webReq.ContentType = "application/x-www-form-urlencoded";
            webReq.AllowAutoRedirect = true;
            webReq.CookieContainer = new CookieContainer();

            // Post to the login form.
            StreamWriter streamRequestWriter = new StreamWriter(webReq.GetRequestStream());
            streamRequestWriter.Write(strPostData);
            streamRequestWriter.Close();

            // Get the response.
            HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();

            // Have some cookies.
            CookieCollection cookieCollection = webResp.Cookies;

            // Read the response

            Stream datastream = webResp.GetResponseStream();
            StreamReader reader = new StreamReader(datastream);
            String strResponseFromServer = reader.ReadToEnd();
            //int x;
            //x =3;
            //    x=x;
            }
            catch
            {
            }
        }

        //Start Page4 Check Thread
        public void Page4checkfcn()
        {
            webBrowser1.Navigate(textBoxBrowerPage.Text);
            vl1 = new Thread(vl1fcn);
            vl1.IsBackground = false;
            vl1.Start();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Page4checkfcn();
        }

        public void vl1fcn()
        {
            try
            {
                Thread.Sleep(9000);
                this.Invoke(new vl1d(vl1f));
                Thread.Sleep(13000);
                this.Invoke(new vl1d2(vl1f2));
            }
            catch { }
        }

        public void vl1f() //Insert values
        {
            hElement1 = webBrowser1.Document.GetElementById(textBoxJSLogin.Text);
            hElement1.SetAttribute("value", textBoxBrowserLogin.Text);
            hElement2 = webBrowser1.Document.GetElementById(textBoxJSPass.Text);
            hElement2.SetAttribute("value", textBoxBrowserPassword.Text);
            hElement3 = webBrowser1.Document.GetElementById(textBoxJSForm.Text);
            hElement3.InvokeMember(textBoxJSSubmit.Text);

        }
        public void vl1f2() //Get string
        {
            objHtmlDoc = (mshtml.HTMLDocument)webBrowser1.Document.DomDocument;
            /*webBrowser1 is the WebBrowser Control showing your page*/
            pageSource = objHtmlDoc.documentElement.innerHTML;
            textBoxWebBrowserStr.Text = pageSource;
            pageSource = pageSource.Substring(int.Parse(textBoxBrowserSt.Text), int.Parse(textBoxBrowserEd.Text));
            labelBrowser.Text = pageSource;
            if (first == true)
            {
                oldpageSource = pageSource;
                newpageSource = pageSource;
            }
            else
            {
                newpageSource = pageSource;
            }
            first = false;
        }
       
        private void button8_Click_1(object sender, EventArgs e)
        {
            //webBrowser1.Refresh();
            webBrowser1.Navigate("http://www.lockerz.com/myLocker");
            objHtmlDoc = (mshtml.HTMLDocument)webBrowser1.Document.DomDocument;
            /*webBrowser1 is the WebBrowser Control showing your page*/
            pageSource = objHtmlDoc.documentElement.innerHTML;
            textBoxWebBrowserStr.Text = pageSource;
            //pageSource = pageSource.Substring(int.Parse(textBoxBrowserSt.Text), int.Parse(textBoxBrowserEd.Text));
            pageSource = pageSource.Substring(int.Parse(textBoxBrowserSt.Text), int.Parse(textBoxBrowserEd.Text));
            //int x = pageSource.IndexOf("Featured Episodes");
            labelBrowser.Text = pageSource;
        }

        private void Calendar_FormClosing(object sender, FormClosingEventArgs e)
        {
            //vl1.Abort();
            //vl2.Abort();   
        }

        public string PublishVersion()
        {

            System.Reflection.Assembly _assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();
            string ourVersion = string.Empty;

            //if running the deployed application, you can get the version
            //  from the ApplicationDeployment information. If you try
            //  to access this when you are running in Visual Studio, it will not work.
            if(System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                ourVersion = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            else
            {
                if (_assemblyInfo != null)
                {
                    ourVersion = _assemblyInfo.GetName().Version.ToString();
                }
            }
            return ourVersion;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ApplicationDeployment deploy = ApplicationDeployment.CurrentDeployment;
            UpdateCheckInfo update = deploy.CheckForDetailedUpdate();
            if (deploy.CheckForUpdate())
            {
                MessageBox.Show("You can update to version: " + update.AvailableVersion.ToString());
                deploy.Update();
                Application.Restart();
            }
            else
            {
                MessageBox.Show("No update avaliable");
            }
        }

    }
}
