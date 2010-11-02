using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Calendar;

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
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.ListView DayEvents;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private Button button1;
        private TextBox textBox1;
        private Label label4;
        private Label label5;
        private Label label6;
        private ListBox listBox1;
        private Label label7;
        private Label label8;


        private ArrayList entryList; 

        public Calendar()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // calendarControl
            // 
            this.calendarControl.Location = new System.Drawing.Point(7, 8);
            this.calendarControl.Name = "calendarControl";
            this.calendarControl.ShowTodayCircle = false;
            this.calendarControl.TabIndex = 0;
            this.calendarControl.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calendarControl_DateSelected);
            // 
            // CalendarURI
            // 
            this.CalendarURI.Location = new System.Drawing.Point(293, 8);
            this.CalendarURI.Name = "CalendarURI";
            this.CalendarURI.Size = new System.Drawing.Size(287, 20);
            this.CalendarURI.TabIndex = 1;
            this.CalendarURI.Text = "http://www.google.com/calendar/feeds/default/private/full";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(239, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(239, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "User:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(239, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password:";
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(293, 31);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(287, 20);
            this.UserName.TabIndex = 5;
            this.UserName.Text = "sample@gmail.com";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(293, 54);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(115, 20);
            this.Password.TabIndex = 6;
            // 
            // Go
            // 
            this.Go.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Go.Location = new System.Drawing.Point(414, 53);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(166, 21);
            this.Go.TabIndex = 7;
            this.Go.Text = "Read Calendar Data";
            this.Go.Click += new System.EventHandler(this.Go_Click);
            // 
            // DayEvents
            // 
            this.DayEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.DayEvents.FullRowSelect = true;
            this.DayEvents.GridLines = true;
            this.DayEvents.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.DayEvents.LabelWrap = false;
            this.DayEvents.Location = new System.Drawing.Point(242, 85);
            this.DayEvents.Name = "DayEvents";
            this.DayEvents.Size = new System.Drawing.Size(338, 84);
            this.DayEvents.TabIndex = 8;
            this.DayEvents.UseCompatibleStateImageBehavior = false;
            this.DayEvents.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Event";
            this.columnHeader1.Width = 200;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Author";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Start";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "End";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(497, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Send SMS";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(242, 192);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(249, 55);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "test text";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(510, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "9";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(239, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Text SMS:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "User List:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            "User1",
            "User2",
            "User3",
            "User4"});
            this.listBox1.Location = new System.Drawing.Point(15, 192);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(219, 56);
            this.listBox1.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(535, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "z 57";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(505, 191);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Char Count:";
            // 
            // Calendar
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(595, 259);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.DayEvents);
            this.Controls.Add(this.Go);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CalendarURI);
            this.Controls.Add(this.calendarControl);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "Calendar";
            this.Text = "Google SMS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
#endregion

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }

        private void Go_Click(object sender, System.EventArgs e)
        {
            try
            {
                RefreshFeed();
            }
            catch
            {
                MessageBox.Show("zle pripojenie ku gmailu");
            }
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
                }

                this.DayEvents.Items.Add(item);
            }
        }

        public void googlecalendarSMSreminder(string sendstring)
        {
            CalendarService service = new CalendarService("exampleCo-exampleApp-1");
            service.setUserCredentials(UserName.Text, Password.Text);

            EventEntry entry = new EventEntry();

            // Set the title and content of the entry.
            entry.Title.Text = sendstring;
            entry.Content.Content = "Nadpis Test SMS.";
            // Set a location for the event.
            Where eventLocation = new Where();
            eventLocation.ValueString = "Test sms";
            entry.Locations.Add(eventLocation);

            When eventTime = new When(DateTime.Now.AddMinutes(3), DateTime.Now.AddHours(1));
            entry.Times.Add(eventTime);

            //Add SMS Reminder
            Reminder fiftyMinReminder = new Reminder();
            fiftyMinReminder.Minutes = 1;
            fiftyMinReminder.Method = Reminder.ReminderMethod.sms;
            entry.Reminders.Add(fiftyMinReminder);
            
            Uri postUri = new Uri("http://www.google.com/calendar/feeds/default/private/full");

            // Send the request and receive the response:
            AtomEntry insertedEntry = service.Insert(postUri, entry);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                googlecalendarSMSreminder(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("zle pripojenie ku gmailu");
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (int.Parse(label4.Text) > 57)
            {
                label4.ForeColor = Color.Red;
                button1.Enabled = false;
            }
            else
            {
                label4.ForeColor = Color.Black;
                button1.Enabled = true;
            }
            label4.Text = ((textBox1.Text).Length).ToString();
        }


    }
}
