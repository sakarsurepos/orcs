using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.ClientServices;
using System.Web.SessionState;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Script.Services;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using System.Text;
using System.IO;
//database
//using System.Data.Odbc;
using System.Media;
//drawings
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
//special
using System.Deployment;
using System.Resources;
using System.Net.NetworkInformation;
//Microsoft SQL
using System.Data.SqlClient;

namespace Robot
{
    [ScriptService]
    public partial class _Default : System.Web.UI.Page
    {
        public static Communication TcpClnt = new Communication();
        string Data = null;
        public Thread WorkerThread;
        Messages SendMessage = new Messages();
        Movement Moves = new Movement();
        Commands Command = new Commands();
        public static string strName = "0";
        static DateTime stopwatch;

        //Graphics
        SolidBrush ColorSatellite;
        Graphics Graph;
        Graphics GraphicsOnMap;
        Bitmap MapImage;

        Bitmap objBitmap;
        Graphics objGraphics;

        //mouse
        int xf;
        int yf;

        public void InsertRow() 
        {
        string connectionString = "server=192.168.1.5;uid=db2861;pwd=indiana;database=db2861";
        SqlConnection myConnection = new SqlConnection(connectionString);
        string xxx = TextBox17.Text;
        string x1 = "1234";
        x1 = TextBox17.Text;
        string myInsertQuery = "INSERT INTO dbo.orcs (Name, Country) Values('" + x1 + "o" + "', 'Northwind')"; //'" + TextBox17.Text + "','" + DropDownList8.Text + "'
        SqlCommand myCommand = new SqlCommand(myInsertQuery);
        myCommand.Connection = myConnection;
        myConnection.Open();
        myCommand.ExecuteNonQuery();
        myCommand.Connection.Close();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                trieda.mapSizeXmax = 21.357079;
                trieda.mapSizeXmin = 21.140198;
                trieda.mapSizeYmax = 48.771068;
                trieda.mapSizeYmin = 48.665117;
                //  string voltage;
            }

            //DB Connection
            /*
            if (DB_mysql.myConnection.State == ConnectionState.Open)
            {
                DB_mysql.myConnection.Close(); // close DB connection
            }
            //DB Connection
            DB_mysql.myConnection.Open();
            DataGrid1.DataSource = DB_mysql.myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            DataGrid1.DataBind();
            
            DB_mysql.myConnection.Close(); // close DB connection
             */ 
        }

        static public class trieda
        {
            //Battery
            static public byte volt = 0;
            static public string voltage = Convert.ToString(volt);
            //Timeout
            static public string testtime = "0:02:00";
            static public string x;
            //Course
            static public float xc1;
            static public float yc1;
            //GPS
            static public string GPS_PositionStatus;
            static public string GPS_Position_Date;
            static public string GPS_Position_Latitude;
            static public string GPS_Position_Longitude;
            static public string GPS_Position_Speed;
            static public string Course_Ovr_Ground;
            static public string GPS_Time;
            //Maps
            static public double mapSizeXmax;
            static public double mapSizeXmin;
            static public double mapSizeYmax;
            static public double mapSizeYmin;
            //graphics
            //static public string WorkingDirectory = @"d:\";
            //static public System.Drawing.Image img3 = System.Drawing.Image.FromFile(WorkingDirectory + @"\energy.png");
        }

        [WebMethod]
        public static string Clear_Garbage()
        {
            GC.Collect();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Server.ClearError();
            //GC.SuppressFinalize(Page);
            //TcpClnt.ClientDataReceived -= new Communication.ClientDelegate(TcpClnt_ClientDataReceived);
            try
            {
                TcpClnt.Client_Thread.Abort();
                TcpClnt.Client1.Close();
                TcpClnt.strm_1.Close();
                TcpClnt.strm_2.Close();
            }
            catch
            {
            }
            return "Thank you for testing Remote Robot Control";
        }

        [WebMethod]
        public static void BrowserCheck()
        {
            strName = "1";
        }
        
        public void SetData()
        {
            //<%SetData();%>;
            //alert('<%=strName%>'); //IP checked
            strName = "Your IP " + trieda.x + " is logged to Database";
        }

        private string Battery(string battery)
        {
            char[] bat = battery.ToCharArray();
            int ADC_voltage = Convert.ToInt16(bat[6]);            
            double voltage = 0.10317 * ADC_voltage;
            Label10.Text = (((voltage-4)/9)*100).ToString("f0") + "%";
            return ("" + voltage.ToString("f1") + "V");
        }

