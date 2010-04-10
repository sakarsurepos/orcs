using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.IO.Ports;
//Resources
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace orcs_mobile3
{
    public partial class Form1 : Form
    {
        //public static Communication TcpClnt = new Communication();
        Communication TcpClnt = new Communication();
        Communication TcpSrvr = new Communication();
        Messages SendMessage = new Messages();
        Movement Moves = new Movement();
        //static Commands Command = new Commands();
        Thread WorkerThread;
        string data = null;

        //Resources
        ResourceManager myManager = new ResourceManager("orcs_mobile3.Properties.Resources", Assembly.GetExecutingAssembly());


        public Form1()
        {
            InitializeComponent();
        }

        private void panel5_GotFocus(object sender, EventArgs e)
        {

        }

        private void menuItem2_Click(object sender, EventArgs e)
        {//Connect
            WorkerThread = new Thread(new ThreadStart(InWorkerThread));
            WorkerThread.Start();
            TcpClnt.TCP_Client_Start(textTCPClientServerIPAddress.Text, Convert.ToInt16(textTCPClientServerPortNumber.Text));
            TcpClnt.ClientDataReceived += new Communication.ClientDelegate(TcpClnt_ClientDataReceived);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {//Horizontal
            SendData(Moves.CAM_Left_Right(trackBar1.Value));
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {//Vertical
            SendData(Moves.CAM_Up_Down(trackBar2.Value));
        }
        private void SendData(string dataForSend)
        {
            TcpClnt.Send_Data_By_Client(dataForSend);
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {//Disconnect
            TcpClnt.ClientDataReceived -= new Communication.ClientDelegate(TcpClnt_ClientDataReceived);
            TcpClnt.TCP_Client_Stop();
        }

        private void TcpClnt_ClientDataReceived()
        {
            HandlingData(TcpClnt.data_from_client);
        }

        private void HandlingData(string IncomingData)
        {
            data = null;
            //IncomingData = null;

            data = IncomingData;

            if (IncomingData != null)
            {
                InWorkerThread();
            }
        }

        void InWorkerThread()
        {
            Analyse_Data(data);
        }

        void Analyse_Data(string dataForAnalyse)
        {
            try
            {
                if (dataForAnalyse == null)
                {
                    dataForAnalyse = "123456678901234567890";
                }

                char[] USART_data = dataForAnalyse.ToCharArray();
                string CTR_MSG = new string(USART_data, 0, 6);

                if (CTR_MSG == "$MSG1,")
                {
                    string battery = new string(USART_data, 0, 7);
                    textBatteryVoltage.Text = Battery(battery);
                }
            }
            catch
            {
            }
        }

        private string Battery(string battery)
        {
            char[] bat = battery.ToCharArray();
            int ADC_voltage = Convert.ToInt16(bat[6]);
            batteryStatus.Value = ADC_voltage;
            double voltage = 0.10317 * ADC_voltage;

            return (" " + voltage.ToString("f1") + " Volts");
        }

        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            SendData(Moves.Direction_Right_Left(trackBar3.Value));
        }

        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            SendData(Moves.Motion_Foreward_Backward(trackBar4.Value));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendData(Moves.Direction_Right_Left(63));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendData(Moves.Motion_Foreward_Backward(63));
        }

        //Devices
        private void Supply_Camera_CheckStateChanged(object sender, EventArgs e)
        {
            if (Supply_Camera.Checked == true)
                SendData(SendMessage.CAMERA_ON());
            if (Supply_Camera.Checked == false)
                SendData(SendMessage.CAMERA_OFF());
        }

        private void Supply_GPS_CheckStateChanged(object sender, EventArgs e)
        {
            if (Supply_GPS.Checked == true)
            {
                SendData(SendMessage.GPS_ON());
            }
            else if (Supply_GPS.Checked == false)
            {
                SendData(SendMessage.GPS_OFF());
            }
        }

        private void CAM_Servo2_CheckStateChanged(object sender, EventArgs e)
        {
            if (CAM_Servo2.Checked == true)
                SendData(SendMessage.CameraServos_ON());
            else if (CAM_Servo2.Checked == false)
                SendData(SendMessage.CameraServos_OFF());
        }

        private void Supply_Direction_Servo_CheckStateChanged(object sender, EventArgs e)
        {
            if (Supply_Direction_Servo.Checked == true)
                SendData(SendMessage.DirectionServo_ON());
            else if (Supply_Direction_Servo.Checked == false)
                SendData(SendMessage.DirectionServo_OFF());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("<html><body>");
            //sb.Append("<a href=");
            //sb.Append("\"");
            //sb.Append("http://www.microsoft.com");
            //sb.Append("\"");
            //sb.Append(">Microsoft</a><p>");

            object htmx = myManager.GetObject("cam2");
            string str2 = (string)htmx;
            StreamWriter SW;
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            SW = File.CreateText(appPath + "\\CAMERA_activeX2.htm");
            SW.WriteLine(str2);
            SW.Close();
            webBrowser1.DocumentText = str2.ToString();
            //Uri test = new Uri("file:///" + appPath + "\\CAMERA_activeX2.htm");
            //webBrowser1.Navigate(("file:///" + appPath + "\\CAMERA_activeX2.htm"); //ok
        }

    }
}