// MJPEG Screensaver
//
// Copyright (C) 2007 Andrew Hamilton
// hamiltona@mchsi.com
//
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//
// The method to retrieve and parse the MJPEG stream
// is used from the excellent software:
// Camara Vision
//
// Copyright © Andrew Kirillov, 2005-2006
// andrew.kirillov@gmail.com
//

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Net;
using System.IO;
using System.Threading;
using System.Text;
using System.Drawing.Imaging;

namespace Robot
{
	/// <summary>
	/// Summary description for Form1.
	public class Form1 : System.Windows.Forms.Form
	{   
        //Options from the config file.
		//private Options op;

        //Did the mouse move enough to kill the screensaver?
        private Point MouseXY;

        //A separate class for each MJPEG stream.
        MjpegStream [] mjpg;

        //The drawing window.
        Graphics g;

        //A separate thread for each stream.
        Thread [] t;

        //Only one thread can draw on the window at the same time, so lock it.
        Mutex mutex = new Mutex(false, "drawlock");

        //Picture2
        int pic2x;
        int pic2y;
        int pic2posx;
        int pic2posy;

        //It seems to be difficult to paint over the Windows taskbar, this is a less
        //than ideal workaround by making a sufficient border around the drawing area.
        const int border = 0;
        private Label Batt_Etime;
        private PictureBox pictureBox2;

        Bitmap batbmp = new Bitmap(Properties.Resources.Batterysmall2);

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			SetStyle(ControlStyles.DoubleBuffer |ControlStyles.AllPaintingInWmPaint| ControlStyles.UserPaint,true);
			//
			// Required for Windows Form Designer support
			//
            
			InitializeComponent();
            pic2x = Screen.PrimaryScreen.Bounds.Width;
            pic2y = Screen.PrimaryScreen.Bounds.Height;
            pictureBox2.Width = pic2x;
            pictureBox2.Height = pic2y;
            pic2posx = pictureBox2.Bounds.X;
            pic2posy = pictureBox2.Bounds.Y;
            g = pictureBox2.CreateGraphics();
            Cursor.Hide();
            MouseXY = System.Windows.Forms.Cursor.Position;
            //op = new Options();            
            //op = op.ReadOptionsFromFile();
            mjpg = new MjpegStream[Robot.Robot1.o.Streams+1];
            t = new Thread[Robot.Robot1.o.Streams+1];
            this.BackColor = ColorTranslator.FromHtml(Robot.Robot1.o.BgColor);
            
            //Are there enough streams provided to match the number requested?
            if ((Robot.Robot1.o.Url.Length - 1) < Robot.Robot1.o.Streams)
                Robot.Robot1.o.Streams = Robot.Robot1.o.Url.Length - 1;

            //Create and start the MJPEG threads.
            for (int i = 0; i <= Robot.Robot1.o.Streams; i++)
            {                
                mjpg[i] = new MjpegStream(Robot.Robot1.o.Url[i]);
                mjpg[i].BmpEvt += new MjpegStream.BmpHandler(PaintBmp);
                mjpg[i].WebEx += new MjpegStream.WebExceptionHandler(PaintException);
                
                t[i] = new Thread(new ThreadStart(mjpg[i].FetchStream));
                t[i].Name = Convert.ToString(i);
                t[i].Start();
                Batt_Etime.Text = Robot1.batt.aktualne_nap.ToString("F1"); 
            }
		}

        //Draw the calling thread's Bmp (bitmap) on the window in the correct location.
        private void PaintBmp()
        {
            Rectangle r = new Rectangle();

            //Make single stream fullsize (except border).
            if (Robot.Robot1.o.Scale == 0 && Robot.Robot1.o.Streams == 0)
            {
                r = new Rectangle(border, border, Screen.PrimaryScreen.Bounds.Width - border*2, Screen.PrimaryScreen.Bounds.Height - border*2);
            }
            //Center single stream in window.
            else if (Robot.Robot1.o.Scale == 1 && Robot.Robot1.o.Streams == 0)
            {
                int x = Screen.PrimaryScreen.Bounds.Width, y = Screen.PrimaryScreen.Bounds.Height;
                int bmpx = mjpg[Convert.ToInt32(Thread.CurrentThread.Name)].Bmp.Width;
                int bmpy = mjpg[Convert.ToInt32(Thread.CurrentThread.Name)].Bmp.Height;
                r = new Rectangle((x - (bmpx + border))/2, (y - (bmpy + border)) / 2, bmpx - border, bmpy - border);
            }
            //Multiple streams, so multiplex them.
            else
            {
                int width = Screen.PrimaryScreen.Bounds.Width / 2;
                int height = Screen.PrimaryScreen.Bounds.Height / 2;
                switch (Convert.ToInt32(Thread.CurrentThread.Name))
                {
                    case 0:
                        r = new Rectangle(border, border, width - border, height - border);
                        break;
                    case 1:
                        r = new Rectangle(width, border, width - border, height - border);
                        break;
                    case 2:
                        r = new Rectangle(border, height, width - border, height - border);
                        break;
                    case 3:
                        r = new Rectangle(width, height, width - border, height - border);
                        break;
                }
            }
            mutex.WaitOne();
            g.DrawImage(mjpg[Convert.ToInt32(Thread.CurrentThread.Name)].Bmp,r);
            batbmp.MakeTransparent();
            g.DrawImage(batbmp, Screen.PrimaryScreen.Bounds.Width - 140, Screen.PrimaryScreen.Bounds.Height - 65);
            
            mutex.ReleaseMutex();
            mjpg[Convert.ToInt32(Thread.CurrentThread.Name)].Bmp.Dispose();
            mjpg[Convert.ToInt32(Thread.CurrentThread.Name)].Bmp = null;       
        }

