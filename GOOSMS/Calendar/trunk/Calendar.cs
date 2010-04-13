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
        private Timer timer50;


        private ArrayList entryList;
        public string older;
        public string older1;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private Button button2;
        private Label labelLastCheck;
        private Button button3;
        public string newer;
        private Label labelRedeem;
        private Button button4;
        public string newer1;

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
            this.components = new System.ComponentModel.Container();
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
            this.timer50 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.labelLastCheck = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.labelRedeem = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // calendarControl
            // 
            this.calendarControl.Location = new System.Drawing.Point(0, 8);
            this.calendarControl.Name = "calendarControl";
            this.calendarControl.ShowTodayCircle = false;
            this.calendarControl.TabIndex = 0;
            this.calendarControl.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.calendarControl_DateSelected);
            // 
            // CalendarURI
            // 
            this.CalendarURI.Location = new System.Drawing.Point(260, 20);
            this.CalendarURI.Name = "CalendarURI";
            this.CalendarURI.Size = new System.Drawing.Size(296, 20);
            this.CalendarURI.TabIndex = 1;
            this.CalendarURI.Text = "http://www.google.com/calendar/feeds/default/private/full";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(180, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(180, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "User:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(180, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password";
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(260, 56);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(296, 20);
            this.UserName.TabIndex = 5;
            this.UserName.Text = "kamil.zidek@gmail.com";
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(260, 92);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(128, 20);
            this.Password.TabIndex = 6;
            this.Password.Text = "joneson55";
            // 
            // Go
            // 
            this.Go.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Go.Location = new System.Drawing.Point(183, 132);
            this.Go.Name = "Go";
            this.Go.Size = new System.Drawing.Size(96, 24);
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
            this.DayEvents.Location = new System.Drawing.Point(11, 182);
            this.DayEvents.Name = "DayEvents";
            this.DayEvents.Size = new System.Drawing.Size(568, 128);
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
            // columnHeader5
            // 
            this.columnHeader5.Text = "Reminder";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Type";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(285, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Auto Check";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer50
            // 
            this.timer50.Interval = 300000;
            this.timer50.Tick += new System.EventHandler(this.timer50_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(481, 133);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Fast SMS";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelLastCheck
            // 
            this.labelLastCheck.AutoSize = true;
            this.labelLastCheck.Location = new System.Drawing.Point(417, 95);
            this.labelLastCheck.Name = "labelLastCheck";
            this.labelLastCheck.Size = new System.Drawing.Size(58, 13);
            this.labelLastCheck.TabIndex = 11;
            this.labelLastCheck.Text = "LastCheck";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(426, 133);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(49, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "Test";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // labelRedeem
            // 
            this.labelRedeem.AutoSize = true;
            this.labelRedeem.ForeColor = System.Drawing.Color.Red;
            this.labelRedeem.Location = new System.Drawing.Point(307, 166);
            this.labelRedeem.Name = "labelRedeem";
            this.labelRedeem.Size = new System.Drawing.Size(81, 13);
            this.labelRedeem.TabIndex = 13;
            this.labelRedeem.Text = "No Redemption";
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(364, 132);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(56, 23);
            this.button4.TabIndex = 14;
            this.button4.Text = "Stop";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Calendar
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(592, 322);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.labelRedeem);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.labelLastCheck);
            this.Controls.Add(this.button2);
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
            this.Text = "GooSMS Page Checker RC1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
#endregion

        private void textBox1_TextChanged(object sender, System.EventArgs e)
        {

        }

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
                button4.Enabled = true;
                button1.Enabled = false;
                //////////////////////////
                //Check 0 original Lockerz

                // Create a request for the URL. 		
                WebRequest request = WebRequest.Create("http://ptzplace.lockerz.com/"); //http://ptzplace.lockerz.com/
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

                //////////////////////////
                //Check 1 LockerzNews

                // Create a request for the URL. 		
                WebRequest request1 = WebRequest.Create("http://lockerzchecker.ismywebsite.com/index.php");
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
                older1 = older1.Substring(0, 262);
            
            //Start Check
            timer50.Start();
             
        }

        private void timer50_Tick(object sender, EventArgs e)
        {
                //////////////////////////
                //Check 0 original Lockerz

                // Create a request for the URL. 		
            WebRequest request3 = WebRequest.Create("http://ptzplace.lockerz.com/"); //http://ptzplace.lockerz.com/
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
                //Check 1 Lockerznews

                // Create a request for the URL. 		
                WebRequest request4 = WebRequest.Create("http://lockerzchecker.ismywebsite.com/index.php");
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
                newer1 = newer1.Substring(0, 262);

                // Compare String
                if (older != newer || older1 != newer1)
                {
                    CalendarService service = new CalendarService("exampleCo-exampleApp-1");
                    service.setUserCredentials("kamil.zidek@gmail.com", "joneson55");

                    EventEntry entry = new EventEntry();

                    // Set the title and content of the entry.
                    entry.Title.Text = "Lockerz";
                    if (older1 != newer1)
                    {
                        entry.Content.Content = "Redemption begins LockerzNews.";
                    }
                    else
                    {
                        entry.Content.Content = "Redemption begins Lockerz Original.";
                    }
                    // Set a location for the event.
                    Where eventLocation = new Where();
                    eventLocation.ValueString = "PC";
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

                    // Save to older.
                    older = newer;
                    older1 = newer1;

                    labelRedeem.Text = "Reedem started";
                    labelRedeem.ForeColor = Color.Green;
                }
                else
                {
                    
                }

           labelLastCheck.Text = "Last Check" +  DateTime.Now;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CalendarService service = new CalendarService("exampleCo-exampleApp-1");
            service.setUserCredentials("kamil.zidek@gmail.com", "joneson55");

            EventEntry entry = new EventEntry();

            // Set the title and content of the entry.
            entry.Title.Text = "Lockerz";
            entry.Content.Content = "Redemption begins.";

            // Set a location for the event.
            Where eventLocation = new Where();
            eventLocation.ValueString = "PC";
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

        private void button3_Click(object sender, EventArgs e)
        {
            labelLastCheck.Text = "Last Check" + DateTime.Now;
            
            // Create a request for the URL. 		
            WebRequest request1 = WebRequest.Create("http://lockerzchecker.ismywebsite.com/index.php");
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
            newer1 = responseFromServer1;
            // Cleanup the streams and the response.
            reader1.Close();
            dataStream1.Close();
            response1.Close();
            newer1 = newer1.Substring(0, 262);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button1.Enabled = true;
            timer50.Stop();
        }


    }
}