        void Analyse_Data(string data_for_analyse)
        {
            if (data_for_analyse == null)
            {
                byte x0 = 0;
                data_for_analyse = "$MSG1,"+x0+"";
            }

            TextBox1.Text = data_for_analyse;
            char[] USART_data = data_for_analyse.ToCharArray();
            string CTR_MSG = new string(USART_data, 0, 6);

            if (CTR_MSG == "$MSG1,")
            {
                string battery = new string(USART_data, 0, 7);
                                
                if (battery != "")
                {
                    trieda.voltage = Battery(battery);
                }
                TextBox1.Text = trieda.voltage;

                Timer1.Enabled = true;
             }

            if (CTR_MSG == "$GPRMC")
            {
                string GPS_RMC_Data = new string(USART_data, 0, USART_data.Length);
                                
                trieda.GPS_PositionStatus = GPS.GPS_Status(GPS_RMC_Data);
                trieda.GPS_Position_Date = GPS.GPS_Date(GPS_RMC_Data);
                trieda.GPS_Position_Latitude = GPS.GPS_Latitude(GPS_RMC_Data, "info");
                trieda.GPS_Position_Longitude = GPS.GPS_Longitude(GPS_RMC_Data, "info");
                trieda.GPS_Position_Speed = GPS.GPS_Speed(GPS_RMC_Data);
                trieda.Course_Ovr_Ground = GPS.GPS_Course(GPS_RMC_Data);
                trieda.GPS_Time = GPS.GPS_UTC_Time(GPS_RMC_Data);

                float number_course = (GPS.GPS_Course2(GPS_RMC_Data)) / 10;
                //textBox6.Text = number_course.ToString();
                //textBox7.Text = GPS.GPS_Course(GPS_RMC_Data);
                trieda.xc1 = (float)(50 * Math.Cos((number_course * Math.PI) / 180));
                trieda.yc1 = (float)(50 * Math.Sin((number_course * Math.PI) / 180));

                
                if (trieda.GPS_PositionStatus == "Valid position")
                {
                    //System.Drawing.Image objBitma = System.Drawing.Image.FromFile("energy.png");
                    objBitmap = new Bitmap(320,320); //"~/Resources/letecka_2_source.bmp"
                    //System.Drawing.Bitmap img2 = new System.Drawing.Bitmap("energy.png");
                    //GraphicsOnMap.DrawImage(img2, 5, 5);
                    GraphicsOnMap = Graphics.FromImage(objBitmap); //objBit
                    //GraphicsOnMap.DrawImage(ImageButto, 50, 50);
                    Pen penPointOnMap = new Pen(Color.Red);
                    Pen penLinesOnMap = new Pen(Color.Black);
                    //float lat = float.Parse(GPS.GPS_Latitude(GPS_RMC_Data, "number"));
                    float lon = float.Parse(GPS.GPS_Longitude(GPS_RMC_Data, "number"));
                    //int x = Convert.ToInt16(Convert.ToDouble(ImageButton1.Width.Value) / (trieda.mapSizeXmax - trieda.mapSizeXmin) * (lon - trieda.mapSizeXmin));
                    //int y = Convert.ToInt16(Convert.ToDouble(ImageButton1.Height.Value) / (trieda.mapSizeYmax - trieda.mapSizeYmin) * (trieda.mapSizeYmax - lat));
                    
                    //if (mapd.Text != "TUKE Zoom")
                    //{
                    //GraphicsOnMap.DrawLine(penLinesOnMap, x, 0, x, (float)Convert.ToDouble(ImageButton1.Height.Value));
                    //GraphicsOnMap.DrawLine(penLinesOnMap, 0, y, (float)Convert.ToDouble(ImageButton1.Width.Value), y);
                    //GraphicsOnMap.DrawRectangle(penLinesOnMap, x - 18, y - 18, 36, 36);
                    //GraphicsOnMap.DrawEllipse(penPointOnMap, x - 3, y - 3, 6, 6);
                    //GraphicsOnMap.DrawImageUnscaled(System.Drawing.Image.FromFile("~/Resources/energy2.bmp"),0, 0);
                    //GraphicsOnMap.DrawEllipse(penPointOnMap, 0, 0, 50, 50);
                    string strFilename;
                    strFilename = Server.MapPath("leteckas.bmp");
                    System.Drawing.Image obr = System.Drawing.Image.FromFile(strFilename);
                    GraphicsOnMap.DrawImageUnscaled(obr, 0, 0);
                    GraphicsOnMap.DrawRectangle(penPointOnMap, 5, 5, 50, 50);
                    //objBitmap.Save(Server.MapPath("~/Resources/mapa_kosice.bmp"), ImageFormat.Bmp);
                    //objBitmap.Save(Server.MapPath("~/Resources/mapa_areal_tuke.bmp"), ImageFormat.Bmp);

                    //}
                   // if (mapd.Text == "TUKE Zoom")
                   // {
                    //GraphicsOnMap.DrawEllipse(penPointOnMap, x - 5, y - 5, 10, 10);
                    //GraphicsOnMap.DrawLine(penPointOnMap, x, y, x + trieda.xc1, y + trieda.yc1);
                   objBitmap.Save(Server.MapPath("~/Resources/letecka_2.bmp"), ImageFormat.Bmp);
                   // }

                }
                

            }

        }