        //Something went wrong with the current thread's stream such as:
        //the request timed out or the url is invalid so display the message.
        private void PaintException()
        {
            int width = Screen.PrimaryScreen.Bounds.Width / 2;
            int height = Screen.PrimaryScreen.Bounds.Height / 2;
            int x = border;
            int y = border;

            switch (Convert.ToInt32(Thread.CurrentThread.Name))
            {
                case 0:
                    break;
                case 1:
                    x += width-border;
                    break;
                case 2:
                    y += height-border;
                    break;
                case 3:
                    x += width-border;
                    y += height-border;
                    break;
            }
            Rectangle rec = new Rectangle(x, y, width - border, height - border);
            Font font = new Font("Times", 11);
            SolidBrush brush = new SolidBrush(GetColor());
            string text = mjpg[Convert.ToInt32(Thread.CurrentThread.Name)].WebExceptionString;
            StringFormat sf = new StringFormat();

            mutex.WaitOne();
            g.FillRectangle(new SolidBrush(ColorTranslator.FromHtml(Robot.Robot1.o.BgColor)), rec);
                g.DrawString(text, font, brush, rec, sf);
//                g.DrawString(mjpg[Convert.ToInt32(Thread.CurrentThread.Name)].WebExceptionString, new Font("Times", 11), new SolidBrush(GetColor()), x, y, sf);
            mutex.ReleaseMutex();

            if (mjpg[Convert.ToInt32(Thread.CurrentThread.Name)].WebExceptionString == "Null Url Supplied!")
            {
                Thread.Sleep(250);
                Thread.CurrentThread.Abort();
            }
        }

        //Draw a pseudo-random color for error messages to help prevent burn-in.
        public Color GetColor()
        {
            Random random = new Random();
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

        //Debugging purposes only. Write each thread's status to a file. (not thread safe)
        //private void GetStatus()
        //{
        //    StreamWriter st = new StreamWriter(@"C:\Documents and Settings\Russell Hamilton\Desktop\out.txt", true);
        //    for (int i = 0; i <= op.Speed; i++)
        //    {
        //        st.Write("{0} : ", t[i].IsAlive);
        //    }
        //    st.WriteLine("");
        //    for (int i = 0; i <= op.Speed; i++)
        //    {
        //        st.Write("{0} : ", t[i].ThreadState);

        //    }
        //    st.WriteLine("");
        //    st.Close();
        //}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
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
            this.Batt_Etime = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // Batt_Etime
            // 
            this.Batt_Etime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Batt_Etime.AutoSize = true;
            this.Batt_Etime.BackColor = System.Drawing.Color.LimeGreen;
            this.Batt_Etime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.Batt_Etime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Batt_Etime.Location = new System.Drawing.Point(915, 734);
            this.Batt_Etime.Name = "Batt_Etime";
            this.Batt_Etime.Size = new System.Drawing.Size(93, 13);
            this.Batt_Etime.TabIndex = 24;
            this.Batt_Etime.Text = "Batt Life: None";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Location = new System.Drawing.Point(2, 2);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(1020, 776);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 25;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1024, 780);
            this.ControlBox = false;
            this.Controls.Add(this.Batt_Etime);
            this.Controls.Add(this.pictureBox2);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main2(string[] args) 
		{
			/*
			if (args.Length>0)
			{
				
				//Show the option menu.
				if (args[0].ToLower().Trim().Substring(0, 2) == "/c")
				{
					frmOptions op = new frmOptions();
					op.ShowDialog();
					Application.Exit();
				}
				if (args[0].ToLower().Equals("/s"))
				{
					Application.Run(new Form1());
				}

			}
			else
             */
				Application.Run(new Form1());
		}

        //private void button1_Click(object sender, System.EventArgs e)
        //{
        //    Options o = new Options();
        //    o.CreateOptionsFile();
        //}

        //private void button2_Click(object sender, System.EventArgs e)
        //{
        //    frmOptions op = new frmOptions();
        //    op.ShowDialog();
        //}

		private void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
            //Kill the threads
            for (int i = 0; i <= Robot.Robot1.o.Streams; i++)
            {
                mjpg[i].runThread = false;
                t[i].Join();
            }
            Cursor.Show();
			this.Close();
		}

		private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
            //Kill the threads
            for (int i = 0; i <= Robot.Robot1.o.Streams; i++)
            {
                mjpg[i].runThread = false;
                t[i].Join();
            }
            Cursor.Show();
			this.Close();
		}

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            /*
            //Only if the mouse has moved more than 10 pixels, then kill the threads
            if (Math.Abs(MouseXY.X - e.X) > 10 || Math.Abs(MouseXY.Y - e.Y) > 10)
            {
                for (int i = 0; i <= op.Streams; i++)
                {
                    mjpg[i].runThread = false;
                    t[i].Abort();
                }
                Cursor.Show();
                this.Close();
            }
            */
             
        }
	}
}
