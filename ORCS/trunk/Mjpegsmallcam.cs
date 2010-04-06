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
    public class Mjpegsmallcam
    {
        //Options from the config file.
        //private Options op;

        //Did the mouse move enough to kill the screensaver?
        private Point MouseXY;

        //A separate class for each MJPEG stream.
        MjpegStream[] mjpg;

        //The drawing window.
        Graphics g;

        //A separate thread for each stream.
        Thread[] t;

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

        public void Mjpegsmallcam2(PictureBox picturebox)
        {
            
            ////////////////////////SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            //
            // Required for Windows Form Designer support
            //

            //pic2x = Screen.PrimaryScreen.Bounds.Width;
            //pic2y = Screen.PrimaryScreen.Bounds.Height;
            pic2x = picturebox.Width;
            pic2y = picturebox.Height;  
            pic2posx = picturebox.Bounds.X;
            pic2posy = picturebox.Bounds.Y;
            g = picturebox.CreateGraphics();
            ////////////////Cursor.Hide();
            MouseXY = System.Windows.Forms.Cursor.Position;
            //op = new Options();
            //op = op.ReadOptionsFromFile();
            mjpg = new MjpegStream[Robot.Robot1.o.Streams + 1];
            t = new Thread[Robot.Robot1.o.Streams + 1];
            picturebox.BackColor = ColorTranslator.FromHtml(Robot.Robot1.o.BgColor);

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
            }
        }

        //Draw the calling thread's Bmp (bitmap) on the window in the correct location.
        private void PaintBmp()
        {
            Rectangle r = new Rectangle();

            //Make single stream fullsize (except border).
            if (Robot.Robot1.o.Scale == 0 && Robot.Robot1.o.Streams == 0)
            {
                r = new Rectangle(border, border, pic2x - border * 2, pic2y - border * 2);
                //////////r = new Rectangle(border, border, Screen.PrimaryScreen.Bounds.Width - border * 2, Screen.PrimaryScreen.Bounds.Height - border * 2);
            }
            //Center single stream in window.
            else if (Robot.Robot1.o.Scale == 1 && Robot.Robot1.o.Streams == 0)
            {
                int x = pic2x, y = pic2y;
                //////////int x = Screen.PrimaryScreen.Bounds.Width, y = Screen.PrimaryScreen.Bounds.Height;
                int bmpx = mjpg[Convert.ToInt32(Thread.CurrentThread.Name)].Bmp.Width;
                int bmpy = mjpg[Convert.ToInt32(Thread.CurrentThread.Name)].Bmp.Height;
                r = new Rectangle((x - (bmpx + border)) / 2, (y - (bmpy + border)) / 2, bmpx - border, bmpy - border);
            }
            //Multiple streams, so multiplex them.
            else
            {
                int width = pic2x / 2;
                int height = pic2y / 2;
                /////////int width = Screen.PrimaryScreen.Bounds.Width / 2;
                /////////int height = Screen.PrimaryScreen.Bounds.Height / 2;
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
            g.DrawImage(mjpg[Convert.ToInt32(Thread.CurrentThread.Name)].Bmp, r);

            mutex.ReleaseMutex();
            mjpg[Convert.ToInt32(Thread.CurrentThread.Name)].Bmp.Dispose();
            mjpg[Convert.ToInt32(Thread.CurrentThread.Name)].Bmp = null;
        }

        //Something went wrong with the current thread's stream such as:
        //the request timed out or the url is invalid so display the message.
        private void PaintException()
        {
            int width = pic2x / 2;
            int height = pic2y / 2;
            ///////////int width = Screen.PrimaryScreen.Bounds.Width / 2;
            ///////////int height = Screen.PrimaryScreen.Bounds.Height / 2;
            int x = border;
            int y = border;

            switch (Convert.ToInt32(Thread.CurrentThread.Name))
            {
                case 0:
                    break;
                case 1:
                    x += width - border;
                    break;
                case 2:
                    y += height - border;
                    break;
                case 3:
                    x += width - border;
                    y += height - border;
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

        public void smallcamClose()
        {
            //Kill the threads
            for (int i = 0; i <= Robot.Robot1.o.Streams; i++)
            {
                mjpg[i].runThread = false;
                t[i].Join();
            }
            Cursor.Show();
            /////////////////////this.Close();
        }    
    }
}