        void InWorkerThread()
        {//Analyze
            Analyse_Data(Data);
        }

        private void HandlingData(string IncomingData)
        {//Handling Data2
            Data = null;
            //IncomingData = null;
            Data = IncomingData;

            if (IncomingData != null)
            {
                InWorkerThread();
            }
        }

        private void TcpClnt_ClientDataReceived()
        {//Handling Data
            HandlingData(TcpClnt.data_from_client);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {//Disconnection
            SendData(SendMessage.CAMERA_OFF());
            SendData(SendMessage.DirectionServo_OFF());
            SendData(SendMessage.CameraServos_OFF());
            SendData(SendMessage.GPS_OFF());
            SendData(Moves.Motion_Forward_Backward(63)); //Stop
            SendData(Moves.Direction_Right_Left(63)); //Center
            TcpClnt.ClientDataReceived -= new Communication.ClientDelegate(TcpClnt_ClientDataReceived);
            TcpClnt.TCP_Client_Stop();
            Button1.Enabled = true;
            Button2.Enabled = false;
            Button4.Enabled = false;
            Button5.Enabled = false;
            Button6.Enabled = false;
            Button7.Enabled = false;
            Button8.Enabled = false;
            Label3.Text = "";
        }

        private void SendData(string dataForSend)
        {//Send function
            TcpClnt.Send_Data_By_Client(dataForSend);
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {//Connection timer disconection
            if (trieda.testtime == "0:00:01")
            {
                SendData(Moves.Motion_Forward_Backward(63)); //Stop
                SendData(Moves.Direction_Right_Left(63)); //Center
                SendData(SendMessage.DirectionServo_OFF()); 
                TcpClnt.ClientDataReceived -= new Communication.ClientDelegate(TcpClnt_ClientDataReceived);
                TcpClnt.TCP_Client_Stop();
                Label1.Text = "Disconnected";
                Button1.Enabled = true;
                Button2.Enabled = false;
                Button4.Enabled = false;
                Button5.Enabled = false;
                Button6.Enabled = false;
                Button7.Enabled = false;
                Button8.Enabled = false;
                Timer1.Enabled = false;
                Label2.Text = "0:02:00";
                trieda.testtime = "0:02:00";
            }

            //Update values sended by robot
            TextBox1.Text = trieda.voltage;
            GPS_PositionStatus.Text = trieda.GPS_PositionStatus;
            GPS_Position_Date.Text = trieda.GPS_Position_Date;
            GPS_Position_Latitude.Text = trieda.GPS_Position_Latitude;
            GPS_Position_Longitude.Text = trieda.GPS_Position_Longitude;
            GPS_Position_Speed.Text = trieda.GPS_Position_Speed;
            Course_Ovr_Ground.Text = trieda.Course_Ovr_Ground;
            GPS_Time.Text = trieda.GPS_Time;
            //Update values sended by robot

            if (TcpClnt.ClientConnectingStatus() == true)
            {
                Label1.Text = "Connected";
                Label2.Text = stopwatch.ToLongTimeString();
                trieda.testtime = stopwatch.ToLongTimeString();
                stopwatch -= TimeSpan.Parse("00:00:01");
            }
            else
            {
                Label1.Text = "Disconnected";
                Button1.Enabled = true;
                Button2.Enabled = false;
                Button4.Enabled = false;
                Button5.Enabled = false;
                Button6.Enabled = false;
                Button7.Enabled = false;
                Button8.Enabled = false;
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {//Forward Move
            Command.Command_Forward_Backward(int.Parse(ECommand_speed.Text) + 63, int.Parse(ECommand_time.Text), ref TcpClnt);            
        }

        protected void Button7_Click(object sender, EventArgs e)
        {//Backward Move
            Command.Command_Forward_Backward(int.Parse(ECommand_speed.Text), int.Parse(ECommand_time.Text), ref TcpClnt);
        }

        protected void Button8_Click(object sender, EventArgs e)
        {//Stop Robot
            Command.Command_Forward_Backward(63, 1000, ref TcpClnt); 
        }

        protected void Button5_Click(object sender, EventArgs e)
        {//Right Move
            Command.Command_Direction_Right_Left(int.Parse(ECommand_speed.Text), 63 + int.Parse(ECommand_angle.Text), int.Parse(ECommand_time.Text), ref TcpClnt);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {//Left Move
            Command.Command_Direction_Right_Left(int.Parse(ECommand_speed.Text), 63 - int.Parse(ECommand_angle.Text), int.Parse(ECommand_time.Text), ref TcpClnt);
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //Camera ON/OFF
            if (CheckBox1.Checked == true)
                SendData(SendMessage.CAMERA_ON());
            if (CheckBox1.Checked == false)
                SendData(SendMessage.CAMERA_OFF());
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {//Direction Servo ON/OFF
            if (CheckBox2.Checked == true)
                SendData(SendMessage.DirectionServo_ON());
            else if (CheckBox2.Checked == false)
                SendData(SendMessage.DirectionServo_OFF());
        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {//Camera Servos ON/OFF
            if (CheckBox3.Checked == true)
                SendData(SendMessage.CameraServos_ON());
            else if (CheckBox3.Checked == false)
                SendData(SendMessage.CameraServos_OFF());
        }

        protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {//GPS ON/OFF
            if (CheckBox4.Checked == true)
                SendData(SendMessage.GPS_ON());
            else if (CheckBox4.Checked == false)
                SendData(SendMessage.GPS_OFF());
        }

        protected void Button9_Click(object sender, EventArgs e)
        {//Check Robot on network
        if(TextBox17.Text != "")
         {

            TcpClient TestClient = new TcpClient(); //static public
            try
            {
                TestClient.Connect((DropDownList1.SelectedItem.Value).ToString(), int.Parse(DropDownList2.SelectedItem.Text));
                if (TestClient.Connected == true)
                {
                    Label4.ForeColor = Color.Green;
                    Label4.Text = "Free for Control";
                    if (strName == "1")
                    {
                        Button1.Enabled = true;
                    }
                }
                TestClient.Close();
            }
            catch(Exception ex)
            {
                Label4.Text = "Inactive or Occupied";
            }

            //try
            //{///Check IP of robot
            //    Dns.GetHostByAddress((DropDownList1.SelectedValue).ToString());
            //}
            //catch
            //{
            //    Label4.Text = "IP unaccesible";
            //}
          }
          else
            {
                Label4.ForeColor = Color.Red;
                Label4.Text = "Your name is missing";
            }
        }

        protected void Button10_Click(object sender, EventArgs e)
        {//Remote Connection Button
            WorkerThread = new Thread(new ThreadStart(InWorkerThread));
            WorkerThread.Start();
            TcpClnt.TCP_Client_Start((DropDownList1.SelectedItem.Value).ToString(), int.Parse(DropDownList2.SelectedItem.Text));
            TcpClnt.ClientDataReceived += new Communication.ClientDelegate(TcpClnt_ClientDataReceived);
            if (TcpClnt.ClientConnectingStatus() == true)
            {
                Label12.ForeColor = Color.Green;
                Label12.Text = "Do you have 2 minutes to control robot";
                stopwatch = DateTime.Parse("0:02:00"); //setup timer1 to 2 minutes
                SendData(SendMessage.DirectionServo_ON());
                Timer1.Enabled = true;
                Button2.Enabled = true;
                Button1.Enabled = false;
                Button4.Enabled = true;
                Button5.Enabled = true;
                Button6.Enabled = true;
                Button7.Enabled = true;
                Button8.Enabled = true;
                trieda.x = Request.ServerVariables["REMOTE_ADDR"].ToString(); //find out your address
                Label3.Text = trieda.x;

                //Databaze insert recorded user
                ////DB_mysql.myConnection.Open();
                ////DB_mysql.myCommand_update_close.ExecuteNonQuery();
                ////DB_mysql.myCommand_update_close2.ExecuteNonQuery();
                ////DB_mysql.myConnection.Close();
                //Databaze insert recorded user

                string CommandText_table = "INSERT INTO web_control_table(user,country) values('" + TextBox17.Text + "','" + DropDownList8.Text + "')";
                ////OdbcCommand myCommand_update_table = new OdbcCommand(CommandText_table, DB_mysql.myConnection); //command drop

                ////DB_mysql.myConnection.Open();
                 try
                 {
                     ////myCommand_update_table.ExecuteNonQuery();
                 }
                 catch
                 {
                     Label12.BackColor = Color.Red;
                     Label12.Text = "Database problem!";
                 }
                 ////DB_mysql.myConnection.Close();
                 //Turn on all devices
                 SendData(SendMessage.CAMERA_ON());
                 SendData(SendMessage.DirectionServo_ON());
                 SendData(SendMessage.CameraServos_ON());
                 SendData(SendMessage.GPS_ON());
            }
        }

        protected void TextBox3s_TextChanged(object sender, EventArgs e)
        {//Slider Cam Left Right
            SendData(Moves.CAM_Left_Right(int.Parse(TextBox3s.Text)));
        }

        protected void TextBox2s_TextChanged(object sender, EventArgs e)
        {//Slider Cam Up Down
            SendData(Moves.CAM_Up_Down(int.Parse(TextBox2s.Text)));
        }

        protected void Button14_Click(object sender, EventArgs e)
        {
            Panel17.Enabled = false;
           //SoundPlayer sp = new SoundPlayer("Windows XP Battery Critical.wav");
           //sp.Play();
        }

        protected void DropDownList9_TextChanged(object sender, EventArgs e)
        {
            SetMap();
        }

        private void ShowMapImage(string path)
        {

            //ImageButton1.SizeMode = PictureBoxSizeMode.StretchImage;
            MapImage = new Bitmap(path);
            //ImageButton1.ClientSize = new Size(575, 575); //575, 334
            //ImageButton1.Image = (Image)MapImage;
            ImageButton1.ImageUrl = path;
        }

        private void SetMap()
        {//set map
            switch (mapd.Text)
            {
                case "Kosice":
                    ImageButton1.ImageUrl = "~/Resources/mapa_kosice.bmp";
                    trieda.mapSizeXmax = 21.357079;
                    trieda.mapSizeXmin = 21.140198;
                    trieda.mapSizeYmax = 48.771068;
                    trieda.mapSizeYmin = 48.665117;
                    break;
                case "TUKE":
                    ImageButton1.ImageUrl = "~/Resources/mapa_areal_tuke.bmp";
                    trieda.mapSizeXmax = 21.251743;
                    trieda.mapSizeXmin = 21.238385;
                    trieda.mapSizeYmax = 48.735864;
                    trieda.mapSizeYmin = 48.728874;
                    break;
                case "TUKE Zoom":
                    ImageButton1.ImageUrl = "~/Resources/letecka_2.bmp";    //Maps/letecka_2.bmp ShowMapImage("~/Resources/letecka_2.bmp");
                    trieda.mapSizeXmax = 21.246147; //21.247010;
                    trieda.mapSizeXmin = 21.243141; //21.243922;
                    trieda.mapSizeYmax = 48.732886; //48.732778;
                    trieda.mapSizeYmin = 48.73055; //48.730430;
                    break;
            }

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {//get mouse click position
                xf = e.X;
                yf = e.Y;
                Label13.Text = xf.ToString();
                Label14.Text = yf.ToString();
                longm.Text = ((Convert.ToDouble(trieda.mapSizeXmin + ((trieda.mapSizeXmax - trieda.mapSizeXmin) / Convert.ToDouble(ImageButton1.Width.Value)) * xf))).ToString("f7");
                lattm.Text = ((Convert.ToDouble(trieda.mapSizeYmin + ((trieda.mapSizeYmax - trieda.mapSizeYmin) / Convert.ToDouble(ImageButton1.Height.Value)) * yf))).ToString("f7");
        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            Panel17.Enabled = true;

        }

        protected void Button18_Click(object sender, EventArgs e)
        {
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send("147.232.20.241");
            if (reply.Status == IPStatus.Success)
            { Label12.Text = string.Format("IP working: {0} ms", reply.RoundtripTime); }
            else { Label12.Text = "IP not working"; }

        }

        protected void Button19_Click(object sender, EventArgs e)
        {
            InsertRow();
        }

        protected void Button19_Click1(object sender, EventArgs e)
        {
            InsertRow();
        }
    }
}