using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.IO.Ports;
//Resources
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Net.NetworkInformation;
//Camera
using VideoSource;
using Tiger.Video.VFW;
//3D
using MTV3D65; //Truevision3D
//Audio Stream
using LumiSoft.Net.RTP;
using LumiSoft.Net.RTP.Debug;
using LumiSoft.Net.Media.Codec.Audio;
using LumiSoft.Net.Media;
//Sound
using System.Media;
//Bootloader
using System.Diagnostics;
//Update
using System.Deployment;
using System.Deployment.Application;

namespace Robot
    {
    public delegate void DelegateAddString(String s); //AUDIO DELEGATE
    public delegate void DelegateAddString1(String s1); //AUDIO DELEGATE

    public partial class Robot1 : System.Windows.Forms.Form
    {
        //////////
        //ACCELEROMETER
        public int testval;
        private SerialPort mySerialPort = null;
        private Byte[] buffer = new Byte[5000];
        //private Char[] sendChars = new Char[] { 'G' };
        private Int32 nMinX = 70; //10000;
        private Int32 nMaxX = 215;//-10000;
        private Int32 nMinY = 70;//10000;
        private Int32 nMaxY = 215;//-10000;
        private Int32 nMinZ = 70;//10000;
        private Int32 nMaxZ = 215;//-10000;
        private Queue byteQueue = new Queue();

        private bool b3dMode = false;
        public UsrCtrlAxis2D usrCtrlAxis2D = null;
        //private UsrCtrlAxis3D usrCtrlAxis3D = null;

        private bool bUseSmoothing = false;
        private int nSmoothingDelta = 5;
        private int nPrevX = 0;
        private int nPrevY = 0;
        private int nPrevZ = 0;

        Byte byte01 = 127; //
        Byte byte02 = 127; //X
        Byte byte03 = 127; //Y
        Byte byte04 = 127; //Z
        Byte byte05 = 127; //13
        Byte byte06 = 127; //10

        Int32 nXaxis = 127;
        Int32 nYaxis = 127;
        Int32 nZaxis = 127;

        //delayed values

        Byte byte010 = 127; //
        Byte byte020 = 127; //X
        Byte byte030 = 127; //Y
        Byte byte040 = 127; //Z

        Int32 nXaxis0 = 127;
        Int32 nYaxis0 = 127;
        Int32 nZaxis0 = 127;
        Int32 selAxis = 127;

        private Int32 nMinX0 = 70;// 10; //10000;
        private Int32 nMaxX0 = 215;// 245;//-10000;
        private Int32 nMinY0 = 70;// 10;//10000;
        private Int32 nMaxY0 = 215;// 245;//-10000;
        private Int32 nMinZ0 = 70;// 10;//10000;
        private Int32 nMaxZ0 = 215;// 245;//-10000;

        public int inkr = 0;

        public static float fAnglex0 = 90;
        public static float fAngley0 = 90;
        public static float fAnglez0 = 90;

        int num = 0;

        public static int radio = 1;

        Thread trd;
        
        //////////
        //3D MODEL
        public cengine3D engine3D = new cengine3D();
        public TVEngine tv;
        public TVScene scene;
        public TVInputEngine input;
        public TVGlobals globals;
        public TVMathLibrary maths;
        public TVTextureFactory texturefactory;
        public TVMaterialFactory materialfactory;
        public TVCamera camera;
        public TVViewport viewport;
        public TVLightEngine lights;
        public TVAtmosphere atmosphere;
        public TVPhysics physics;
        //Game Loop
        public bool bDoLoop;
        //Terrain
        TVLandscape Land;
        int pbLand;
        //Materials
        int matLand;
        int matVehicleBody;
        int matWindow;
        int matWheels;
        //Models
        TVMesh pk_9;
        TVMesh m_chassis;
        TVMesh m_fl;
        TVMesh m_fr;
        TVMesh m_rl;
        TVMesh m_rr;
        int pbi_chassis;
        int flw;
        int frw;
        int rlw;
        int rrw;
        int car_ID;
        float steerAngle;
        float CarPower = 800;
        int pmatTerrain;
        int pmatChassis;
        bool autrst = false;
        bool camfol = false;
        bool camtop = false;
        //3D MODEL
        //////////

        ////////
        //CAMERA
        private const int statLength = 15;
        private int statIndex = 0, statReady = 0;
        private int[] statCount = new int[statLength];
        private IMotionDetector detector = new MotionDetector1();
        private int detectorType = 1;
        private int intervalsToSave = 0;
        public bool GameFlag = false;  //Flag for Enable 3D Environment
        //variables for RGB
        public static string vysledok = "start";
        public static string A1 = "null";
        public static string R1 = "null";
        public static string G1 = "null";
        public static string B1 = "null";
        public static string vysledok1 = "start";
        public static string vysledok2 = "start";
        public static string vysledok3 = "start";
        //moj kod

        //Set Label112
        private static byte Red_l112;
        public static byte Setlabel112
        {
            get { return Red_l112; }
            set { Red_l112 = value; }
        }

        //Set Label114
        private static byte Green_l114;
        public static byte Setlabel114
        {
            get { return Green_l114; }
            set { Green_l114 = value; }
        }

        //Set Label116
        private static byte Blue_l116;
        public static byte Setlabel116
        {
            get { return Blue_l116; }
            set { Blue_l116 = value; }
        }

        //Set Label118
        private static byte Red_l118;
        public static byte Setlabel118
        {
            get { return Red_l118; }
            set { Red_l118 = value; }
        }

        //Set Label120
        private static byte Green_l120;

        public static byte Setlabel120
        {
            get { return Green_l120; }
            set { Green_l120 = value; }
        }

        //Set Label119
        private static byte Blue_l119;
        public static byte Setlabel119
        {
            get { return Blue_l119; }
            set { Blue_l119 = value; }
        }

        //Set Label115
        private static byte Red_l115;
        public static byte Setlabel115
        {
            get { return Red_l115; }
            set { Red_l115 = value; }
        }
        //Set Label113
        private static byte Green_l113;
        public static byte Setlabel113
        {
            get { return Green_l113; }
            set { Green_l113 = value; }
        }

        //Set Label111
        private static byte Blue_l111;
        public static byte Setlabel111
        {
            get { return Blue_l111; }
            set { Blue_l111 = value; }
        }
        //mouse cam
        public static int mv1x;
        public static int mv1y;
        public static int sens = 1;
        public static bool filter = false;
        public static bool mot = false;
        //check color
        public static bool c = false;
        public static bool checkcolor = false;
        int c2 = 1;
        //CAMERA
        ////////

        ////////////
        //KINEMATICS
        Kinematics Kin = new Kinematics();
        //mouse kin
        int m1x;
        int m1y;
        string Rout;
        string Angleout;
        bool flag2;
        int count;
        //KINEMATICS
        ////////////
        
        /////////////
        //GOOGLE MAPS
        double lat;
        double lon;
        float[] gpsx = new float[1000];
        float[] gpsy = new float[1000];
        double[] gpslong = new double[1000];
        double[] gpslatt = new double[1000];
        int gpsi = 0;
        double xpos;
        double ypos;
        //Save load Track
        int gpsri = 0;
        Pen xp = new Pen(Color.Red);
        double latcent;
        double loncent;
        //GOOGLE MAPS
        /////////////
        
        /////////////////
        //BATTERY MONITOR
        public static battery batt = new battery(); //Inicialize class for battery
        public double mv1xmon;
        public double mv1ymon;
        //actualize values on UI
        public void change_components_text(string nap, string cas) //Update values
        {
            this.textBoxbattmonvolt.Text = nap;
            this.textBoxbattmontime.Text = cas;
            this.richTextBoxbattmon.AppendText(cas);
            this.richTextBoxbattmon.AppendText("_");
            this.richTextBoxbattmon.AppendText(nap);
            this.richTextBoxbattmon.AppendText(Environment.NewLine);
        }
        //BATTERY MONITOR
        /////////////////

        ////////////////
        //AUDIO STREAMER
        private bool m_IsRunning = false;
        private bool m_IsSendingTest = false;
        private RTP_MultimediaSession m_pRtpSession = null;
        private AudioOut m_pWaveOut = null;
        private FileStream m_pRecordStream = null;
        private string m_PlayFile = "";
        private AudioCodec m_pActiveCodec = null;
        wfrm_SendMic2 frm;
        //wfrm_Receive2 frm2;
        // delegate is used to communicate with UI Thread from a non-UI thread
        public DelegateAddString m_DelegateAddString; //AUDIO DELEGATE
        public DelegateAddString1 m_DelegateAddString1; //AUDIO DELEGATE
        //AUDIO STREAMER
        ////////////////

        ////////////
        //BOOTLOADER
        static string error, standard;
        string selectedfilepath = "test.hex";
        string filename;
        //BOOTLOADER
        ////////////


        /////////////
        //FULL SCREEN
        //frmOptions op;
        public static Options o;
        //FULL SCREEN
        /////////////

        /////////////
        //ultra sonic
        double xultra;
        double yultra;
        int[] bultr = new int[25] { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 140, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };
        int ultrainc = 0;
        int UltraSonic;
        int lop;
        double xultrasonic;
        double yultrasonic;
        int servoinc = 126;
        Graphics Obj1;
        //ultra sonic
        /////////////

        /////////////
        //Infra
        int InfraFront;
        int InfraBack;
        //Infra
        /////////////

        /////////////
        //CONSOLE
        char[] test2;
        Thread ThreadCheckStat;
        //CONSOLE
        /////////////

        public Robot1()
        {
            //ACCELEROMETER
            this.usrCtrlAxis2D = new UsrCtrlAxis2D();
            this.usrCtrlAxis2D.Size = new Size(350, 350);
            this.usrCtrlAxis2D.Location = new Point(0, 0);
            this.usrCtrlAxis2D.BackColor = Color.AliceBlue;
            //this.tabPage19.Controls.Add(this.usrCtrlAxis2D);
                        
            InitializeComponent();
            
            //VERSION UPDATE PUBLISH
            Version version2 = new Version();
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                System.Deployment.Application.ApplicationDeployment ad = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                version2 = ad.CurrentVersion;
            }

            //VERSION UPDATE BUILD
            Version vrs = new Version(Application.ProductVersion);
            this.Text = "CONTROL APPLICATION  ORCS ROBOT G4 (SECURITY GUARD ROBOT) / AssemblyBuid " + vrs.Major + "." + vrs.Minor + "." + vrs.Build + "." + vrs.Revision + " Publish Version  " + PublishVersion().ToString() + " ClickOnce RC" + version2.Revision.ToString(); //String.Format("ClickOnce Version {0}.{1}.{2}.{3}", version.Major, version.Minor, version.Revision, version.Build);
            
            //BATTERY
            batt.aktualne_nap = 0.1f; //initial value for battery monitor
            SetMap();
            Robot1.CheckForIllegalCrossThreadCalls = false;
            comboBox1.SelectedItem = "nothing";
            comboBox2.SelectedItem = "nothing";
            comboBox3.SelectedItem = "nothing";
            comboBox4.SelectedItem = "nothing";
            comboBox5.SelectedItem = "nothing";
            comboBox6.SelectedItem = "nothing";
            comboBox7.SelectedItem = "nothing";
            comboBox8.SelectedItem = "nothing";
            comboBox9.SelectedItem = "nothing";
            comboBox10.SelectedItem = "nothing";
            //Trajectory activation
            button12.Enabled = true;
            button14.Enabled = false;
            button17.Enabled = false;
            button13.Enabled = false;
            button18.Enabled = false;
            button15.Enabled = false;
            //Audio stream
            foreach (IPAddress ip in System.Net.Dns.GetHostAddresses(""))
            {
                m_pLocalIP.Items.Add(ip.ToString());
            }
            m_pLocalIP.SelectedIndex = m_pLocalIP.Items.Count-2;
            //m_pLocalIP.SelectedIndex = 0;
            m_pCodec.Items.Add("G711 a-law");
            m_pCodec.Items.Add("G711 u-law");
            m_pCodec.Enabled = false;
            LoadWaveDevices();
            m_pActiveCodec = new PCMA();
            //Bootloader
            comboBoxport.SelectedIndex = 0;
            comboBoxMCU.SelectedIndex = 0;
            //Inicialize TCP Client
            groupDirectionAndMotion.Enabled = false;
            groupCameraRotation.Enabled = false;
            groupCameraRot2.Enabled = false;
            groupComPortSettings.Enabled = false;
            groupComPortDefault.Enabled = false;
            groupTcpClientServerSettings.Enabled = true;
            groupTcpClientServerDefault.Enabled = true;
            textTCPClientServerIPAddress.ReadOnly = false;
            textTCPCommunicationType.Text = "CLIENT";
            labelCommunicationType.Text = "TCP Client";
            toolStripconnect.Enabled = true;
            ThreadCheckStat = new Thread(CheckStatus);
            ThreadCheckStat.Start();
            //Small Cam
            o = new Options();
            combo_streams.SelectedText = "1";
            //AUDIO DELEGATE
            m_DelegateAddString = new DelegateAddString(this.AddString);
            m_DelegateAddString1 = new DelegateAddString1(this.AddString1);

            //ACCELEROMETER
            //this.usrCtrlAxis3D = new UsrCtrlAxis3D();
            //this.usrCtrlAxis3D.Size = new Size(350, 350);
            //this.usrCtrlAxis3D.Location = new Point(0, 0);
            //this.Controls.Add(this.usrCtrlAxis3D);
            this.mySerialPort = new SerialPort("COM4", 9600, Parity.None, 8, StopBits.One);
            this.mySerialPort.Handshake = Handshake.None;
            this.mySerialPort.RtsEnable = true;
            this.mySerialPort.DataReceived += new SerialDataReceivedEventHandler(MySerialPort_DataReceived);

        }
         
        //Classes
        Communication   TcpClnt     = new Communication();
        Communication   TcpSrvr     = new Communication();
        JoystickDevice  Joystick    = new JoystickDevice();
        Messages        SendMessage = new Messages();
        Movement        Moves       = new Movement();
        static Commands Command     = new Commands();
        Mjpegsmallcam mjpegsmall    = new Mjpegsmallcam();

        //Components
        Thread          WorkerThread;
        SolidBrush      ColorSatellite;
        Graphics        Graph;
        Graphics        GraphicsOnMap;
        Graphics        GraphicsGmaps;
        Bitmap          MapImage;
        Pen penPointOnMap = new Pen(Color.Red);
        Pen penLinesOnMap = new Pen(Color.Black);
        Pen penLinesTraj = new Pen(Color.Red, 2);

        double longm1;
        double lattm1;
        double          mapSizeXmax;
        double          mapSizeXmin;
        double          mapSizeYmax;
        double          mapSizeYmin;

        int         numberOfMsgs;
        int         numberOfSatellites;
        string      data1;
        string[]    GPGSV_Data = new string[4];
        int         scriptCor1;
        string      data = null;
        
        float[] PointX = new float[100];
        float[] PointY = new float[100];
        int a = 0;
        int b = 0;
        
        int m2x;
        int m2y;

        float xc1;
        float yc1;

        int x;
        int y;
        float number_course;
        
        //Trajectory
        bool traj = false;
        double[] TrajLong = new double[100];
        double[] TrajLatt = new double[100];
        int[] TrajCourse = new int[100];

        //Point Mouse Point
        int i = 0;
        int[] TrajPointX = new int[100];
        int[] TrajPointY = new int[100];
        float GPS_Latitude;
        float GPS_Longtitude;

        int active_command = 1;

        //Resources
        ResourceManager myManager = new ResourceManager("Robot.Properties.Resources", Assembly.GetExecutingAssembly());

        private void direction_Scroll(object sender, EventArgs e)
        {
            SendData(Moves.Direction_Right_Left(slidDirection.Value));  
        }
        private void motion_Scroll(object sender, EventArgs e)
        {
            SendData(Moves.Motion_Foreward_Backward(slidMotion.Value));
        }
        private void Supply_GPS_CheckedChanged(object sender, EventArgs e)
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
  
        private void Joystick_Initialization_Click(object sender, EventArgs e)
        {
            
            if (labelJoystickStatus.Text == "OFF")
            {
                labelJoystickStatus.Text = "ON";
                Joystick.Init_Joystick_Device();
                Joystick.Joystick_START();
                labelJoystickName.Text   = Joystick.JoystickName();
                Joystick.JoystickEvent   += new JoystickDevice.JoystickDelagate(Joystick_JoystickEvent);
            }
            else if (labelJoystickStatus.Text == "ON")
            {
                JoystickOFF();
            }
        }

        private void JoystickOFF()
        {
            labelJoystickStatus.Text        = "OFF";
            Joystick.JoystickEvent          -= new JoystickDevice.JoystickDelagate(Joystick_JoystickEvent);
            Joystick.Joystick_STOP();
        }

        private void Joystick_JoystickEvent()
        {
            slidDirection.Value             = Joystick.directionValue;
            slidMotion.Value                = Joystick.motionValue;
            slidCameraLeftRight.Value       = Joystick.cameraHorizontalValue;
            slidCameraUpDown.Value          = Joystick.cameraVerticalValue;
        }
        
        private void Supply_Direction_Servo_CheckedChanged(object sender, EventArgs e)
        {
            if (Supply_Direction_Servo.Checked == true)
                SendData(SendMessage.DirectionServo_ON());
            else if (Supply_Direction_Servo.Checked == false)
                SendData(SendMessage.DirectionServo_OFF());
        }

        private void Camera_Up_Down_Scroll(object sender, EventArgs e)
        {
            SendData(Moves.CAM_Up_Down(slidCameraUpDown.Value));            
        }

        private void Camera_Left_Right_Scroll(object sender, EventArgs e)
        {
            SendData(Moves.CAM_Left_Right(slidCameraLeftRight.Value)); 
        }
        
        private void CAM_Servo2_CheckedChanged(object sender, EventArgs e)
        {
            if(CAM_Servo2.Checked == true)
                SendData(SendMessage.CameraServos_ON());
            else if (CAM_Servo2.Checked == false)
                SendData(SendMessage.CameraServos_OFF());
        }
        
        private void Supply_Camera_CheckedChanged(object sender, EventArgs e)
        {
            if (Supply_Camera.Checked == true)
                SendData(SendMessage.CAMERA_ON());
            if (Supply_Camera.Checked == false)
                SendData(SendMessage.CAMERA_OFF());
        }

        private void STOP_Motion_Click(object sender, EventArgs e)
        {
            SendData(SendMessage.MotionStop());
            slidMotion.Value = 63;
        }

        private void endClick(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void buttClearLogs_Click(object sender, EventArgs e)
        {
            textLogs.Clear();
        }
        
        private void endApplicationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //ThreadCheckStat.Suspend();
            ThreadCheckStat.Abort();
            this.Close();
        }

        private void Supply_Lights_CheckedChanged(object sender, EventArgs e)
        {

        }
        
        void Analyse_Data(string dataForAnalyse)
        {
            try
            {

            if (dataForAnalyse == null)
            {
                dataForAnalyse = "123456678901234567890";
            }
                textLogs.Paste(dataForAnalyse);
                char[] USART_data               = dataForAnalyse.ToCharArray();
                string CTR_MSG                  = new string(USART_data, 0, 6);
                //SetMap();

            if (CTR_MSG == "$GPRMC")
                {
                    string GPS_RMC_Data             = new string(USART_data, 0, USART_data.Length);

                    GPS_PositionStatus.Text         = GPS.GPS_Status(GPS_RMC_Data);
                    GPS_PositionStatus2.Text        = GPS.GPS_Status(GPS_RMC_Data);
                    GPS_Position_Date.Text          = GPS.GPS_Date(GPS_RMC_Data);
                    GPS_Position_Latitude.Text      = GPS.GPS_Latitude(GPS_RMC_Data, "info");
                    GPS_Position_Latitude_Map.Text  = GPS.GPS_Latitude(GPS_RMC_Data, "info");
                    GPS_Position_Longitude.Text     = GPS.GPS_Longitude(GPS_RMC_Data, "info");
                    GPS_Position_Longitude_Map.Text = GPS.GPS_Longitude(GPS_RMC_Data, "info");
                    GPS_Position_Speed.Text         = GPS.GPS_Speed(GPS_RMC_Data);
                    GPS_Time.Text                   = GPS.GPS_UTC_Time(GPS_RMC_Data);
                    Course_Ovr_Ground.Text          = GPS.GPS_Course(GPS_RMC_Data);
                    GPS_Latitude                    = float.Parse(GPS.GPS_Latitude(GPS_RMC_Data, "number"));
                    GPS_Longtitude                  = float.Parse(GPS.GPS_Longitude(GPS_RMC_Data, "number"));

                    number_course = (GPS.GPS_Course2(GPS_RMC_Data))/10;
                    textBox6.Text = number_course.ToString();
                    textBox7.Text = GPS.GPS_Course(GPS_RMC_Data);
                    xc1 = (float)(50 * Math.Cos((number_course * Math.PI)/ 180));
                    yc1 = (float)(50 * Math.Sin((number_course * Math.PI)/ 180));

                    if (GPS.GPS_Status(GPS_RMC_Data) == "Valid position")
                    {
                        GraphicsOnMap               = map.CreateGraphics();
                        lat                   = float.Parse(GPS.GPS_Latitude(GPS_RMC_Data, "number"));
                        lon                   = float.Parse(GPS.GPS_Longitude(GPS_RMC_Data, "number"));
                        x                       = Convert.ToInt16(map.Size.Width / (mapSizeXmax - mapSizeXmin) * (lon - mapSizeXmin));
                        y                       = Convert.ToInt16(map.Size.Height / (mapSizeYmax - mapSizeYmin) * (mapSizeYmax - lat));
                        //Unlock Trajectory


                        if (Maps.Text != "TUKE_new")
                        {
                            GraphicsOnMap.DrawLine(penLinesOnMap, x, 0, x, map.Size.Height);
                            GraphicsOnMap.DrawLine(penLinesOnMap, 0, y, map.Size.Width, y);
                            GraphicsOnMap.DrawRectangle(penLinesOnMap, x - 18, y - 18, 36, 36);
                            GraphicsOnMap.DrawEllipse(penPointOnMap, x - 3, y - 3, 6, 6);
                        }
                        if (Maps.Text == "TUKE_new")
                        {
                            GraphicsOnMap.DrawEllipse(penPointOnMap, x - 5, y - 5, 10, 10);
                            GraphicsOnMap.DrawLine(penPointOnMap, x, y, x+xc1, y+yc1);
                            /*for (x = 0; x < 20; x++)
                            {
                            GraphicsOnMap.DrawLine(penPointOnMap, PointX[a], PointX[b], PointX[a++], PointX[b++]);
                            }
                            PointX[a++] = x;
                            PointY[b++] = y;*/                            
                        }
                       
                    }
                }


                else if (CTR_MSG == "$MSG1,")
                {
                    string battery                  = new string(USART_data, 0, 7);
                    textBatteryVoltage.Text         = Battery(battery);
                }

                else if (CTR_MSG == "$MSG2S")
                {
                    string sonar = new string(USART_data, 0, 7);
                    textSonarValue.Text = Sensors(sonar);
                }

                else if (CTR_MSG == "$MSG3F")
                {
                    string infrafront = new string(USART_data, 0, 7);
                    textInfraFront.Text = SensorsF(infrafront);
                }

                else if (CTR_MSG == "$MSG3B")
                {
                    string infraback = new string(USART_data, 0, 7);
                    textInfraBack.Text = SensorsB(infraback);
                }

                else if (CTR_MSG == "$MSG4L")
                {
                    string micleft = new string(USART_data, 0, 7);
                    //textMicLeft.Text = Sensors(micleft);
                }

                else if (CTR_MSG == "$MSG4R")
                {
                    string micright = new string(USART_data, 0, 7);
                    //textMicRight.Text = Sensors(micright);
                }

                else if (CTR_MSG == "$GPGSV")
                {
                    char[] GPGSV_msg                = new string(USART_data, 0, USART_data.Length).ToCharArray();
                    numberOfMsgs                    = Convert.ToInt16(GPGSV_msg[7].ToString()); // 1 or 2 or 3
                    int Msg_number                  = Convert.ToInt16(GPGSV_msg[9].ToString());
                    numberOfSatellites              = Convert.ToInt16(new string(GPGSV_msg, 11, 2));
                    GPGSV_Data[Msg_number]          = new string(GPGSV_msg, 14, (GPGSV_msg.Length - 19));
                    
                    if (numberOfMsgs == Msg_number)
                    {
                        Graph                       = ViewSatellites.CreateGraphics();
                        Graph.Clear(Color.White);
                        Pen pen1                    = new Pen(Color.Black, 1);
                        Pen pen2                    = new Pen(Color.Red, 1);
                        SolidBrush color_BLUE       = new SolidBrush(Color.Blue);
                        SolidBrush color_WHITE      = new SolidBrush(Color.White);
                        Font script1                = new Font(FontFamily.GenericSansSerif, 7, FontStyle.Bold);

                        Satellite_Map(pen1);

                        Sat_View.Text       = numberOfSatellites.ToString();
                        data1               = GPGSV_Data[1] + "," + GPGSV_Data[2] + "," + GPGSV_Data[3] + ",";
                        char[] GPS_GSV_Data = data1.ToCharArray();
                        try
                        {

                            for (int i = 0; i < numberOfSatellites; i++)
                            {
                                char[] Sat = (new string(GPS_GSV_Data, i * 13, 13)).ToCharArray();
                                int Azimuth = int.Parse(new string(Sat, 6, 3));
                                int Elevation = int.Parse(new string(Sat, 3, 2));
                                int Sat_ID = int.Parse(new string(Sat, 0, 2));
                                int SNR = int.Parse(new string(Sat, 10, 2));

                                Analyse_SNR_SatID(SNR, Sat_ID);

                                double Elevation_rad = Elevation * (Math.PI / 180);
                                int ax_x;
                                int ax_y;

                                double s = Math.Cos(Elevation_rad) * 90;

                                if (Azimuth >= 0 && Azimuth <= 90)      /////////////////// 1.KV
                                {
                                    double Azimuth_rad = Azimuth * (Math.PI / 180);

                                    int x = (int)(Math.Sin(Azimuth_rad) * s);
                                    int y = (int)(Math.Cos(Azimuth_rad) * s);

                                    ax_x = 100 + x;
                                    ax_y = 100 - y;
                                    Graph.FillEllipse(ColorSatellite, ax_x - 8, ax_y - 8, 16, 16);
                                    Graph.DrawString(Sat_ID.ToString(), script1, color_WHITE, ax_x - scriptCor1, ax_y - 6);

                                }
                                if (Azimuth > 90 && Azimuth <= 180)    /////////////////// 2.KV
                                {
                                    double Azimuth_rad = (Azimuth - 90) * (Math.PI / 180);

                                    int y = (int)(Math.Sin(Azimuth_rad) * s);
                                    int x = (int)(Math.Cos(Azimuth_rad) * s);

                                    ax_x = 100 + x;
                                    ax_y = 100 + y;
                                    Graph.FillEllipse(ColorSatellite, ax_x - 8, ax_y - 8, 16, 16);
                                    Graph.DrawString(Sat_ID.ToString(), script1, color_WHITE, ax_x - scriptCor1, ax_y - 6);
                                }
                                if (Azimuth > 180 && Azimuth <= 270)   /////////////////// 3.KV
                                {
                                    double Azimuth_rad = (Azimuth - 180) * (Math.PI / 180);

                                    int x = (int)(Math.Sin(Azimuth_rad) * s);
                                    int y = (int)(Math.Cos(Azimuth_rad) * s);

                                    ax_x = 100 - x;
                                    ax_y = 100 + y;
                                    Graph.FillEllipse(ColorSatellite, ax_x - 8, ax_y - 8, 16, 16);
                                    Graph.DrawString(Sat_ID.ToString(), script1, color_WHITE, ax_x - scriptCor1, ax_y - 6);
                                }
                                if (Azimuth > 270 && Azimuth <= 359)   /////////////////// 4.KV
                                {
                                    double Azimuth_rad = (Azimuth - 270) * (Math.PI / 180);

                                    int y = (int)(Math.Sin(Azimuth_rad) * s);
                                    int x = (int)(Math.Cos(Azimuth_rad) * s);

                                    ax_x = 100 - x;
                                    ax_y = 100 - y;
                                    Graph.FillEllipse(ColorSatellite, ax_x - 8, ax_y - 8, 16, 16);
                                    Graph.DrawString(Sat_ID.ToString(), script1, color_WHITE, ax_x - scriptCor1, ax_y - 6);
                                }
                            }
                        }
                        catch {}
                    }
                }

                if (serialPort1.IsOpen)
                {
                    serialPort1.DiscardInBuffer();
                }
            }
            catch {}

        }
        //Batery String
        private string Battery(string battery)
        {
            char[] bat              = battery.ToCharArray();
            int ADC_voltage         = Convert.ToInt16(bat[6]);
            batteryStatus.Value     = ADC_voltage;
            double voltage          = 0.10317 * ADC_voltage;
            batt.aktualne_nap = (float)voltage; //Test battery             
            return (" " + voltage.ToString("f1") + " V");
        }
        //Ultrasonic String
        private string Sensors(string sensors)
        {
            char[] sens = sensors.ToCharArray();
            int intsens = (Convert.ToInt16(sens[6]))*2;
            UltraSonic = intsens;
            //Texbox Export
            return (intsens.ToString("f1"));
        }
        //Infra Front String
        private string SensorsF(string sensors)
        {
            char[] sens = sensors.ToCharArray();
            int intsens = (Convert.ToInt16(sens[6]))-48;
            InfraFront = intsens;
            //Texbox Export
            return (intsens.ToString("f1"));
        }
        //Infra Back String
        private string SensorsB(string sensors)
        {
            char[] sens = sensors.ToCharArray();
            int intsens = (Convert.ToInt16(sens[6]))-48;
            InfraBack = intsens;
            //Texbox Export
            return (intsens.ToString("f1"));
        }

        private void Satellite_Map(Pen pen1)
        {
            Graph.DrawEllipse(pen1, 10, 10, 180, 180);
            Graph.DrawEllipse(pen1, 35, 35, 130, 130);
            Graph.DrawEllipse(pen1, 60, 60, 80, 80);
            Graph.DrawLine(pen1, 10, 100, 190, 100);
            Graph.DrawLine(pen1, 100, 10, 100, 190);
        }

        private void Analyse_SNR_SatID(int SNR, int Sat_ID)
        {
            if (SNR <= 25)                                          // Analyse SNR
            {
                ColorSatellite = new SolidBrush(Color.Red);
            }
            else if (SNR > 25 && SNR <=50)
            {
                ColorSatellite = new SolidBrush(Color.Green);
            }
            else if (SNR > 50)
            {
                ColorSatellite = new SolidBrush(Color.Blue);
            }
            if (Sat_ID < 10) // Analyse Satellite ID 
            {
                scriptCor1 = 4;
            }
            else
            {
                scriptCor1 = 7;
            }
        }

        private void TCPClient_Click(object sender, EventArgs e)
        {
            TCPClient.Image = Properties.Resources.apply;
            SerialPort.Image = Properties.Resources.blank;
            TCPServer.Image = Properties.Resources.blank;
            groupDirectionAndMotion.Enabled         = false;
            groupCameraRotation.Enabled             = false;
            groupCameraRot2.Enabled                 = false;
            groupComPortSettings.Enabled            = false;
            groupComPortDefault.Enabled             = false;
            groupTcpClientServerSettings.Enabled    = true;
            groupTcpClientServerDefault.Enabled     = true;
            textTCPClientServerIPAddress.ReadOnly   = false;
            textTCPCommunicationType.Text           = "CLIENT";
            labelCommunicationType.Text             = "TCP Client";
            toolStripconnect.Enabled                = true;
        }

        private void TCPServer_Click(object sender, EventArgs e)
        {
            TCPClient.Image = Properties.Resources.blank;
            SerialPort.Image = Properties.Resources.blank;
            TCPServer.Image = Properties.Resources.apply;
            groupDirectionAndMotion.Enabled         = false;
            groupCameraRotation.Enabled             = false;
            groupCameraRot2.Enabled                 = false;
            groupComPortSettings.Enabled            = false;
            groupComPortDefault.Enabled             = false;
            groupTcpClientServerSettings.Enabled    = true;
            groupTcpClientServerDefault.Enabled     = true;
            //IPHostEntry ihe                         = Dns.GetHostByName(Dns.GetHostName());
            //IPAddress localIpAddress                = ihe.AddressList[0];
            //textTCPClientServerIPAddress.Text       = localIpAddress.ToString();  //Local IP Address
            textTCPClientServerIPAddress.ReadOnly   = true;
            textTCPCommunicationType.Text           = "SERVER";
            labelCommunicationType.Text             = "TCP Server";
            toolStripconnect.Enabled                = true;
        }

        private void Robot_FormClosing(object sender, FormClosingEventArgs e)
        {
            ////3D
            bDoLoop = false;
            //// /3D
        }

        private void Robot_Load(object sender, EventArgs e)
        {
            groupComPortDefault.Enabled = false;
            groupComPortSettings.Enabled = false;
            groupTcpClientServerSettings.Enabled = false;
            groupTcpClientServerDefault.Enabled = false;
            groupDirectionAndMotion.Enabled = false;
            groupCameraRotation.Enabled = false;
            groupCameraRot2.Enabled = false;
            groupAdvencedSuppDevices.Enabled = false;
            groupJoystickInit.Enabled = false;
            labelCommunicationType.Text = null;
            labelConnectingStatus.Text = null;
            labelJoystickName.Text = null;
            SetMap();
            Maps.SelectedItem = "TUKE";

            //Full Screen
            Options op = new Options();
            op = op.ReadOptionsFromFile();
            this.combo_method.SelectedIndex = op.Scale;
            this.combo_streams.SelectedIndex = op.Streams;
            this.textBox1.Text = op.Text;
            //this.lbl_foreColor.BackColor = ColorTranslator.FromHtml(op.ForeColor);
        }

        private void SerialPort_Click(object sender, EventArgs e)
        {
            TCPClient.Image = Properties.Resources.blank;
            SerialPort.Image = Properties.Resources.apply;
            TCPServer.Image = Properties.Resources.blank;
            groupTcpClientServerSettings.Enabled    = false;
            groupTcpClientServerDefault.Enabled     = false;
            groupComPortDefault.Enabled             = true;
            groupComPortSettings.Enabled            = true;
            textTCPClientServerIPAddress.ReadOnly   = false;
            labelCommunicationType.Text             = "RS232";
            toolStripconnect.Enabled                = true;
        }

        private void COM_Port_Default_Click(object sender, EventArgs e)
        {
            comboComPortName.Text       = "COM3";
            comboComPortBaudrate.Text   = "19200";
            comboComPortParity.Text     = "none";
            comboComPortDataBits.Text   = "8";
            comboComPortStopBits.Text   = "1";
        }

        private void TCP_ClientServer_Default_Click(object sender, EventArgs e)
        {
            textBox26.Text = "http://147.232.20.242:80/mjpeg.cgi";
            textTCPClientServerIPAddress.Text   = "147.232.20.241";
            textTCPClientServerPortNumber.Text  = "1470";

        }
   
        private void Connecting()
        {
            WorkerThread = new Thread(new ThreadStart(InWorkerThread));
            WorkerThread.Start();
            
            if (labelCommunicationType.Text == "RS232")
            {
                SetSerialPort();
                if (serialPort1.IsOpen)
                {
                    Connecting_status_1.Text    = "Connected";
                    labelConnectingStatus.Text  = "Connected"; 
                }

            }
            else if (labelCommunicationType.Text == "TCP Server")
            {
                TcpSrvr.TCP_Server_Start(textTCPClientServerIPAddress.Text, Convert.ToInt16(textTCPClientServerPortNumber.Text));
                TcpSrvr.ServerDataReceived  +=new Communication.ServerDelegate(TcpSrvr_ServerDataReceived);
                textConnectingStatus.Text   = "Listening";
                labelConnectingStatus.Text  = "Connected";
            }
            else if (labelCommunicationType.Text == "TCP Client")
            {
                TcpClnt.TCP_Client_Start(textTCPClientServerIPAddress.Text, Convert.ToInt16(textTCPClientServerPortNumber.Text));
                TcpClnt.ClientDataReceived  +=new Communication.ClientDelegate(TcpClnt_ClientDataReceived);
                textConnectingStatus.Text   = "Connected";
                labelConnectingStatus.Text  = "Connected";
                
            }
        }
        private void Disconnecting()
        {
            try
            {
                if (labelCommunicationType.Text == "RS232")
                {
                    //serialPort1.DataReceived  -= new System.IO.Ports.SerialDataReceivedEventHandler(SerialPort_DataReceived);
                    serialPort1.DiscardInBuffer();
                    serialPort1.Close();
                    Connecting_status_1.Text    = "Disconnected";
                    labelConnectingStatus.Text  = Connecting_status_1.Text;
                }
                else if (labelCommunicationType.Text == "TCP Client")
                {
                    TcpClnt.ClientDataReceived  -= new Communication.ClientDelegate(TcpClnt_ClientDataReceived);
                    TcpClnt.TCP_Client_Stop();
                    textConnectingStatus.Text   = "Disconnected";
                    labelConnectingStatus.Text  = textConnectingStatus.Text;
                }
                else if (labelCommunicationType.Text == "TCP Server")
                {
                    TcpSrvr.ServerDataReceived  -= new Communication.ServerDelegate(TcpSrvr_ServerDataReceived);
                    TcpSrvr.TCP_Server_Stop();
                    textConnectingStatus.Text   = "";
                    labelConnectingStatus.Text  = "Disconnected";
                }
                if (Joystick.joystickStatus)
                {
                    JoystickOFF();
                }
            }   
            catch {}
        }
        
        private void toolStripconnect_Click(object sender, EventArgs e)
        {
            Ping pingSender = new Ping();
            //PingReply reply = pingSender.Send("147.232.20.1");
            PingReply reply = pingSender.Send(textTCPClientServerIPAddress.Text);
            if (reply.Status == IPStatus.Success)
            {
                toolStripdisconnect.Enabled = true;
                toolStripconnect.Enabled = false;
                settings.Enabled = false;
                groupComPortDefault.Enabled = false;
                groupComPortSettings.Enabled = false;
                groupTcpClientServerSettings.Enabled = false;
                groupTcpClientServerDefault.Enabled = false;
                groupDirectionAndMotion.Enabled = true;
                groupCameraRotation.Enabled = true;
                groupCameraRot2.Enabled = true;
                groupAdvencedSuppDevices.Enabled = true;
                groupJoystickInit.Enabled = true;
                Connecting_status_1.Clear();
                labelConnectingStatus.ResetText();
                textConnectingStatus.Clear();

                Connecting();
            }
            else
            {
                MessageBox.Show("Robot is not connected to network, try again Later", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    
        }

        private void toolStripdisconnect_Click(object sender, EventArgs e)
        {
            TcpClnt.TCP_Client_Stop();
            WorkerThread.Abort();
            Disconnecting();
            toolStripdisconnect.Enabled         = false;
            toolStripconnect.Enabled            = true;
            settings.Enabled                    = true;
            groupDirectionAndMotion.Enabled     = false;
            groupCameraRotation.Enabled         = false;
            groupCameraRot2.Enabled             = false;
            groupAdvencedSuppDevices.Enabled    = false;
            groupJoystickInit.Enabled           = false;
            
            if (labelCommunicationType.Text == "TCP Client" || labelCommunicationType.Text == "TCP Server")
            {
                groupComPortDefault.Enabled             = false;
                groupComPortSettings.Enabled            = false;
                groupTcpClientServerSettings.Enabled    = true;
                groupTcpClientServerDefault.Enabled     = true;
            }
            else if (labelCommunicationType.Text == "RS232")
            {
                groupComPortDefault.Enabled             = true;
                groupComPortSettings.Enabled            = true;
                groupTcpClientServerSettings.Enabled    = false;
                groupTcpClientServerDefault.Enabled     = false;
                
            }
        }

        private void TcpClnt_ClientDataReceived()
        {
            HandlingData(TcpClnt.data_from_client);            
        }

        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            HandlingData(serialPort1.ReadLine());
        }

        private void TcpSrvr_ServerDataReceived()
        {
            HandlingData(TcpSrvr.data_from_server);
        }

        private void HandlingData(string IncomingData)
        {
            data = null;
            //IncomingData = null;

            if (labelCommunicationType.Text == "RS232")
            {
                data = IncomingData + "\n";
            }
            else if (labelCommunicationType.Text == "TCP Client")
            {
                data = IncomingData;
            }
            else if (labelCommunicationType.Text == "TCP Server")
            {
                data = IncomingData;
            }

            if (IncomingData != null)
            {
                InWorkerThread();
            }

        }
        
        private void SetSerialPort()
        {
            try
            {
                serialPort1             = new SerialPort();
                serialPort1.PortName    = comboComPortName.Text;                  //port name
                serialPort1.BaudRate    = Convert.ToInt32(comboComPortBaudrate.Text); //port baudrate
                
                switch (comboComPortParity.Text)                                 //Parity
                {
                    case "none":
                        serialPort1.Parity = System.IO.Ports.Parity.None;
                        break;
                    case "odd":
                        serialPort1.Parity = System.IO.Ports.Parity.Odd;
                        break;
                    case "even":
                        serialPort1.Parity = System.IO.Ports.Parity.Even;
                        break;
                    case "mark":
                        serialPort1.Parity = System.IO.Ports.Parity.Mark;
                        break;
                    case "space":
                        serialPort1.Parity = System.IO.Ports.Parity.Space;
                        break;
                    default:
                        break;
                }
                switch (comboComPortStopBits.Text)                                         //StopBits
                {
                    case "1":
                        serialPort1.StopBits = System.IO.Ports.StopBits.One;
                        break;
                    case "1.5":
                        serialPort1.StopBits = System.IO.Ports.StopBits.OnePointFive;
                        break;
                    case "2":
                        serialPort1.StopBits = System.IO.Ports.StopBits.Two;
                        break;
                    default:
                        break;
                }

                serialPort1.DataBits        = Convert.ToInt16(comboComPortDataBits.Text); //port databits
                serialPort1.DataReceived    += new System.IO.Ports.SerialDataReceivedEventHandler(SerialPort_DataReceived);
                serialPort1.Open();
            }
            catch {}
        }
        void InWorkerThread()
        {
            Analyse_Data(data);
        }
                
        private void SendData(string dataForSend)
        {
            if (labelCommunicationType.Text == "RS232")
            {
                serialPort1.WriteLine(dataForSend);               
            }
            else if (labelCommunicationType.Text == "TCP Client")
            {
                TcpClnt.Send_Data_By_Client(dataForSend);                
            }
            else if (labelCommunicationType.Text == "TCP Server")
            {
                TcpSrvr.Send_Data_By_Server(dataForSend);
            }

        }

        private void map_Click(object sender, EventArgs e)
        {//Select Finish
            if (traj == true)
            {
                TrajPointX[i] = m2x;
                TrajPointY[i] = m2y;
                TrajLong[i] = longm1;
                TrajLatt[i] = lattm1;

                if (i == 0)
                {
                    GraphicsOnMap = map.CreateGraphics();
                }
                if (i > 0)
                {
                    GraphicsOnMap.DrawLine(penLinesTraj, TrajPointX[i - 1], TrajPointY[i - 1], TrajPointX[i], TrajPointY[i]);
                    label70.Text = i.ToString();
                }
                string str = "" + i + ": x: " + TrajPointX[i] + " y: " + TrajPointY[i] + " Long: " + longm1.ToString("f6") + " Latt: " + lattm1.ToString("f6") + "\r\n"; 
                textBox8.Paste(str);
                i++;
            }
            if (i > 2)
            {
                button17.Enabled = true;
            }
        }

        private void SelectMap(object sender, EventArgs e)
        {
            SetMap();
        }

        public void ShowMapImage(string path)
        {
            
            map.SizeMode = PictureBoxSizeMode.StretchImage;
            //MapImage = new Bitmap(path);
            MapImage = (Bitmap)myManager.GetObject(path);
            map.ClientSize = new Size(575, 575); //575, 334
            map.Image = (Image)MapImage;
        }

        private void SetMap()
        {//set map
            switch (Maps.Text)
            {
                case "TUKE":
                    ShowMapImage("mapa_areal_tuke");
                    mapSizeXmax = 21.251743;
                    mapSizeXmin = 21.238385;
                    mapSizeYmax = 48.735864;
                    mapSizeYmin = 48.728874;
                    break;
                case "Kosice area":
                    ShowMapImage("mapa_kosice_okolie");
                    mapSizeXmax = 21.367078;
                    mapSizeXmin = 21.147823;
                    mapSizeYmax = 48.682404;
                    mapSizeYmin = 48.575471;
                    break;
                case "Kosice":
                    ShowMapImage("mapa_kosice");
                    mapSizeXmax = 21.357079;
                    mapSizeXmin = 21.140198;
                    mapSizeYmax = 48.771068;
                    mapSizeYmin = 48.665117;
                    break;
                case "Zlin area":
                    ShowMapImage("mapa_zlin");
                    mapSizeXmax = 17.714424;
                    mapSizeXmin = 17.612457;
                    mapSizeYmax = 49.197747;
                    mapSizeYmin = 49.250551;
                    break;
                case "TUKE_new":
                    ShowMapImage("letecka_2");
                    mapSizeXmax = 21.246147; //21.247010;
                    mapSizeXmin = 21.243141; //21.243922;
                    mapSizeYmax = 48.732886; //48.732778;
                    mapSizeYmin = 48.73055; //48.730430;
                    break;
            }
        }

        private void map_MouseMove(object sender, MouseEventArgs e)
        {//mouse position to long and lattitude
            m2x = e.X;
            m2y = e.Y;
            label18.Text = m2x.ToString();
            label22.Text = m2y.ToString();

            longm1 = (Convert.ToDouble(mapSizeXmin + ((mapSizeXmax - mapSizeXmin) / map.Size.Width) * m2x));
            lattm1 = (Convert.ToDouble(mapSizeYmin + ((mapSizeYmax - mapSizeYmin) / map.Size.Height) * m2y));

            longm.Text = longm1.ToString();
            lattm.Text = lattm1.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {//Forward
            Command.Command_Forward_Backward(int.Parse(ECommand_speed.Text) + 63, int.Parse(ECommand_time.Text), ref TcpClnt);            
        }

        private void button4_Click(object sender, EventArgs e)
        {//Backward
            Command.Command_Forward_Backward(int.Parse(ECommand_speed.Text), int.Parse(ECommand_time.Text), ref TcpClnt);
        }

        private void button5_Click(object sender, EventArgs e)
        {//Left
            Command.Command_Direction_Right_Left(int.Parse(ECommand_speed.Text), 63-int.Parse(ECommand_angle.Text), int.Parse(ECommand_time.Text), ref TcpClnt);
        }

        private void button6_Click(object sender, EventArgs e)
        {//Right
            Command.Command_Direction_Right_Left(int.Parse(ECommand_speed.Text), 63 + int.Parse(ECommand_angle.Text), int.Parse(ECommand_time.Text), ref TcpClnt);
        }

        private void button2_Click_2(object sender, EventArgs e)
        {//Stop
            Command.Command_Forward_Backward(63, 1000, ref TcpClnt);
        }  

        private void button7_Click(object sender, EventArgs e)
        {//Default tcp client
            textTCPClientServerIPAddress.Text = "127.0.0.1";
            textTCPClientServerPortNumber.Text = "1001";
            textBox26.Text = "http://127.0.0.1:8080/cam_1.cgi";
        }

        private void ECommand_angle_Leave(object sender, EventArgs e)
        {//angle test value
            if (int.Parse(ECommand_angle.Text) < 0 || int.Parse(ECommand_angle.Text) > 63)
            {
                ECommand_angle.Text = "60";
                MessageBox.Show("Bad value, interval is from 0 to 63");
            }
        }

        private void ECommand_speed_Leave(object sender, EventArgs e)
        {//speed test value
            if (int.Parse(ECommand_speed.Text) < 0 || int.Parse(ECommand_speed.Text) > 63)
            {
                ECommand_speed.Text = "40";
                MessageBox.Show("Bad value, interval is from 0 to 63");
            }
        }

        private void ECommand_time_Leave(object sender, EventArgs e)
        {//time test value
            if (int.Parse(ECommand_time.Text) < 1000 || int.Parse(ECommand_time.Text) > 30000)
            {
                ECommand_time.Text = "2000";
                MessageBox.Show("Bad value, interval is from 0 to 63");
            }
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {//Camera2 right left control
            SendData(Moves.CAM_Left_Right(trackBar2.Value));
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {//Camera2 up down control
            SendData(Moves.CAM_Up_Down(trackBar1.Value));
        }

        public void ProgramRobot(string command, int speed, int angle, int time)
        {
            //nothing condition
            if (command == "nothing" && time == 0)
            {
                Command.command_flag = false;
            }
            else if(command == "nothing" && time != 0)
            {
                Command.Command_Forward_Backward(63, time, ref TcpClnt);
            }
            //forward condition
            if(command == "forward" && speed != 0 && time != 0)
            {
                Command.Command_Forward_Backward(speed, time, ref TcpClnt);
                ////forward graphics////
                //int x = (int)(100 * ((int.Parse(textBoxrtravel.Text)) / (int.Parse(textBoxspeed.Text))));     //leght/speed*1000
                //timerforward.Interval = x;
                timerforward.Interval = time;
                timerforward.Enabled = true;
            }
            //backward condition
            if (command == "backward" && speed != 0 && time != 0)
            {
                Command.Command_Forward_Backward(speed, time, ref TcpClnt);
                ////backward graphics////
                //int x = (int)(100 * ((int.Parse(textBoxrtravel.Text)) / (int.Parse(textBoxspeed.Text))));     //leght/speed*1000
                //timerbackward.Interval = x;
                timerbackward.Interval = time;
                timerbackward.Enabled = true;
            }
            //left condition
            if (command == "left" && speed != 0 && time != 0 && angle != 0)
            {
                Command.Command_Direction_Right_Left(speed, 63 - angle, time, ref TcpClnt);
                ////left graphics////
                int x = (int)(100 * ((int.Parse(textBoxrtravel.Text)) / (int.Parse(textBoxspeed.Text))));     //leght/speed*1000
                timerleft.Interval = x;
                timerleft.Enabled = true;
            }
            //right condition
            if (command == "right" && speed != 0 && time != 0 && angle != 0)
            {
                Command.Command_Direction_Right_Left(speed, 63 + angle, time, ref TcpClnt);
                ////right graphics////
                int x = (int)(100 * ((int.Parse(textBoxrtravel.Text)) / (int.Parse(textBoxspeed.Text))));     //leght/speed*1000
                timerright.Interval = x;
                timerright.Enabled = true;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            flag2 = true;
            count = 0;
            timer1.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {//Clear
            comboBox1.Text = "nothing";
            comboBox2.Text = "nothing";
            comboBox3.Text = "nothing";
            comboBox4.Text = "nothing";
            comboBox5.Text = "nothing";
            comboBox6.Text = "nothing";
            comboBox7.Text = "nothing";
            comboBox8.Text = "nothing";
            comboBox9.Text = "nothing";
            comboBox10.Text = "nothing";
            textBox011.Text = "0";
            textBox012.Text = "0";
            textBox013.Text = "0";
            textBox021.Text = "0";
            textBox022.Text = "0";
            textBox023.Text = "0";
            textBox031.Text = "0";
            textBox032.Text = "0";
            textBox033.Text = "0";
            textBox041.Text = "0";
            textBox042.Text = "0";
            textBox043.Text = "0";
            textBox051.Text = "0";
            textBox052.Text = "0";
            textBox053.Text = "0";
            textBox061.Text = "0";
            textBox062.Text = "0";
            textBox063.Text = "0";
            textBox071.Text = "0";
            textBox072.Text = "0";
            textBox073.Text = "0";
            textBox081.Text = "0";
            textBox082.Text = "0";
            textBox083.Text = "0";
            textBox091.Text = "0";
            textBox092.Text = "0";
            textBox093.Text = "0";
            textBox0101.Text = "0";
            textBox0102.Text = "0";
            textBox0103.Text = "0";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                if (Command.command_flag == false && active_command ==1)
                {
                    Command.command_flag = true;
                    ProgramRobot(comboBox1.Text, int.Parse(textBox011.Text), int.Parse(textBox012.Text), int.Parse(textBox013.Text));
                    active_command = 2;
                }
                if (Command.command_flag == false && active_command ==2)
                {
                    Command.command_flag = true;
                    ProgramRobot(comboBox2.Text, int.Parse(textBox021.Text), int.Parse(textBox022.Text), int.Parse(textBox023.Text));
                    active_command = 3;
                }
                if (Command.command_flag == false && active_command ==3)
                {
                    Command.command_flag = true;
                    ProgramRobot(comboBox3.Text, int.Parse(textBox031.Text), int.Parse(textBox032.Text), int.Parse(textBox033.Text));
                    active_command = 4;
                }                
                if (Command.command_flag == false && active_command ==4)
                {
                    Command.command_flag = true;
                    ProgramRobot(comboBox4.Text, int.Parse(textBox041.Text), int.Parse(textBox042.Text), int.Parse(textBox043.Text));
                    active_command = 5;
                }
                if (Command.command_flag == false && active_command ==5)
                {
                    Command.command_flag = true;
                    ProgramRobot(comboBox5.Text, int.Parse(textBox051.Text), int.Parse(textBox052.Text), int.Parse(textBox053.Text));
                    active_command = 6;
                }                
                if (Command.command_flag == false && active_command ==6)
                {
                    Command.command_flag = true;
                    ProgramRobot(comboBox6.Text, int.Parse(textBox061.Text), int.Parse(textBox062.Text), int.Parse(textBox063.Text));
                    active_command = 7;
                }                
                if (Command.command_flag == false && active_command ==7)
                {
                    Command.command_flag = true;
                    ProgramRobot(comboBox7.Text, int.Parse(textBox071.Text), int.Parse(textBox072.Text), int.Parse(textBox073.Text));
                    active_command = 8;
                }                
                if (Command.command_flag == false && active_command ==8)
                {
                    Command.command_flag = true;
                    ProgramRobot(comboBox8.Text, int.Parse(textBox081.Text), int.Parse(textBox082.Text), int.Parse(textBox083.Text));
                    active_command = 9;
                }                
                if (Command.command_flag == false && active_command ==9)
                {
                    Command.command_flag = true;
                    ProgramRobot(comboBox9.Text, int.Parse(textBox091.Text), int.Parse(textBox092.Text), int.Parse(textBox093.Text));
                    active_command = 10;
                }                
                if (Command.command_flag == false && active_command ==10)
                {
                    ProgramRobot(comboBox10.Text, int.Parse(textBox0101.Text), int.Parse(textBox0102.Text), int.Parse(textBox0103.Text));
                    active_command = 0;
                }
                if (Command.command_flag == false && active_command == 0)
                {
                    active_command = 1;
                    timer1.Enabled = false;
                    MessageBox.Show("Programmed robot move accomplished");
                }
        }

        private void button12_Click(object sender, EventArgs e)
        {//draw/save
            traj = true;
            button12.Enabled = false;
            button14.Enabled = true;
            button16.Enabled = false;
        }

        private void button17_Click(object sender, EventArgs e)
        {//close shape
            GraphicsOnMap = map.CreateGraphics();
            GraphicsOnMap.DrawLine(penLinesTraj, TrajPointX[int.Parse(label70.Text)], TrajPointY[int.Parse(label70.Text)], TrajPointX[0], TrajPointY[0]);
            traj = false;
            button12.Enabled = false;
            button17.Enabled = false;
            button13.Enabled = true;
            timer2.Enabled = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {//reset
            GraphicsOnMap = map.CreateGraphics();
            map.Refresh();
            button12.Enabled = true;
            button14.Enabled = false;
            i = 0;
            TrajPointX = new int[100];
            TrajPointY = new int[100];
            TrajPointY[0] = 0;
            label70.Text = i.ToString();
            button13.Enabled = false;
            button17.Enabled = false;
            textBox8.Clear();
            button16.Enabled = true;
            timer2.Enabled = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            button12.Enabled = false;
            button15.Enabled = true;
            GraphicsOnMap = map.CreateGraphics();
            float lat = GPS_Latitude;
            float lon = GPS_Longtitude;
            int x = Convert.ToInt32(map.Size.Width / (mapSizeXmax - mapSizeXmin) * (lon - mapSizeXmin));
            int y = Convert.ToInt32(map.Size.Height / (mapSizeYmax - mapSizeYmin) * (mapSizeYmax - lat));

            TrajLong[i] = (float)lat;
            TrajLatt[i] = (float)lon;

            TrajPointX[i] = x;
            TrajPointY[i] = y;
            if (i > 0)
            {
            GraphicsOnMap.DrawLine(penLinesTraj, TrajPointX[i], TrajPointY[i], TrajPointX[i - 1], TrajPointY[i - 1]);
            label71.Text = i.ToString();
            }
            if (i > 2)
            {
                button18.Enabled = true;
            }
            string str = "" + i + ": x: " + TrajPointX[i] + " y: " + TrajPointY[i] + " Long: " + longm1.ToString("f6") + " Latt: " + lattm1.ToString("f6") + "\r\n";
            textBox8.Paste(str);
            i++;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            GraphicsOnMap = map.CreateGraphics();
            GraphicsOnMap.DrawLine(penLinesTraj, TrajPointX[int.Parse(label71.Text)], TrajPointY[int.Parse(label70.Text)], TrajPointX[0], TrajPointY[0]);
            button13.Enabled = true;
            button16.Enabled = false;
            button18.Enabled = false;

        }

        private void button15_Click(object sender, EventArgs e)
        {
            GraphicsOnMap = map.CreateGraphics();
            map.Refresh();
            button16.Enabled = true;
            button15.Enabled = false;
            i = 0;
            label71.Text = i.ToString();
            button13.Enabled = false;
            button18.Enabled = false;
            textBox8.Clear();
            button12.Enabled = true;
        }

        private void button19_Click(object sender, EventArgs e)
        {//Redraw
            int p = 0;
            for (int x = 0; x < 100; x++)
            {
                if (TrajPointX[p+1] != 0)
                {
                    GraphicsOnMap.DrawLine(penLinesTraj, TrajPointX[p], TrajPointY[p], TrajPointX[p + 1], TrajPointY[p + 1]);
                }
                p++;
                if (TrajPointX[p] == 0)
                {
                    GraphicsOnMap.DrawLine(penLinesTraj, TrajPointX[p - 1], TrajPointY[p - 1], TrajPointX[0], TrajPointY[0]);
                    break;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int p = 0;
            for (int x = 0; x < 100; x++)
            {
                if (TrajPointX[p + 1] != 0)
                {
                    GraphicsOnMap.DrawLine(penLinesTraj, TrajPointX[p], TrajPointY[p], TrajPointX[p + 1], TrajPointY[p + 1]);
                }
                p++;
                if (TrajPointX[p] == 0)
                {
                    GraphicsOnMap.DrawLine(penLinesTraj, TrajPointX[p - 1], TrajPointY[p - 1], TrajPointX[0], TrajPointY[0]);
                    break;
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            timer3.Enabled = true;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {//Robot Navigation algorithm

            //Count requested angle to check point
            int xv = TrajPointX[0] - x;
            int yv = TrajPointY[0] - y;
            double c = Math.Sqrt(xv * xv + yv * yv);
            double reqangle = (Math.Asin(yv / c)) * (Math.PI / 180);

            //Comparing requested and real angle robot action
            if (number_course - reqangle > 0)
            {//right
                Command.Command_Direction_Right_Left(int.Parse(ECommand_speed.Text), 63 + int.Parse(ECommand_angle.Text), int.Parse(ECommand_time.Text), ref TcpClnt);
            }

            if (number_course - reqangle < 0)
            {//left
                Command.Command_Direction_Right_Left(int.Parse(ECommand_speed.Text), 63 - int.Parse(ECommand_angle.Text), int.Parse(ECommand_time.Text), ref TcpClnt);
            }

        }

        private void button20_Click(object sender, EventArgs e)
        {
        Ping pingSender = new Ping();
        //PingReply reply = pingSender.Send("147.232.20.1");
        PingReply reply = pingSender.Send(textTCPClientServerIPAddress.Text);
        if (reply.Status == IPStatus.Success)
        {MessageBox.Show(string.Format("Robot IP is working(response): {0} ms",reply.RoundtripTime));}
        else { MessageBox.Show("IP not working"); }
        }

        // Open video source
        private void OpenVideoSource(IVideoSource source)
        {
            // set busy cursor
            this.Cursor = Cursors.WaitCursor;

            // close previous file
            CloseFile();

            // enable/disable motion alarm
            if (detector != null)
            {
                detector.MotionLevelCalculation = motionAlarmItem.Checked;
            }

            // create camera
            Camera camera = new Camera(source, detector);
            // start camera
            camera.Start();

            // attach camera to camera window
            cameraWindow.Camera = camera;

            // reset statistics
            statIndex = statReady = 0;

            // set event handlers
            camera.NewFrame += new EventHandler(camera_NewFrame);

            // start timer
            timer.Start();

            this.Cursor = Cursors.Default;
        }

        // Close current file
        private void CloseFile()
        {
            Camera camera = cameraWindow.Camera;

            if (camera != null)
            {
                // detach camera from camera window
                cameraWindow.Camera = null;

                // signal camera to stop
                camera.SignalToStop();
                // wait for the camera
                camera.WaitForStop();

                camera = null;

                if (detector != null)
                    detector.Reset();
            }
            intervalsToSave = 0;
        }

        // Remove any motion detectors
        private void noneMotionItem_Click(object sender, System.EventArgs e)
        {
            detector = null;
            detectorType = 0;
            SetMotionDetector();
        }

        // Select detector 1
        private void detector1MotionItem_Click(object sender, System.EventArgs e)
        {
            detector = new MotionDetector1();
            detectorType = 1;
            SetMotionDetector();
        }

        // Update motion detector
        private void SetMotionDetector()
        {
            Camera camera = cameraWindow.Camera;

            // set motion detector to camera
            if (camera != null)
            {
                camera.Lock();
                camera.MotionDetector = detector;

                // reset statistics
                statIndex = statReady = 0;
                camera.Unlock();
            }
        }

        // On new frame
        private void camera_NewFrame(object sender, System.EventArgs e)
        {
            
        }

        private void button21_Click(object sender, EventArgs e)
        {
            trackBar6.Enabled = false;
            // create video source
            MJPEGStream mjpegSource = new MJPEGStream();
            mjpegSource.VideoSource = textBox26.Text;
            // open it
            OpenVideoSource(mjpegSource);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            trackBar6.Enabled = false;
            CaptureDeviceForm form = new CaptureDeviceForm();

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                // create video source
                CaptureDevice localSource = new CaptureDevice();
                localSource.VideoSource = form.Device;
                
                // open it
                OpenVideoSource(localSource);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Camera camera = cameraWindow.Camera;

            if (camera != null)
            {
                // get number of frames for the last second
                statCount[statIndex] = camera.FramesReceived;

                // increment indexes
                if (++statIndex >= statLength)
                    statIndex = 0;
                if (statReady < statLength)
                    statReady++;

                float fps = 0;

                // calculate average value
                for (int i = 0; i < statReady; i++)
                {
                    fps += statCount[i];
                }
                fps /= statReady/10; //test

                statCount[statIndex] = 0;

                label73.Text = A1;
                label74.Text = R1;
                label75.Text = G1;
                label76.Text = B1;

                //moj kod

                label112.Text = Red_l112.ToString();
                label114.Text = Green_l114.ToString();
                label116.Text = Blue_l116.ToString();
                label118.Text = Red_l118.ToString();
                label120.Text = Green_l120.ToString();
                label119.Text = Blue_l119.ToString();
                label115.Text = Red_l115.ToString();
                label113.Text = Green_l113.ToString();
                label111.Text = Blue_l111.ToString();

                textBox23.Text = vysledok1;
                textBox24.Text = vysledok2;
                textBox22.Text = vysledok3;
                fpsPanel.Text = fps.ToString("F0") + " fps";
            }
            // descrease save counter
        }

        private void Robot_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseFile();            
            //audio stream
            m_IsRunning = false;
            if (m_pRtpSession != null)
            {
                m_pRtpSession.Close(null);
                m_pRtpSession = null;
            }
            if (m_pWaveOut != null)
            {
                m_pWaveOut.Dispose();
                m_pWaveOut = null;
            }
            if (m_pRecordStream != null)
            {
                m_pRecordStream.Dispose();
                m_pRecordStream = null;
            }
            //audio stream
            //Robot1.ActiveForm.Dispose();
            try
            {

                ThreadCheckStat.Abort();
                mjpegsmall.smallcamClose();
            }
            catch
            {
            }
        }

        private void cameraWindow_MouseMove(object sender, MouseEventArgs e)
        {
            ////mouse move////
            mv1x = (e.X); //correction
            mv1y = (e.Y); //correction
            labelclickx.Text = mv1x.ToString();
            labelclicky.Text = mv1y.ToString();
        }

        private void pictureBoxkinematics_Click(object sender, EventArgs e)
        {
            ////mouse click////
            textBoxrendx.Text = m1x.ToString();
            textBoxrendy.Text = m1y.ToString();
        }

        private void pictureBoxkinematics_MouseMove(object sender, MouseEventArgs e)
        {
            ////mouse move////
            m1x = (e.X) - 30; //correction
            m1y = -(e.Y) + 220; //correction
            labeldblclick.Text = m1x.ToString();
            labelclk.Text = m1y.ToString();
        }

        private void pictureBoxkinematics_DoubleClick(object sender, EventArgs e)
        {
            ////mouse double click////
            textBoxrstartx1.Text = m1x.ToString();
            textBoxrstarty1.Text = m1y.ToString();
        }

        private void buttondrawstartend_Click(object sender, EventArgs e)
        {
            ////draw start end////
            Kin.drawstartend(pictureBoxkinematics.CreateGraphics(), textBoxrstartx1.Text, textBoxrstarty1.Text, textBoxrangle.Text, textBoxrendx.Text, textBoxrendy.Text);    
        }

        private void buttongamestart_Click(object sender, EventArgs e)
        {
            ////Game start////
            timergame.Enabled = true;
            buttongamestart.Enabled = false;
        }

        private void buttonreset_Click(object sender, EventArgs e)
        {
            ////reset////
            textBoxrstartx1.Text = "0";
            textBoxrstarty1.Text = "0";
            textBoxrangle.Text = "45";
            Kin.start(pictureBoxkinematics.CreateGraphics(), textBoxrstartx1.Text, textBoxrstarty1.Text, textBoxrtravel.Text, textBoxrangle.Text, textBoxrendx.Text, textBoxrendy.Text);
        }

        private void buttonforward_Click(object sender, EventArgs e)
        {
            flag2 = false;
            ////forward graphics////
            int x = (int)(100 * ((int.Parse(textBoxrtravel.Text)) / (int.Parse(textBoxspeed.Text))));     //leght/speed*1000
            timerforward.Interval = x;
            timerforward.Enabled = true;
            buttonforward.Enabled = false;
            buttonbackward.Enabled = false;
            buttonleft.Enabled = false;
            buttonright.Enabled = false;
        }

        private void buttonbackward_Click(object sender, EventArgs e)
        {
            flag2 = false;
            ////backward graphics////
            int x = (int)(100 * ((int.Parse(textBoxrtravel.Text)) / (int.Parse(textBoxspeed.Text))));     //leght/speed*1000
            timerbackward.Interval = x;
            timerbackward.Enabled = true;
            buttonforward.Enabled = false;
            buttonbackward.Enabled = false;
            buttonleft.Enabled = false;
            buttonright.Enabled = false;
        }

        private void buttonleft_Click(object sender, EventArgs e)
        {
            flag2 = false;
            ////left graphics////
            int x = (int)(100 * ((int.Parse(textBoxrtravel.Text)) / (int.Parse(textBoxspeed.Text))));     //leght/speed*1000
            timerleft.Interval = x;
            timerleft.Enabled = true;
            buttonforward.Enabled = false;
            buttonbackward.Enabled = false;
            buttonleft.Enabled = false;
            buttonright.Enabled = false; 
        }

        private void buttonright_Click(object sender, EventArgs e)
        {
            flag2 = false;
            ////right graphics////
            int x = (int)(100 * ((int.Parse(textBoxrtravel.Text)) / (int.Parse(textBoxspeed.Text))));     //leght/speed*1000
            timerright.Interval = x;
            timerright.Enabled = true;
            buttonforward.Enabled = false;
            buttonbackward.Enabled = false;
            buttonleft.Enabled = false;
            buttonright.Enabled = false;    
        }

        private void buttonrandomend_Click(object sender, EventArgs e)
        {
            ////Random end////
            Random RandomClass = new Random();
            textBoxrendx.Text = (RandomClass.Next(200, 220)).ToString();
            textBoxrendy.Text = (RandomClass.Next(200, 300)).ToString();
        }

        private void buttongameend_Click(object sender, EventArgs e)
        {
            ////Game end////
            timergame.Enabled = false;
            buttongameend.Enabled = false;
            buttongamestart.Enabled = true;
            MessageBox.Show("Training Canceled");
        }

        private void buttonstart_Click(object sender, EventArgs e)
        {
            ////start////
            Kin.start(pictureBoxkinematics.CreateGraphics(), textBoxrstartx1.Text, textBoxrstarty1.Text, textBoxrtravel.Text, textBoxrangle.Text, textBoxrendx.Text, textBoxrendy.Text);
        }

        private void timergame_Tick(object sender, EventArgs e)
        {
            ////Game tick////
            if ((double.Parse(textBoxrstarty1.Text)) > (double.Parse(textBoxrendy.Text) - 20) && (double.Parse(textBoxrstarty1.Text)) < (double.Parse(textBoxrendy.Text) + 20) && (double.Parse(textBoxrstartx1.Text)) > (double.Parse(textBoxrendx.Text) - 20) && (double.Parse(textBoxrstartx1.Text)) < (double.Parse(textBoxrendx.Text) + 20))
            {
                timergame.Enabled = false;
                MessageBox.Show("Goal reached");
                buttongameend.Enabled = false;
                buttongamestart.Enabled = true;
            }
        }

        private void timerforward_Tick(object sender, EventArgs e)
        {
            ////forward function////
            if (flag2 == false)
            {
                Kin.forward(pictureBoxkinematics.CreateGraphics(), textBoxrstartx1.Text, textBoxrstarty1.Text, textBoxrtravel.Text, textBoxrangle.Text, textBoxrendx.Text, textBoxrendy.Text);
            }
            else
            {
                count = count+1;
                int fow = (seltex_speed() * seltex_time()) / 250;
                Kin.forward(pictureBoxkinematics.CreateGraphics(), textBoxrstartx1.Text, textBoxrstarty1.Text, fow.ToString(), textBoxrangle.Text, textBoxrendx.Text, textBoxrendy.Text);
            }

            //Write final to position as start
            textBoxrstartx1.Text = (Kin.x1 + Kin.x2).ToString();
            textBoxrstarty1.Text = (Kin.y1 + Kin.y2).ToString();

            //timer settings
            buttonforward.Enabled = true;
            buttonbackward.Enabled = true;
            buttonleft.Enabled = true;
            buttonright.Enabled = true;
            timerforward.Enabled = false;
        }

        private void timerbackward_Tick(object sender, EventArgs e)
        {
            if (flag2 == false)
            {
                ////backward function////
                Kin.backward(pictureBoxkinematics.CreateGraphics(), textBoxrstartx1.Text, textBoxrstarty1.Text, textBoxrtravel.Text, textBoxrangle.Text, textBoxrendx.Text, textBoxrendy.Text);
            }
            else
            {
                count = count + 1;
                int back = ((63 - (seltex_speed()) + 63) * seltex_time()) / 250;
                Kin.backward(pictureBoxkinematics.CreateGraphics(), textBoxrstartx1.Text, textBoxrstarty1.Text, back.ToString(), textBoxrangle.Text, textBoxrendx.Text, textBoxrendy.Text);
            }

            //Write final to position as start
            textBoxrstartx1.Text = (Kin.x1 - Kin.x2).ToString();
            textBoxrstarty1.Text = (Kin.y1 - Kin.y2).ToString();

            //timer settings
            timerbackward.Enabled = false;
            buttonforward.Enabled = true;
            buttonbackward.Enabled = true;
            buttonleft.Enabled = true;
            buttonright.Enabled = true;
        }

        private void timerleft_Tick(object sender, EventArgs e)
        {
            if (flag2 == false)
            {
                ////left function////
                Kin.left(pictureBoxkinematics.CreateGraphics(), textBoxrstartx1.Text, textBoxrstarty1.Text, textBoxrtravel.Text, textBoxrangle.Text, textBoxrendx.Text, textBoxrendy.Text, textBoxrlenght.Text, textBoxrmaxsteer.Text, textBoxrangletravel.Text, out Rout, out Angleout);
            }
            else
            {
                count = count + 1;
                int left = ((63 - (seltex_speed()) + 63) * seltex_time()) / 250;
                Kin.left(pictureBoxkinematics.CreateGraphics(), textBoxrstartx1.Text, textBoxrstarty1.Text, textBoxrtravel.Text, textBoxrangle.Text, textBoxrendx.Text, textBoxrendy.Text, textBoxrlenght.Text, seltex_angle().ToString(), left.ToString(), out Rout, out Angleout);
            }
            //Transform Start to End
            textBoxrstartx1.Text = (Kin.x1 - Kin.x4 + Kin.x5).ToString(); //+x4
            textBoxrstarty1.Text = (Kin.y1 + Kin.y4 + Kin.y5).ToString(); //-y4

            //Transform Angle
            //textBoxrangle.Text = ((Convert.ToDouble(textBoxrangle.Text) + Convert.ToDouble(textBoxrangletravel.Text))).ToString();
            textBoxrangle.Text = Angleout;

            //timer settings
            timerleft.Enabled = false;
            buttonforward.Enabled = true;
            buttonbackward.Enabled = true;
            buttonleft.Enabled = true;
            buttonright.Enabled = true;
        }

        private void timerright_Tick(object sender, EventArgs e)
        {
            if (flag2 == false)
            {
                ////right function////
                Kin.right(pictureBoxkinematics.CreateGraphics(), textBoxrstartx1.Text, textBoxrstarty1.Text, textBoxrtravel.Text, textBoxrangle.Text, textBoxrendx.Text, textBoxrendy.Text, textBoxrlenght.Text, textBoxrmaxsteer.Text, textBoxrangletravel.Text, out Rout, out Angleout);
            }
            else
            {
                count = count + 1;
                int right = ((63 - (seltex_speed()) + 63) * seltex_time()) / 250;
                Kin.right(pictureBoxkinematics.CreateGraphics(), textBoxrstartx1.Text, textBoxrstarty1.Text, textBoxrtravel.Text, textBoxrangle.Text, textBoxrendx.Text, textBoxrendy.Text, textBoxrlenght.Text, seltex_angle().ToString(), right.ToString(), out Rout, out Angleout);
            }
            
            
            textBoxrturnradius.Text = Rout; /////

            //Transform Start to End
            textBoxrstartx1.Text = (Kin.x1 + Kin.x4 + Kin.x5).ToString();
            textBoxrstarty1.Text = (Kin.y1 - Kin.y4 + Kin.y5).ToString();

            //Transform Angle
            //textBoxrangle.Text = ((Convert.ToDouble(textBoxrangle.Text) - Convert.ToDouble(textBoxrangletravel.Text))).ToString();
            textBoxrangle.Text = Angleout;

            timerright.Enabled = false;
            buttonforward.Enabled = true;
            buttonbackward.Enabled = true;
            buttonleft.Enabled = true;
            buttonright.Enabled = true;
        }

        private void buttonsenddata_Click(object sender, EventArgs e)
        {
            richTextBoxfile.AppendText(textBoxrstartx1.Text.ToString());
        }

        private void buttonwritefile_Click(object sender, EventArgs e)
        {
            richTextBoxfile.SaveFile(@"C:\xxx.txt", RichTextBoxStreamType.PlainText);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            trackBar6.Enabled = true;
            CloseFile();
            richTextBox2.SaveFile("motion_log.txt", RichTextBoxStreamType.PlainText);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            detector = null;
            detectorType = 0;
            SetMotionDetector();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            detector = new MotionDetector1();
            detectorType = 1;
            SetMotionDetector();
        }

        private void motionAlarmItem_CheckedChanged(object sender, EventArgs e)
        {
            //motionAlarmItem.Checked = !motionAlarmItem.Checked;            
            // enable/disable motion alarm
            if (detector != null)
            {
                Camera.AlarmDataReceived += new Camera.AlarmDelegate(Cam_AlarmDelegate);
                //TcpClnt.ClientDataReceived += new Communication.ClientDelegate(TcpClnt_ClientDataReceived);
                detector.MotionLevelCalculation = motionAlarmItem.Checked;
            }
        }

        private void Cam_AlarmDelegate()
        {
            richTextBox2.AppendText(DateTime.Now + "  Motion detected");
            richTextBox2.AppendText(Environment.NewLine);
            mot = true;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            detector = new MotionDetector2();
            detectorType = 2;
            SetMotionDetector();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            detector = new MotionDetector3();
            detectorType = 3;
            SetMotionDetector();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            detector = new MotionDetector3Optimized();
            detectorType = 4;
            SetMotionDetector();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            detector = new MotionDetector4();
            detectorType = 5;
            SetMotionDetector();
        }
        ///Motion Jpeg      

        int seltex_speed()
        {
        int sp01;
            switch (count)
            {
                case 1:
                    sp01 = int.Parse(textBox011.Text);
                    break;
                case 2:
                    sp01 = int.Parse(textBox021.Text);
                    break;
                case 3:
                    sp01 = int.Parse(textBox031.Text);
                    break;
                case 4:
                    sp01 = int.Parse(textBox041.Text);
                    break;
                case 5:
                    sp01 = int.Parse(textBox051.Text);
                    break;
                case 6:
                    sp01 = int.Parse(textBox061.Text);
                    break;
                case 7:
                    sp01 = int.Parse(textBox071.Text);
                    break;
                case 8:
                    sp01 = int.Parse(textBox081.Text);
                    break;
                case 9:
                    sp01 = int.Parse(textBox091.Text);
                    break;
                case 10:
                    sp01 = int.Parse(textBox0101.Text);
                    break;
                default:
                    sp01 = 1;
                    break;
            }
            return sp01;
        }

        int seltex_time()
        {
            int t01;
            switch (count)
            {
                case 1:
                    t01 = int.Parse(textBox013.Text);
                    break;
                case 2:
                    t01 = int.Parse(textBox023.Text);
                    break;
                case 3:
                    t01 = int.Parse(textBox033.Text);
                    break;
                case 4:
                    t01 = int.Parse(textBox043.Text);
                    break;
                case 5:
                    t01 = int.Parse(textBox053.Text);
                    break;
                case 6:
                    t01 = int.Parse(textBox063.Text);
                    break;
                case 7:
                    t01 = int.Parse(textBox073.Text);
                    break;
                case 8:
                    t01 = int.Parse(textBox083.Text);
                    break;
                case 9:
                    t01 = int.Parse(textBox093.Text);
                    break;
                case 10:
                    t01 = int.Parse(textBox0103.Text);
                    break;
                default:
                    t01 = 1;
                    break;
            }
            return t01;
        }

        int seltex_angle()
        {
            int a01;
            switch (count)
            {
                case 1:
                    a01 = int.Parse(textBox012.Text);
                    break;
                case 2:
                    a01 = int.Parse(textBox022.Text);
                    break;
                case 3:
                    a01 = int.Parse(textBox032.Text);
                    break;
                case 4:
                    a01 = int.Parse(textBox042.Text);
                    break;
                case 5:
                    a01 = int.Parse(textBox052.Text);
                    break;
                case 6:
                    a01 = int.Parse(textBox062.Text);
                    break;
                case 7:
                    a01 = int.Parse(textBox072.Text);
                    break;
                case 8:
                    a01 = int.Parse(textBox082.Text);
                    break;
                case 9:
                    a01 = int.Parse(textBox092.Text);
                    break;
                case 10:
                    a01 = int.Parse(textBox0102.Text);
                    break;
                default:
                    a01 = 1;
                    break;
            }
            return a01;
        }

        private void button49_Click(object sender, EventArgs e)
        {
            //webBrowser3.Navigate("file:///c:/gmaps2.html");
            //webBrowser3.Navigate("http://maps.google.com/staticmap?center=48.731688,21.244611&zoom=18&size=480x475&maptype=satellite&markers=48.731688,21.244611,blues&key=ABQIAAAAAHZd4HzfYHQAbT-yHdR6jhT12lc4B-AjdNcAAwoqRMhEnlbnthRPOEGr5DMGAtO_8J0kWsbSUeuQZg&sensor=true&sensor=true"); 

            //GPS Middle(Center)
            float lat1 = 48.731688f;
            float lon1 = 21.244611f;
            string lat1s = (lat1.ToString()).Replace(',','.');
            string lon1s = (lon1.ToString()).Replace(',','.');
            //GPS Robot Marker (R)
            string latr = (lat.ToString()).Replace(',','.');
            string lonr = (lon.ToString()).Replace(',', '.');

            string gps_string = string.Format("http://maps.google.com/staticmap?center={0},{1}&zoom=18&size=500x500&maptype=satellite&markers={2},{3},bluer|{4},{5},greena|{6},{7},yellowb|{8},{9},redc&key=ABQIAAAAAHZd4HzfYHQAbT-yHdR6jhT12lc4B-AjdNcAAwoqRMhEnlbnthRPOEGr5DMGAtO_8J0kWsbSUeuQZg&sensor=true&sensor=true", lat1s, lon1s, latr, lonr,textBox9.Text,textBox10.Text,textBox11.Text,textBox13.Text,textBox14.Text,textBox15.Text);
            Bitmap bmpgmaps = BitmapFromWeb(gps_string);
            pictureBox27.Image = bmpgmaps;
            //bmpgmaps.Save("image1.jpg");
        }

        public static Bitmap BitmapFromWeb(string URL)
        {
            try
            {
                // create a web request to the url of the image
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(URL);
                // set the method to GET to get the image
                myRequest.Method = "GET";
                // get the response from the webpage
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                // create a bitmap from the stream of the response
                Bitmap bmp = new Bitmap(myResponse.GetResponseStream());
                // close off the stream and the response
                myResponse.Close();
                // return the Bitmap of the image
                return bmp;
            }
            catch //(Exception ex)
            {
                return null; // if for some reason we couldn't get to image, we return null
            }
        }

        private void button50_Click(object sender, EventArgs e)
        {
            GraphicsGmaps =  pictureBox27.CreateGraphics();
            Pen xp = new Pen(Color.Red);
            GraphicsGmaps.DrawLine(xp, 0, 0, 500, 500);
            GraphicsGmaps.DrawLine(xp, 500, 0, 0, 500);
            GraphicsGmaps.DrawLine(xp, 250, 250, 5, 250);
            
            //CalculationPixel
            double x = 250 + (lat - 48.731688f) * 290909.0909f;
            double y = 250 + (21.244611f - lon) * 182170.54263f;
            //RobotMove
            GraphicsGmaps.DrawEllipse(penPointOnMap, (int)x - 5, (int)y - 5, 10, 10);
            GraphicsGmaps.DrawLine(penPointOnMap, (int)x, (int)y, (int)x + (int)xc1, (int)y - (int)yc1);
        }

        private void pictureBox27_MouseMove(object sender, MouseEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                latcent = 48.998386f;
                loncent = 21.275219f;
            }

          int mouse_gmx = e.X;
          int mouse_gmy = e.Y;
          label88.Text = mouse_gmx.ToString();
          label89.Text = mouse_gmy.ToString();
          double mouse_gmgpsx = loncent + (250 - mouse_gmx) * 0.000003526881f; //float.Parse(textBox18.Text);//0.000003526881f;
          double mouse_gmgpsy = latcent + (-250 + mouse_gmy) * 0.000005354166f; //float.Parse(textBox19.Text);//0.000005354166f;
          label94.Text = (mouse_gmgpsx.ToString("f6")).Replace(',','.');
          label95.Text = (mouse_gmgpsy.ToString("f6")).Replace(',', '.');
        }

        private void button48_Click(object sender, EventArgs e)
        {
            button51.Enabled = true;
            //GPS Middle(Center)
            latcent = lat;
            loncent = lon;
            string latcents = (lat.ToString()).Replace(',', '.');
            string loncents = (lon.ToString()).Replace(',', '.');
            //GPS Robot Marker (R)
            string latr = (lat.ToString()).Replace(',', '.');
            string lonr = (lon.ToString()).Replace(',', '.');
            //Get GPS Image
            string gps_string = string.Format("http://maps.google.com/staticmap?center={0},{1}&zoom={10}&size=500x500&maptype=satellite&markers={2},{3},bluer|{4},{5},greena|{6},{7},yellowb|{8},{9},redc&key=ABQIAAAAAHZd4HzfYHQAbT-yHdR6jhT12lc4B-AjdNcAAwoqRMhEnlbnthRPOEGr5DMGAtO_8J0kWsbSUeuQZg&sensor=true&sensor=true", latcents, loncents, latr, lonr, textBox9.Text, textBox10.Text, textBox11.Text, textBox13.Text, textBox14.Text, textBox15.Text,trackBar5.Value.ToString());
            Bitmap bmpgmaps = BitmapFromWeb(gps_string);
            pictureBox27.Image = bmpgmaps;
            bmpgmaps.Save(Application.StartupPath + "\\Resources\\image1.jpg");
        }

        private void button51_Click(object sender, EventArgs e)
        {
            textBox9.Text = "48.998000";
            textBox10.Text = "21.275000";
            textBox11.Text = "48.998386";
            textBox13.Text = "21.275219";
            textBox14.Text = "48.998386";
            textBox15.Text = "21.275219";
        }

        private void button51_Click_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                latcent = 48.998386f;
                loncent = 21.275219f;
            }
            latcent = lat;
            loncent = lon;
            //GPSTimer
            timergpss.Enabled = true;
        }

        private void button53_Click(object sender, EventArgs e)
        {
            label102.Text = lat.ToString("f6"); //kamil
            label103.Text = lon.ToString("f6"); //kamil
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {

        }

        private void timergpss_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                latcent = 48.998386f;
                loncent = 21.275219f;
            }
            label102.Text = lat.ToString("f6"); //kamil
            label103.Text = lon.ToString("f6"); //kamil
            //Draw Cross on Map
            GraphicsGmaps = pictureBox27.CreateGraphics();
            //CalculationPixel
            xpos = 250 + ( - loncent + lon ) * long.Parse(textBox16.Text);//180933.85214f  //21.275219f
            ypos = 250 + ( - lat + latcent ) * long.Parse(textBox17.Text);//292682.926829f //48.998386f

            //RobotMove
            GraphicsGmaps.DrawEllipse(penPointOnMap, (float)xpos - 5, (float)ypos - 5, 10, 10);
            GraphicsGmaps.DrawLine(penPointOnMap, (float)xpos, (float)ypos, (float)xpos + xc1, (float)ypos - yc1);

            //Richbox fill data
            gpsx[gpsi] = (float)xpos;
            gpsy[gpsi] = (float)ypos;
            gpslong[gpsi] = lon;
            gpslatt[gpsi] = lat;
            if(gpsi != 0 && (gpslong[gpsi-1] != gpslong[gpsi] || gpslatt[gpsi-1] != gpslatt[gpsi]))
            {
            richTextBox1.AppendText((gpsx[gpsi]).ToString("f2") + "_" + (gpsy[gpsi]).ToString("f2") + "_" + (gpslong[gpsi]).ToString("f6") + "_" + (gpslatt[gpsi]).ToString("f6"));
            richTextBox1.AppendText(Environment.NewLine);
            gpsi = gpsi + 1;
            //pictureBox27.Load();
            pictureBox27.Refresh();
            }
            if (gpsi == 0 && gpslong[gpsi] != 0.000000f)
            {
                richTextBox1.AppendText((gpsx[gpsi]).ToString("f2") + "_" + (gpsy[gpsi]).ToString("f2") + "_" + (gpslong[gpsi]).ToString("f6") + "_" + (gpslatt[gpsi]).ToString("f6"));
                richTextBox1.AppendText(Environment.NewLine);
                gpsi = gpsi + 1;
            }
            ////Tracking GPS
            ////Tracking GPS
            //extract lines to string array
            string[] gpsr = richTextBox1.Lines;
            string[] gpsxy = new string[1000];
            float[] gpsxloc = new float[1000];
            float[] gpsyloc = new float[1000];

            for (i = 0; i < gpsr.Length - 1; i++)
            {
                //extract value from lines
                gpsxy = gpsr[gpsri].Split('_');
                //extract x and y
                gpsxloc[gpsri] = float.Parse(gpsxy[0]);
                gpsyloc[gpsri] = float.Parse(gpsxy[1]);
                GraphicsGmaps = pictureBox27.CreateGraphics();

                if (i > 0)
                {
                    GraphicsGmaps.DrawLine(xp, gpsxloc[i - 1], gpsyloc[i - 1], gpsxloc[i], gpsyloc[i]);
                }
                gpsri = gpsri + 1;
                
            }
            gpsri = 0;

            //Graphics clear
            //GraphicsGmaps.Clear(Color.Transparent);
        }

        private void button57_Click(object sender, EventArgs e)
        {
            timergpss.Enabled = false;
        }

        private void button55_Click(object sender, EventArgs e)
        {
            //Faith//
            //Save data to text file//
            string Saved_File = "";
            saveFD2.InitialDirectory = (@"C:\");
            saveFD2.Title = "Save a text file";
            saveFD2.FileName = "";
            saveFD2.Filter = "Text Files|*.txt|All Files|*.*";

            if (saveFD2.ShowDialog() != DialogResult.Cancel)
            {
                Saved_File = saveFD2.FileName;
                richTextBox1.SaveFile(Saved_File, RichTextBoxStreamType.PlainText);
            }
            //Faith//
        }

        private void button54_Click(object sender, EventArgs e)
        {
            //Faith//
            //Load data from text file//
            string Loaded_File = "";
            openFD2.InitialDirectory = (@"C:\");
            openFD2.Title = "Save a text file";
            openFD2.FileName = "";
            openFD2.Filter = "Text Files|*.txt|All Files|*.*";

            if (openFD2.ShowDialog() != DialogResult.Cancel)
            {
                Loaded_File = openFD2.FileName;
                richTextBox1.LoadFile(Loaded_File, RichTextBoxStreamType.PlainText);
            }
            //Faith//
        }

        private void button56_Click(object sender, EventArgs e)
        {
            //extract lines to string array
            string[] gpsr = richTextBox1.Lines;
            string[] gpsxy = new string[1000];
            float[] gpsxloc = new float[1000];
            float[] gpsyloc = new float[1000];

            for ( i = 0 ; i < gpsr.Length-1 ; i++ ) 
            {
                //extract value from lines
                gpsxy = gpsr[gpsri].Split('_');
                //extract x and y
                gpsxloc[gpsri] = float.Parse(gpsxy[0]);
                gpsyloc[gpsri] = float.Parse(gpsxy[1]);
                GraphicsGmaps = pictureBox27.CreateGraphics();

                if (i > 0)
                {
                GraphicsGmaps.DrawLine(xp, gpsxloc[i - 1], gpsyloc[i - 1], gpsxloc[i], gpsyloc[i]);
                }
                gpsri = gpsri + 1;
            }
            gpsri = 0;
        }

        private void button58_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void button52_Click(object sender, EventArgs e)
        {
            string latx = "48.998386";
            string lonx = "21.275219";
            string gps_string = string.Format("http://maps.google.com/staticmap?center={0},{1}&zoom=18&size=500x500&maptype=satellite&key=ABQIAAAAAHZd4HzfYHQAbT-yHdR6jhT12lc4B-AjdNcAAwoqRMhEnlbnthRPOEGr5DMGAtO_8J0kWsbSUeuQZg&sensor=true&sensor=true", latx, lonx);
            Bitmap bmpgmaps = BitmapFromWeb(gps_string);
            pictureBox27.Image = bmpgmaps;
            bmpgmaps.Save(Application.StartupPath + "\\Resources\\imager.jpg");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                button51.Enabled = true;
                pictureBox27.Load(Application.StartupPath + "\\Resources\\imager.jpg");
            }
            else
            {
                button51.Enabled = false;
            }
        }

        private void trackBar6_ValueChanged(object sender, EventArgs e)
        {
            sens = trackBar6.Value;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Enabled == true)
            {
                filter = true;
            }
            else { filter = false; }
        }

        private void button59_Click(object sender, EventArgs e)
        {
            richTextBox2.Clear();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void button61_Click_1(object sender, EventArgs e)
        {
            timer4.Enabled = true;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            c2 = c2 + 1;
            if (c == false && c2 < 126)
            {
                SendData(Moves.CAM_Left_Right(c2));
            }
            else
            {
                timer4.Enabled = false;
                textBox25.Text = c2.ToString();
                c2 = 1;
                c = false;
            }
        }

        private void button60_Click(object sender, EventArgs e)
        {
            timer4.Enabled = false;
        }

        //SSH Client for linux connect
        private void button70_Click(object sender, EventArgs e)
        {
            try
            {
                this.terminalControl1.UserName = this.usertextBox3.Text;
                this.terminalControl1.Password = this.passtextBox2.Text;
                this.terminalControl1.Host = this.textTCPClientServerIPAddress.Text;
                this.terminalControl1.Method = WalburySoftware.ConnectionMethod.SSH2;

                this.terminalControl1.Connect();

                this.terminalControl1.SetPaneColors(Color.White, Color.Black);
                this.terminalControl1.Focus();
                ConsoleStat.Text = "Opened";
            }
            catch
            {
                MessageBox.Show("Not exist");
            }
        }

        private void button69_Click_1(object sender, EventArgs e)
        {
            if (this.terminalControl1.TerminalPane.ConnectionTag == null) // it will be null if you're not connected to anything
                return;


            Poderosa.Forms.EditRenderProfile dlg = new Poderosa.Forms.EditRenderProfile(this.terminalControl1.TerminalPane.ConnectionTag.RenderProfile);

            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            this.terminalControl1.TerminalPane.ConnectionTag.RenderProfile = dlg.Result;
            this.terminalControl1.TerminalPane.ApplyRenderProfile(dlg.Result);
        }

        private void button68_Click(object sender, EventArgs e)
        {
            this.terminalControl1.SendText("mc\n");
        }

        private void button71_Click(object sender, EventArgs e)
        {
            Ping pingSender1 = new Ping();
            PingReply reply1 = pingSender1.Send(textTCPClientServerIPAddress.Text);
            if (reply1.Status == IPStatus.Success)
            { MessageBox.Show(string.Format("Robot IP is working(response): {0} ms", reply1.RoundtripTime)); }
            else { MessageBox.Show("IP not working"); }
        }

        private void button74_Click(object sender, EventArgs e)
        {
            this.terminalControl1.SendText("clear\n");
            this.terminalControl1.ResetText();
            this.terminalControl1.Close();
            ConsoleStat.Text = "Closed";
        }

        private void button72_Click(object sender, EventArgs e)
        {
            this.terminalControl1.SendText("mc\n");
        }
        //SSH Client for linux connect

        //3D ON
        private void button75_Click(object sender, EventArgs e)
        {
            GameFlag = true;
        }

        //3D OFF
        private void button76_Click(object sender, EventArgs e)
        {
            GameFlag = false;
        }

        //Battery Monitor
        private void battmonstart_Click(object sender, EventArgs e)
        {
            if (labelConnectingStatus.Text == "Connected" && batt.aktualne_nap != 0.1f)
            {
                Graphics gr = pictureBoxbattmon.CreateGraphics();
                int combx = 10; //int.Parse(comboBoxbattmon.SelectedItem.ToString());
                string sLine2 = batt.rfiles(gr, combx);
                richTextBoxbattmon.AppendText(sLine2);
                batt.run_tr(pictureBoxbattmon.CreateGraphics());
                battmonstart.Enabled = false;
                battmonstop.Enabled = true;
                comboBoxbattmon.Enabled = true;
            }
            else
            {
                MessageBox.Show("Robot is not connected, try again later");
            }
        }

        private void battmonstop_Click(object sender, EventArgs e)
        {
            batt.Time_Thread2.Abort();
            richTextBoxbattmon.Clear();
            pictureBoxbattmon.Hide();
            pictureBoxbattmon.Show();
            battmonstart.Enabled = true;
            battmonstop.Enabled = false;
            comboBoxbattmon.Enabled = false;
        }

        private void comboBoxbattmon_SelectedValueChanged(object sender, EventArgs e)
        {
            richTextBoxbattmon.Clear();
            batt.Time_Thread2.Abort();
            string sLine2 = batt.rfiles(pictureBoxbattmon.CreateGraphics(), int.Parse(comboBoxbattmon.SelectedItem.ToString()));
            richTextBoxbattmon.AppendText(sLine2);
            batt.run_tr(pictureBoxbattmon.CreateGraphics());
        }

        private void pictureBoxbattmon_MouseMove(object sender, MouseEventArgs e)
        {
            mv1xmon = (e.X); //correction
            mv1ymon = (e.Y); //correction
            mv1ymon = (mv1ymon - 180) / (-11);

            if (batt.mierka < 5)
            {
                mv1xmon = ((mv1xmon - 20) * batt.mierka);
            }
            if (batt.mierka >= 5 && batt.mierka < 50)
            {
                mv1xmon = ((mv1xmon - 20) * batt.mierka) / 60;
            }
            if (batt.mierka >= 50)
            {
                mv1xmon = ((mv1xmon - 20) * batt.mierka) / 3600;
            }
            int rnd = 0;
            labelclickx2.Text = "Time [" + batt.value + "]  " + mv1xmon.ToString("f1");
            labelclicky2.Text = "Voltage [V]  " + mv1ymon.ToString("f1");
        }
        //Battery Monitor

        //AUDIO STREAM OLD
        private void m_pToggleRun_Click(object sender, EventArgs e)
        {
            if (m_IsRunning)
            {
                m_IsRunning = false;
                m_IsSendingTest = false;

                m_pRtpSession.Dispose();
                m_pRtpSession = null;

                m_pWaveOut.Dispose();
                m_pWaveOut = null;

                if (m_pRecordStream != null)
                {
                    m_pRecordStream.Dispose();
                    m_pRecordStream = null;
                }

                m_pOutDevices.Enabled = true;
                m_pToggleRun.Text = "Start";
                m_pRecord.Enabled = true;
                m_pRecordFile.Enabled = true;
                m_pRecordFileBrowse.Enabled = true;
                m_pRemoteIP.Enabled = false;
                m_pRemotePort.Enabled = false;
                m_pCodec.Enabled = false;
                m_pToggleMic.Text = "Send";
                m_pToggleMic.Enabled = false;
                m_pSendTestSound.Enabled = false;
                m_pSendTestSound.Text = "Send";
                m_pPlayTestSound.Enabled = false;
                m_pPlayTestSound.Text = "Play";
            }
            else
            {
                if (m_pOutDevices.SelectedIndex == -1)
                {
                    MessageBox.Show(this, "Please select output device !", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (m_pRecord.Checked && m_pRecordFile.Text == "")
                {
                    MessageBox.Show(this, "Please specify record file !", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (m_pRecord.Checked)
                {
                    m_pRecordStream = File.Create(m_pRecordFile.Text);
                }

                m_IsRunning = true;

                m_pWaveOut = new AudioOut(AudioOut.Devices[m_pOutDevices.SelectedIndex], 8000, 16, 1);

                m_pRtpSession = new RTP_MultimediaSession(RTP_Utils.GenerateCNAME());
                // --- Debug -----
                //wfrm_RTP_Debug frmRtpDebug = new wfrm_RTP_Debug(m_pRtpSession);
                //frmRtpDebug.Show();
                //-----------------
                m_pRtpSession.CreateSession(new RTP_Address(IPAddress.Parse(m_pLocalIP.Text), (int)m_pLocalPort.Value, (int)m_pLocalPort.Value + 1), new RTP_Clock(0, 8000));
                m_pRtpSession.Sessions[0].AddTarget(new RTP_Address(IPAddress.Parse(m_pRemoteIP.Text), (int)m_pRemotePort.Value, (int)m_pRemotePort.Value + 1));
                m_pRtpSession.Sessions[0].NewSendStream += new EventHandler<RTP_SendStreamEventArgs>(m_pRtpSession_NewSendStream);
                m_pRtpSession.Sessions[0].NewReceiveStream += new EventHandler<RTP_ReceiveStreamEventArgs>(m_pRtpSession_NewReceiveStream);
                m_pRtpSession.Sessions[0].Payload = 8;
                m_pRtpSession.Sessions[0].Start();

                m_pOutDevices.Enabled = false;
                m_pToggleRun.Text = "Stop";
                m_pRecord.Enabled = false;
                m_pRecordFile.Enabled = false;
                m_pRecordFileBrowse.Enabled = false;
                m_pRemoteIP.Enabled = true;
                m_pRemotePort.Enabled = true;
                m_pCodec.Enabled = true;
                m_pToggleMic.Enabled = true;
                m_pSendTestSound.Enabled = true;
                m_pSendTestSound.Text = "Send";
                m_pPlayTestSound.Enabled = true;
                m_pPlayTestSound.Text = "Play";
            }
            m_pCodec.SelectedIndex = 0;
        }

        private void m_pRecord_CheckedChanged(object sender, EventArgs e)
        {
            if (m_pRecord.Checked)
            {
                m_pRecordFile.Enabled = true;
                m_pRecordFileBrowse.Enabled = true;
            }
            else
            {
                m_pRecordFile.Enabled = false;
                m_pRecordFileBrowse.Enabled = false;
            }
        }

        private void m_pRecordFileBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "record.raw";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                m_pRecordFile.Text = dlg.FileName;
            }
        }

        private void m_pCodec_SelectedIndexChanged(object sender, EventArgs e)
        {
            // G711 a-law
            if (m_pCodec.SelectedIndex == 0)
            {
                m_pActiveCodec = new PCMA();
                m_pRtpSession.Sessions[0].Payload = 8;
            }
            // G711 u-law
            else
            {
                m_pActiveCodec = new PCMU();
                m_pRtpSession.Sessions[0].Payload = 0;
            } 
        }

        private void m_pToggleMic_Click(object sender, EventArgs e) //Send Old
        {
            wfrm_SendMic frm = new wfrm_SendMic(this, m_pRtpSession.Sessions[0]);
            frm.Show();
        }

        private void m_pSendTestSound_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.InitialDirectory = Application.StartupPath + "\\audio";
            if (dlg.ShowDialog(null) == DialogResult.OK)
            {
                wfrm_SendAudio frm = new wfrm_SendAudio(this, m_pRtpSession.Sessions[0], dlg.FileName);
                frm.Show();
            }
        }

        private void m_pPlayTestSound_Click(object sender, EventArgs e)
        {
            if (m_IsSendingTest)
            {
                m_IsSendingTest = false;
            }
            else
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.InitialDirectory = Application.StartupPath + "\\audio";
                if (dlg.ShowDialog(null) == DialogResult.OK)
                {
                    m_PlayFile = dlg.FileName;

                    m_IsSendingTest = true;

                    m_pToggleMic.Enabled = false;
                    m_pSendTestSound.Enabled = false;
                    m_pPlayTestSound.Text = "Stop";

                    Thread tr = new Thread(new ThreadStart(this.PlayTestAudio));
                    tr.Start();
                }
            }
        }

        private void PlayTestAudio()
        {
            try
            {
                using (FileStream fs = File.OpenRead(m_PlayFile))
                {
                    byte[] buffer = new byte[400];
                    int readedCount = fs.Read(buffer, 0, buffer.Length);
                    long lastSendTime = DateTime.Now.Ticks;
                    while (m_IsSendingTest && readedCount > 0)
                    {
                        // Send and read next.
                        m_pWaveOut.Write(buffer, 0, readedCount);
                        readedCount = fs.Read(buffer, 0, buffer.Length);

                        Thread.Sleep(25);

                        lastSendTime = DateTime.Now.Ticks;
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(null, "Error: " + x.ToString(), "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadWaveDevices()
        {
            // Load output devices.
            m_pOutDevices.Items.Clear();
            foreach (AudioOutDevice device in AudioOut.Devices)
            {
                m_pOutDevices.Items.Add(device.Name);
            }
            if (m_pOutDevices.Items.Count > 0)
            {
                m_pOutDevices.SelectedIndex = 0;
            }
        }

        public AudioCodec ActiveCodec
        {
            get { return m_pActiveCodec; }
        }

        private void m_pRtpSession_NewReceiveStream(object sender, RTP_ReceiveStreamEventArgs e)  //AUDIO RECEIVE OLD
        {
            this.BeginInvoke(new MethodInvoker(delegate()
            {
                wfrm_Receive frm = new wfrm_Receive(e.Stream);
                frm.Show();
            }));
        }

        private void m_pRtpSession_NewReceiveStream2(object sender, RTP_ReceiveStreamEventArgs e) //AUDIO RECEIVE NEW
        {
            this.BeginInvoke(new MethodInvoker(delegate()
            {
                wfrm_Receive2 frm2 = new wfrm_Receive2(e.Stream,this);
                //frm.Show();
            }));
        }

        private void m_pRtpSession_NewSendStream(object sender, RTP_SendStreamEventArgs e) //SEND Stream ???????
        {
            this.BeginInvoke(new MethodInvoker(delegate()
            {

            }));
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutf = new AboutForm();
            aboutf.Show();
        }

        private void button81_Click(object sender, EventArgs e)
        {
            this.terminalControl1.SendText("gst-launch-0.10 udpsrc port=10000 ! rtppcmadepay ! audio/x-alaw, channels=1, rate=8000 ! alawdec ! autoaudiosink\n");
        }

        private void button82_Click(object sender, EventArgs e)
        {
            this.terminalControl1.SendText("gst-launch-0.10 alsasrc ! queue ! audio/x-raw-int,channels=1,rate=8000 ! alawenc ! rtppcmapay mtu=1438 ! udpsink host=147.232.20.244 port=11000\n");
        }

        //Audio stream
        //Sound
        private void button79_Click(object sender, EventArgs e)
        {
            //SoundPlayer simpleSound = new SoundPlayer(@"c:\Windows\Media\chimes.wav");
            SoundPlayer simpleSound = new SoundPlayer(Application.StartupPath + "\\ding.wav");
            simpleSound.Play();
        }
        //Sound

        //Bootloader
        private void ClearLog_Click(object sender, EventArgs e)
        {
            textBoxLog.Text = "";
        }

        private void robotprogram_Click(object sender, EventArgs e)
        {
            Writefirmware("-p " + comboBoxMCU.SelectedItem.ToString() + " -P " + comboBoxport.SelectedItem.ToString() + " -c stk500v2 -U flash:r:" + filename + " -vvv");
        }

        private void Write_Click(object sender, EventArgs e)
        {
            Writefirmware("-p " + comboBoxMCU.SelectedItem.ToString() + " -P " + comboBoxport.SelectedItem.ToString() + " -c stk500v2 -U flash:w:" + filename + " -vvv");
        }

        private void Selectfile_Click(object sender, EventArgs e)
        {
            openFileHex.ShowDialog(); //open select window
            selectedfilepath = openFileHex.FileName; //acquire address
            filepath.Text = selectedfilepath; //show value in form
            filename = Path.GetFileName(selectedfilepath); // 
        }

        string Writefirmware(string argument)
        {
            Process run = new System.Diagnostics.Process();
            try
            {
                run.StartInfo.FileName = "avrdude.exe";
                run.StartInfo.Arguments = argument;
                run.StartInfo.UseShellExecute = false;
                run.StartInfo.CreateNoWindow = true;
                run.StartInfo.RedirectStandardError = true;
                run.StartInfo.RedirectStandardOutput = true;
                run.Start();
                error = run.StandardError.ReadToEnd();
                standard = run.StandardOutput.ReadToEnd();
                run.WaitForExit();
                run.Close();
                //textBoxLog.AppendText("Command: avrdude " + argument + Environment.NewLine);
                //textBoxLog.AppendText(standard.Replace("avrdude.exe: safemode:", ""));
                textBoxLog.AppendText(error.Replace("avrdude.exe", "").Replace(", or use -F to override this check", "").Replace(":", "").Replace("Version 5.5, compiled on Jan  6 2008 at 135717", "").Replace("Copyright (c) 2000-2005 Brian Dean, http//www.bdmicro.com/", "").Remove(1, 30));
                if (error.Contains("AVR device initialized and ready to accept instructions") & error.Contains("error programm enable"))
                {
                }
                return error.Replace("avrdude.exe", "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return e.Message.ToString();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HelpForm Helpf = new HelpForm();
            Helpf.Show();
        }

        private void button84_Click(object sender, EventArgs e)
        {
            //Read values
            o.Streams = combo_streams.SelectedIndex;
            o.Scale = combo_method.SelectedIndex;
            o.BgColor = ColorTranslator.ToHtml(Color.Black);//ColorTranslator.ToHtml(lbl_bgColor.BackColor);
            o.Text = textBox26.Text;
            o.Url = textBox26.Text.Split(';');
            //Run Fullscreen
            Form1 fullscrfrm = new Form1();
            fullscrfrm.Show();
        }
        //Bootloader

        //Console
        private void button73_Click(object sender, EventArgs e)
        {
            this.terminalControl1.SendText("iwconfig | grep Link \n");
            
            test2 = terminalControl1.TerminalPane.ConnectionTag.Document.LastLine.PrevLine.Text;         
            String consoleout = new String(test2);
            textBoxSignalLevel.Text = consoleout;
            labelSignal.Text = consoleout.Substring(43,3) + "dBm";
            labelQuality.Text = consoleout.Substring(23, 5);

            //this.terminalControl1.SendText("iwconfig \n");
            //test2 = terminalControl1.TerminalPane.ConnectionTag.Document.LastLine.PrevLine.Text;
            //consoleout = new String(test2);
            //labelBitrate.Text = consoleout.Substring(19, 2) + "Mb/s";
        }
        //Console
        
        //Check Status
        void CheckStatus()
        {
            while (true)
            {
                label122.Text = GPS_PositionStatus.Text;
                try
                {
                    Ping pingSender = new Ping();
                    //PingReply reply = pingSender.Send("147.232.20.1");
                    PingReply reply = pingSender.Send(textTCPClientServerIPAddress.Text);
                    if (reply.Status == IPStatus.Success)
                    { labelPingDelay.Text = string.Format(" {0} ms", reply.RoundtripTime); }
                    else
                    { labelPingDelay.Text = "error"; }
                }
                catch { }

                if (ConsoleStat.Text == "Opened" && checkBoxStatUp.Checked == true )
                {
                    this.terminalControl1.SendText("iwconfig | grep Link \n");
                    Thread.Sleep(400);
                    test2 = terminalControl1.TerminalPane.ConnectionTag.Document.LastLine.PrevLine.Text;
                    String consoleout = new String(test2);
                    textBoxSignalLevel.Text = consoleout;
                    labelSignal.Text = consoleout.Substring(43, 3) + "dBm"; //Signal
                    labelQuality.Text = consoleout.Substring(23, 5);        //Quality
                    int quality = int.Parse(consoleout.Substring(23, 2));
                    Thread.Sleep(400);
                    this.terminalControl1.SendText("iwconfig | grep Bit \n");
                    Thread.Sleep(400);
                    test2 = terminalControl1.TerminalPane.ConnectionTag.Document.LastLine.PrevLine.Text;
                    consoleout = new String(test2);
                    labelBitrate.Text = consoleout.Substring(19, 2) + "Mb/s"; //BitRate

                    this.terminalControl1.SendText("top -n1 | grep CPU \n");
                    Thread.Sleep(400);
                    test2 = terminalControl1.TerminalPane.ConnectionTag.Document.LastLine.PrevLine.PrevLine.PrevLine.Text;
                    consoleout = new String(test2);
                    labelCPU.Text = consoleout.Substring(7, 3); //CPU
                    
                    if (quality < 20) //lower than 20
                    {
                        pictureBox39.Image = Properties.Resources.wifi2r;
                    }
                    else 
                    {
                        if (quality < 35) //lower than 35
                        {
                            pictureBox39.Image = Properties.Resources.wifi2y;
                        }
                        else  //upper than 35
                        {
                            pictureBox39.Image = Properties.Resources.wifi2;
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }

        private void buttonFlite_Click(object sender, EventArgs e) //Flite
        {
            checkBoxStatUp.Checked = false;
            this.terminalControl1.SendText("iwconfig | grep Link \n");
            checkBoxStatUp.Checked = true;
        }

        private void button86_Click(object sender, EventArgs e)
        {
            //Read values
            o.Streams = combo_streams.SelectedIndex;
            o.Scale = combo_method.SelectedIndex;
            o.BgColor = ColorTranslator.ToHtml(Color.Black);//ColorTranslator.ToHtml(lbl_bgColor.BackColor);
            o.Text = textBox26.Text;
            o.Url = textBox26.Text.Split(';');
            //Run SmallCam
            mjpegsmall.Mjpegsmallcam2(pictureBoxsmallcam);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            mjpegsmall.smallcamClose();
        }

        private void connectAllDevicesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //CONTROL SYSTEM
            Ping pingSender = new Ping();
            //PingReply reply = pingSender.Send("147.232.20.1");
            PingReply reply = pingSender.Send(textTCPClientServerIPAddress.Text);
            if (reply.Status == IPStatus.Success)
            {
                toolStripdisconnect.Enabled = true;
                toolStripconnect.Enabled = false;
                settings.Enabled = false;
                groupComPortDefault.Enabled = false;
                groupComPortSettings.Enabled = false;
                groupTcpClientServerSettings.Enabled = false;
                groupTcpClientServerDefault.Enabled = false;
                groupDirectionAndMotion.Enabled = true;
                groupCameraRotation.Enabled = true;
                groupCameraRot2.Enabled = true;
                groupAdvencedSuppDevices.Enabled = true;
                groupJoystickInit.Enabled = true;
                Connecting_status_1.Clear();
                labelConnectingStatus.ResetText();
                textConnectingStatus.Clear();

                Connecting();
            }
            else
            {
                MessageBox.Show("Robot is not connected to network, try again Later", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //CONSOLE
            try
            {
                this.terminalControl1.UserName = this.usertextBox3.Text;
                this.terminalControl1.Password = this.passtextBox2.Text;
                this.terminalControl1.Host = this.textTCPClientServerIPAddress.Text;
                this.terminalControl1.Method = WalburySoftware.ConnectionMethod.SSH2;

                this.terminalControl1.Connect();

                this.terminalControl1.SetPaneColors(Color.White, Color.Black);
                this.terminalControl1.Focus();
                ConsoleStat.Text = "Opened";
            }
            catch
            {
                MessageBox.Show("Not exist");
            }

            ///SMALL CAM           
            mjpegsmall.Mjpegsmallcam2(pictureBoxsmallcam);
            
            //JOYSTICK
            if (labelJoystickStatus.Text == "OFF")
            {
                labelJoystickStatus.Text = "ON";
                Joystick.Init_Joystick_Device();
                Joystick.Joystick_START();
                labelJoystickName.Text = Joystick.JoystickName();
                Joystick.JoystickEvent += new JoystickDevice.JoystickDelagate(Joystick_JoystickEvent);
            }
            else if (labelJoystickStatus.Text == "ON")
            {
                JoystickOFF();
            }
            //AUDIO
            button85_Click_1(sender,e);

            //ULTRASONIC SCAN

            //3D VIEW
            GameFlag = true;

            //BATTERY
            if (labelConnectingStatus.Text == "Connected" && batt.aktualne_nap != 0.1f)
            {
                Graphics gr = pictureBoxbattmon.CreateGraphics();
                int combx = 10; //int.Parse(comboBoxbattmon.SelectedItem.ToString());
                string sLine2 = batt.rfiles(gr, combx);
                richTextBoxbattmon.AppendText(sLine2);
                batt.run_tr(pictureBoxbattmon.CreateGraphics());
                battmonstart.Enabled = false;
                battmonstop.Enabled = true;
                comboBoxbattmon.Enabled = true;
            }
            else
            {
                MessageBox.Show("Robot is not connected, try again later");
            }

        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            o.Streams = combo_streams.SelectedIndex;
            o.Scale = combo_method.SelectedIndex;
            o.BgColor = ColorTranslator.ToHtml(Color.Black);//ColorTranslator.ToHtml(lbl_bgColor.BackColor);
            o.Text = textBox1.Text;
            o.Url = textBox1.Text.Split(';');

            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Options));
            System.IO.StreamWriter wr = new System.IO.StreamWriter(Options.options_path);
            ser.Serialize(wr, o);

            wr.Flush();
            wr.Close();

            this.Close();
        }

        private void button47_Click_1(object sender, EventArgs e)
        {
            textTCPClientServerIPAddress.Text = "147.232.20.70";
            textTCPClientServerPortNumber.Text = "2000";
            textBox26.Text = "http://147.232.20.70:8080/?action=stream";
        }

        private void button71_Click_1(object sender, EventArgs e) //AUDIO SEND INICIALIZE
        {
            frm = new wfrm_SendMic2(this, m_pRtpSession.Sessions[0],m_pInDevices2);
            //frm.Show();
        }

        private void button83_Click(object sender, EventArgs e) //AUDIO SEND CLOSE
        {
             frm.wfrm_SendMic_FormClosing();
        }

        private void m_pToggleSend2_Click(object sender, EventArgs e) //AUDIO SEND START
        {
            frm.m_pToggleSend_Click2(m_pInDevices2, m_pToggleSend2);
        }

        private void AddString(String s)  //AUDIO DELEGATE
        {
            //m_pCodec2.Text = message;
            //m_pPacketsSent.Text = message2;
            //m_pKBSent.Text = message3;
            m_pKBSent.Text = s;
        }

        private void AddString1(String s1)  //AUDIO DELEGATE
        {
            //m_pCodec2.Text = message;
            //m_pPacketsSent.Text = message2;
            //m_pKBSent.Text = message3;
            m_pKBReceived.Text = s1;
        }

        private void button85_Click(object sender, EventArgs e)
        {
            //UI GET DEVICES
            foreach (AudioOutDevice device in AudioOut.Devices)
            {
                m_pOutputDevice.Items.Add(device.Name);
            }
            if (m_pOutputDevice.Items.Count > 0)
            {
                m_pOutputDevice.SelectedIndex = 0;
            }

            //RECEIVE START
            if (m_IsRunning)
            {
                m_IsRunning = false;
                m_IsSendingTest = false;

                m_pRtpSession.Dispose();
                m_pRtpSession = null;

                m_pWaveOut.Dispose();
                m_pWaveOut = null;

                if (m_pRecordStream != null)
                {
                    m_pRecordStream.Dispose();
                    m_pRecordStream = null;
                }

                m_pOutDevices.Enabled = true;
                m_pToggleRun2.Text = "Start";
                m_pRecord.Enabled = true;
                m_pRecordFile.Enabled = true;
                m_pRecordFileBrowse.Enabled = true;
                m_pRemoteIP.Enabled = false;
                m_pRemotePort.Enabled = false;
                m_pCodec.Enabled = false;
                m_pToggleMic.Text = "Send";
                m_pToggleMic.Enabled = false;
                m_pSendTestSound.Enabled = false;
                m_pSendTestSound.Text = "Send";
                m_pPlayTestSound.Enabled = false;
                m_pPlayTestSound.Text = "Play";
            }
            else
            {
                if (m_pOutDevices.SelectedIndex == -1)
                {
                    MessageBox.Show(this, "Please select output device !", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (m_pRecord.Checked && m_pRecordFile.Text == "")
                {
                    MessageBox.Show(this, "Please specify record file !", "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (m_pRecord.Checked)
                {
                    m_pRecordStream = File.Create(m_pRecordFile.Text);
                }

                m_IsRunning = true;

                m_pWaveOut = new AudioOut(AudioOut.Devices[m_pOutDevices.SelectedIndex], 8000, 16, 1);

                m_pRtpSession = new RTP_MultimediaSession(RTP_Utils.GenerateCNAME());
                // --- Debug -----
                //wfrm_RTP_Debug frmRtpDebug = new wfrm_RTP_Debug(m_pRtpSession);
                //frmRtpDebug.Show();
                //-----------------
                m_pRtpSession.CreateSession(new RTP_Address(IPAddress.Parse(m_pLocalIP.Text), (int)m_pLocalPort.Value, (int)m_pLocalPort.Value + 1), new RTP_Clock(0, 8000));
                m_pRtpSession.Sessions[0].AddTarget(new RTP_Address(IPAddress.Parse(m_pRemoteIP.Text), (int)m_pRemotePort.Value, (int)m_pRemotePort.Value + 1));
                m_pRtpSession.Sessions[0].NewSendStream += new EventHandler<RTP_SendStreamEventArgs>(m_pRtpSession_NewSendStream);
                m_pRtpSession.Sessions[0].NewReceiveStream += new EventHandler<RTP_ReceiveStreamEventArgs>(m_pRtpSession_NewReceiveStream2); //change
                m_pRtpSession.Sessions[0].Payload = 8;
                m_pRtpSession.Sessions[0].Start();

                m_pOutDevices.Enabled = false;
                m_pToggleRun2.Text = "Stop";
                m_pRecord.Enabled = false;
                m_pRecordFile.Enabled = false;
                m_pRecordFileBrowse.Enabled = false;
                m_pRemoteIP.Enabled = true;
                m_pRemotePort.Enabled = true;
                m_pCodec.Enabled = true;
                m_pToggleMic.Enabled = true;
                m_pSendTestSound.Enabled = true;
                m_pSendTestSound.Text = "Send";
                m_pPlayTestSound.Enabled = true;
                m_pPlayTestSound.Text = "Play";
            }
            m_pCodec.SelectedIndex = 0;
        }

        private void button85_Click_1(object sender, EventArgs e) //Activate all RTP Sound stream
        {
            button85_Click(sender, e);       //receive
            button71_Click_1(sender, e);     //send
            m_pToggleSend2_Click(sender, e); //ok
        }

        private void button87_Click(object sender, EventArgs e) //Deactivate all RTP Sound stream
        {
            m_pToggleSend2_Click(sender, e); //ok
            button83_Click(sender, e);       //exit
            button85_Click(sender, e);       //receive
        }

        private void button88_Click(object sender, EventArgs e) //OPEN PORT ACCELEROMETER
        {
            try
            {
                this.mySerialPort.Open();

                Thread.Sleep(2000);

                Utility.Timer(DirectXTimer.Start);

                this.timerMain.Interval = 75;
                this.timerMain.Enabled = true;

                this.txtOutput.Text += "Serial port opened...\r\n";
            }
            catch (Exception ex)
            {
                this.txtOutput.Text += String.Format("{0}\r\n", ex.Message);
            }
            trd = new Thread(new ThreadStart(this.drawvalue));
            trd.Start();
        }

        private void button89_Click(object sender, EventArgs e)
        {
            this.mySerialPort.Close();
        }

        private void btnMode_Click(object sender, EventArgs e)
        {
            this.b3dMode = !this.b3dMode;

            if (this.b3dMode == true)
            {
                this.btnMode.Text = "Use 2D";

                this.usrCtrlAxis2D.Visible = false;
                ///this.usrCtrlAxis3D.Visible = true;
            }
            else
            {
                this.btnMode.Text = "Use 3D";
                this.usrCtrlAxis2D.Visible = true;
                ///this.usrCtrlAxis3D.Visible = false;
            }
        }

        private void chkBoxSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            this.bUseSmoothing = this.chkBoxSmoothing.Checked;
        }

        private void drawvalue()
        {
            while (inkr < 5000)
            {
                Thread.Sleep(200);

                this.txtByte01.Text = String.Format("{0:x2}", byte010); //$
                this.txtByte02.Text = String.Format("{0:x2}", byte020); //x
                this.txtByte03.Text = String.Format("{0:x2}", byte030); //y
                this.txtByte04.Text = String.Format("{0:x2}", byte040); //z

                this.txtXaxis.Text = String.Format("{0}", nXaxis0);
                this.txtYaxis.Text = String.Format("{0}", nYaxis0);
                this.txtZaxis.Text = String.Format("{0}", nYaxis0);

                this.txtMinX.Text = this.nMinX0.ToString();
                this.txtMaxX.Text = this.nMaxX0.ToString();

                this.txtMinY.Text = this.nMinY0.ToString();
                this.txtMaxY.Text = this.nMaxY0.ToString();

                this.txtMinZ.Text = this.nMinZ0.ToString();
                this.txtMaxZ.Text = this.nMaxZ0.ToString();

                this.txtRefreshRate.Text = String.Format("{0:f4}", Utility.Timer(DirectXTimer.GetElapsedTime));
                //this.txtBytesRead.Text = nBytes.ToString();

                this.txtAngleX.Text = fAnglex0.ToString();
                this.txtAngleY.Text = fAngley0.ToString();
                this.txtAngleZ.Text = fAnglez0.ToString();

                if (fAnglex0 < 50 || fAnglex0 > 130)  //WARNING X
                {
                    num = num + 1;
                    txtAngleX.BackColor = Color.Red;
                    txtOutput.AppendText(num.ToString() + ". X axis warning dangerous tilt");
                    txtOutput.AppendText("\n");
                }
                else
                {
                    txtAngleX.BackColor = Color.White;
                }

                if (fAnglez0 < 50 || fAnglez0 > 130) //WARNING Z(Y)
                {
                    num = num + 1;
                    txtAngleZ.BackColor = Color.Red;
                    txtOutput.AppendText(num.ToString() + ". Y axis warning dangerous tilt");
                    txtOutput.AppendText("\n");
                }
                else
                {
                    txtAngleZ.BackColor = Color.White;
                }

                if (radioButton1.Checked == true)
                {
                    radio = 1;
                    selAxis = nXaxis;
                }
                if (radioButton2.Checked == true)
                {
                    radio = 2;
                    selAxis = nYaxis;
                }
                if (radioButton3.Checked == true)
                {
                    radio = 3;
                    selAxis = nZaxis;
                }
            }
        }

        private void TimerMain_Tick(object sender, EventArgs e)
        {
            if (this.mySerialPort.IsOpen == false)
                this.txtOutput.Text = "Serial port is not opened.";
            else
            {
                //                this.mySerialPort.Write(this.sendChars, 0, this.sendChars.Length);
            }
        }

        void MySerialPort_DataReceived(object objSender, SerialDataReceivedEventArgs dataReceivedEventArgs)
        {
            if (this.InvokeRequired == true)
            {
                UpdateSerialData updateHandler = new UpdateSerialData(MySerialPort_DataReceived);
                this.BeginInvoke(updateHandler, new Object[] { objSender, dataReceivedEventArgs });
                return;
            }

            if (dataReceivedEventArgs.EventType == SerialData.Chars)
            {
                Int32 nBytes = this.mySerialPort.Read(this.buffer, 0, this.mySerialPort.BytesToRead);

                lock (this.byteQueue.SyncRoot)
                {
                    for (int i = 0; i < nBytes; i++)
                        this.byteQueue.Enqueue(this.buffer[i]);
                    ProcessData();
                }
                //refresh 0
            }
            //if (dataReceivedEventArgs.EventType == SerialData.Eof)
            //{
            //    this.txtBytesRead.Text = "EOF"; //EOF = 0x1A
            //}
        }

        private void ProcessData()
        {
            if (this.byteQueue.Count < 6)
                return;

            byte01 = (Byte)this.byteQueue.Dequeue(); //$
            if (byte01 != 36)
                return;
            byte02 = (Byte)this.byteQueue.Dequeue(); //X
            byte03 = (Byte)this.byteQueue.Dequeue(); //Y
            byte04 = (Byte)this.byteQueue.Dequeue(); //Z
            byte05 = (Byte)this.byteQueue.Dequeue(); //13
            byte06 = (Byte)this.byteQueue.Dequeue(); //10

            //refresh  11
            //this.txtByte05.Text = String.Format("{0:x2}", byte05);
            //this.txtByte06.Text = String.Format("{0:x2}", byte06);
            // PWM % based on documentation formula
            //float fXaxis = (256 * this.buffer[0] + this.buffer[1]) / 100;
            //float fYaxis = (256 * this.buffer[2] + this.buffer[3]) / 100;
            //this.txtXaxis.Text = String.Format("{0:f4}", fXaxis);
            //this.txtYaxis.Text = String.Format("{0:f4}", fYaxis);

            nXaxis = Convert.ToInt32(byte02);
            nYaxis = Convert.ToInt32(byte03);
            nZaxis = Convert.ToInt32(byte04);

            //refresh 2

            if (nXaxis < this.nMinX)
                this.nMinX = nXaxis;

            if (nXaxis > this.nMaxX)
                this.nMaxX = nXaxis;

            if (nYaxis < this.nMinY)
                this.nMinY = nYaxis;

            if (nYaxis > this.nMaxY)
                this.nMaxY = nYaxis;

            if (nZaxis < this.nMinZ)
                this.nMinZ = nZaxis;

            if (nZaxis > this.nMaxZ)
                this.nMaxZ = nZaxis;

            ///refresh 3

            if (this.bUseSmoothing == true)
            {
                int nDeltaX = Math.Abs(nXaxis - this.nPrevX);
                int nDeltaY = Math.Abs(nYaxis - this.nPrevY);
                int nDeltaZ = Math.Abs(nZaxis - this.nPrevZ);

                if (nDeltaX < this.nSmoothingDelta)
                    nXaxis = this.nPrevX;

                if (nYaxis <= 0)
                    nYaxis = this.nPrevY;

                if (nDeltaY < this.nSmoothingDelta)
                    nYaxis = this.nPrevY;

                if (nDeltaZ < this.nSmoothingDelta)
                    nZaxis = this.nPrevZ;

                if (this.b3dMode == true)
                    testval =1;/////this.usrCtrlAxis3D.SetAxesValues(nXaxis, nYaxis, nZaxis);  //dorobit z
                else
                    this.usrCtrlAxis2D.SetCurrentValue(selAxis);

                this.nPrevX = nXaxis;
                this.nPrevY = nYaxis;
                this.nPrevZ = nZaxis;

                return;
            }

            //delayed write
            inkr = inkr + 1;

            if (inkr >= 1)
            {
                byte010 = byte01; //
                byte020 = byte02; //X
                byte030 = byte03; //Y
                byte040 = byte04; //Z

                nXaxis0 = nXaxis;
                nYaxis0 = nYaxis;
                nYaxis0 = nZaxis;

                nMinX0 = nMinX;//10000;
                nMaxX0 = nMaxX;//-10000;
                nMinY0 = nMinY;//10000;
                nMaxY0 = nMaxY;//-10000;
                nMinZ0 = nMinZ;//10000;
                nMaxZ0 = nMaxZ;//-10000;

                inkr = 0;
            }
            int sdssss;
            // Default
            if (this.b3dMode == true)
                sdssss=1;   //    this.usrCtrlAxis3D.SetAxesValues(nXaxis, nYaxis, nZaxis);
            else
            {
                if (radio == 1)
                {
                    this.usrCtrlAxis2D.SetCurrentValue(nXaxis);
                }
                if (radio == 2)
                {
                    this.usrCtrlAxis2D.SetCurrentValue(nYaxis);
                }
                if (radio == 3)
                {
                    this.usrCtrlAxis2D.SetCurrentValue(nZaxis);
                }
            }

        } // End of ProcessData method

        public delegate void UpdateSerialData(Object objSender, SerialDataReceivedEventArgs dataReceivedEventArgs);

        private void button90_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Add(this.usrCtrlAxis2D);
        }

        //ultra funkcia
        public void ultra()
        {
            int a = 120;
            //int d = 0;
            Pen p2 = new Pen(Brushes.Red, 2);
            //kresli os
            Obj1.DrawLine(p2, 20, 20, 20, 170);
            //kresli os
            Obj1.DrawLine(p2, 20, 160, 300, 160);
            //kreslios
            Obj1.DrawLine(p2, 300, 20, 300, 170);

            float h = 20;
            float jk = 140;
            
            //ciachovanie na osi
            while (h < 160)
            {
                Pen p4 = new Pen(Brushes.Red, 2);
                Obj1.DrawLine(p4, 10, h, 30, h);
                Obj1.DrawString(jk.ToString(), new Font("Verdana", 5), new SolidBrush(Color.Red), 300, h);
                Obj1.DrawLine(p4, 290, h, 310, h);
                Obj1.DrawString(jk.ToString(), new Font("Verdana", 5), new SolidBrush(Color.Red), 0, h);
                h = (h + 20);
                jk = (jk - 20);
            }

            float s = 20;
            float kl = -140;
            //ciachovanie na osi

            while (s < 300)
            {
                Pen p5 = new Pen(Brushes.Red, 2);
                Obj1.DrawLine(p5, s, 150, s, 170);
                Obj1.DrawString(kl.ToString(), new Font("Verdana", 4), new SolidBrush(Color.Red), s, 160);
                s = (s + 20);
                kl = (kl + 20);
            }

            while (a <= 240)
            {
                //if (checkBoxSim.Checked == false)
                //{
                //SendData(Moves.CAM_Left_Right(servoinc - 5));
                //}
                Thread.Sleep(2000);

                //vypocet sinusu prepona a protilahla strana
                xultra = Math.Sin(a * (Math.PI / 180));
                yultra = Math.Cos(a * (Math.PI / 180));

                if (checkBoxSim.Checked == false)
                {
                    yultra = (yultra * UltraSonic);
                    xultra = (xultra * UltraSonic);
                }
                else
                {
                    yultra = (yultra * bultr[ultrainc]);
                    xultra = (xultra * bultr[ultrainc]);
                    ultrainc = ultrainc + 1;
                }
                
                //pomocne premenne k mayaniu predoslej ciary
                xultrasonic = xultra;
                yultrasonic = yultra;

                float x1 = (float)(160 - xultra);

                float y1 = (float)(160 + yultra);

                float x11 = (float)(160 - xultrasonic);
                float y11 = (float)(160 + yultrasonic);

                Pen p3 = new Pen(Brushes.Black, 2);
                Pen p4 = new Pen(Brushes.White, 2);
                Pen p = new Pen(Color.Blue, 1);

                // set the Arrow
                p.EndCap = LineCap.RoundAnchor;

                //kresli ciaru                
                Obj1.DrawLine(p3, x1, y1, 160, 160);
                //vykresluje bod samotny
                Obj1.DrawLine(p, x1+2, y1+2, x1-2, y1-2);
                //kresli ciaru bielu
                Thread.Sleep(400);
                Obj1.DrawLine(p4, x11, y11, 160, 160);
                //kresli stredovy bod
                Obj1.DrawLine(p2, 160, 165 + 5, 160, 165 + 1);
                a = (a + 5);

            }
            servoinc = 126;
        }

        /////Tlacitko na scanovanie
        private void button91_Click(object sender, EventArgs e)
        {
            //ultrasonic
            Obj1 = pictureBoxUltraSonicScan.CreateGraphics();
            Thread tr1 = new Thread(ultra);
            tr1.Start();
            button61_Click_1(sender, e);
        }

        private void button92_Click(object sender, EventArgs e)
        {
            timer5.Start();
        }

        //Check Version
        public string PublishVersion()
        {
            System.Reflection.Assembly _assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();
            string ourVersion = string.Empty;
            //if running the deployed application, you can get the version
            //  from the ApplicationDeployment information. If you try
            //  to access this when you are running in Visual Studio, it will not work.
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
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

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
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

        private void button93_Click(object sender, EventArgs e)
        {
            //Tracking
            //pictureBox27.Load(Application.StartupPath + "\\Resources\\image1.jpg");
            /////3D
            lights = new TVLightEngine();
            globals = new TVGlobals();
            atmosphere = new TVAtmosphere();
            maths = new TVMathLibrary();
            materialfactory = new TVMaterialFactory();
            texturefactory = new TVTextureFactory();
            tv = new TVEngine();
            physics = new TVPhysics();

            //Setup TV
            tv.SetDebugMode(true, true);
            tv.SetDebugFile(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\debugfile.txt");
            tv.SetAntialiasing(true, CONST_TV_MULTISAMPLE_TYPE.TV_MULTISAMPLE_2_SAMPLES);

            //Enter Your Beta Username And Password Here
            //tv.SetBetaKey("", "");

            tv.SetAngleSystem(CONST_TV_ANGLE.TV_ANGLE_DEGREE);
            //tv.Init3DWindowed(this.Handle, true);
            tv.Init3DWindowed(this.pictureBox3D.Handle, true);
            tv.GetViewport().SetAutoResize(true);
            tv.DisplayFPS(true);
            tv.SetVSync(true);

            scene = new TVScene();

            input = new TVInputEngine();
            input.Initialize(true, true);

            camera = new TVCamera();
            camera = scene.GetCamera();
            camera.SetViewFrustum(45, 1000, 0.1f);
            camera.SetPosition(0, 5, -20);
            camera.SetLookAt(0, 3, 0);

            viewport = new TVViewport();
            viewport = tv.CreateViewport(this.Handle, "viewport");
            viewport.SetCamera(camera);
            viewport.SetBackgroundColor(Color.Blue.ToArgb());
            bDoLoop = true;

            InitSound();
            InitMaterials();
            InitTextures();
            InitFonts();
            InitShaders();
            InitEnvironment();
            InitPhysics();
            InitLandscape();
            InitObjects();
            InitLights();
            InitPhysicsMaterials();
            Init2DText();

            this.Show();
            this.Focus();

            GameLoop();

            //tv = null;

            //this.Close();
            //// /3D
        }

        ////3D
        private void InitSound()
        {
            //Add code here
        }

        private void InitGame2DText()
        {
            //Add code here
        }

        private void InitEnvironment()
        {
            //SkyBox
            atmosphere.SkyBox_SetTexture(globals.GetTex("SkyFront"), globals.GetTex("SkyBack"), globals.GetTex("SkyLeft"), globals.GetTex("SkyRight"), globals.GetTex("SkyTop"), globals.GetTex("SkyBottom"));
            atmosphere.SkyBox_Enable(true);
        }

        private void InitFonts()
        {
            //Add code here
        }

        private void InitShaders()
        {
            //Add code here
        }

        private void Init2DText()
        {
            //Add code here
        }

        private void InitLights()
        {
            lights.CreateDirectionalLight(new TV_3DVECTOR(1, -1, -1), 1, 1, 1, "Sun", 1);
            lights.SetLightProperties(globals.GetLight("Sun"), true, true, true);
            lights.SetSpecularLighting(true);
        }

        private void InitPhysics()
        {
            physics.Initialize();
            physics.SetSolverModel(CONST_TV_PHYSICS_SOLVER.TV_SOLVER_EXACT);
            physics.SetFrictionModel(CONST_TV_PHYSICS_FRICTION.TV_FRICTION_EXACT);
            physics.SetGlobalGravity(new TV_3DVECTOR(0, -9.8f, 0));
        }

        private void InitObjects()
        {
            #region Car
            //Building PK9
            pk_9 = scene.CreateMeshBuilder("pk");
            // load the object from an x file
            pk_9.LoadTVM(@"Models\pk8.tvm", false, false);
            // set its position
            pk_9.SetPosition(5.0f, 0.0f, 50.0f);
            // make the table 3x larger
            pk_9.SetScale(3, 3, 3);
            // rotate it 25 degrees around the Y 3D Axis
            pk_9.RotateY(25, true);
            // set the tables texture
            pk_9.SetShadowCast(true, true);
            pk_9.SetTexture(globals.GetTex("pk9tex"), 0);

            //Chassis
            m_chassis = scene.CreateMeshBuilder("mChassis");
            m_chassis.LoadTVM(@"Models\chassis.tvm", false, false);
            m_chassis.SetShadowCast(true, true);
            m_chassis.SetTexture(globals.GetTex("ChassisSTI"), 0);
            //m_chassis.SetTextureEx(0, globals.GetTex("ChassisSTI"), 1);
            //m_chassis.SetTextureEx(1, globals.GetTex("ChassisSTI"), 1);
            m_chassis.SetTexture(globals.GetTex("UnderCarriage"), 2);
            m_chassis.SetMaterial(matWindow, 1);
            m_chassis.SetAlphaTest(true, 0, true, 1);
            m_chassis.SetBlendingMode(CONST_TV_BLENDINGMODE.TV_BLEND_ADD, 1);
            m_chassis.SetCullMode(CONST_TV_CULLING.TV_DOUBLESIDED);
            m_chassis.SetShadowCast(true, true);

            //Front Left Wheel
            float scale = 1f;
            m_fl = scene.CreateMeshBuilder("mfl");
            m_fl.LoadTVM(@"Models\wheel_l.tvm", true, true);
            m_fl.SetScale(scale, scale, scale);
            m_fl.SetLightingMode(CONST_TV_LIGHTINGMODE.TV_LIGHTING_MANAGED);
            m_fl.SetMaterial(matWheels);
            m_fl.SetTexture(globals.GetTex("Wheel"));
            m_fl.SetCullMode(CONST_TV_CULLING.TV_DOUBLESIDED);
            m_fl.SetShadowCast(true, true);

            //Front Right Wheel
            m_rl = scene.CreateMeshBuilder("mrl");
            m_rl.LoadTVM(@"Models\wheel_l.tvm", true, true);
            m_rl.SetScale(scale, scale, scale);
            m_rl.SetMaterial(matWheels);
            m_rl.SetLightingMode(CONST_TV_LIGHTINGMODE.TV_LIGHTING_MANAGED);
            m_rl.SetTexture(globals.GetTex("Wheel"));
            m_rl.SetCullMode(CONST_TV_CULLING.TV_DOUBLESIDED);
            m_rl.SetShadowCast(true, false);
            m_rl.SetShadowCast(true, true);

            //Rear Left Wheel
            m_fr = scene.CreateMeshBuilder("mfr");
            m_fr.LoadTVM(@"Models\wheel_r.tvm", true, true);
            m_fr.SetScale(scale, scale, scale);
            m_fr.SetMaterial(matWheels);
            m_fr.SetLightingMode(CONST_TV_LIGHTINGMODE.TV_LIGHTING_MANAGED);
            m_fr.SetTexture(globals.GetTex("Wheel"));
            m_fr.SetCullMode(CONST_TV_CULLING.TV_DOUBLESIDED);
            m_fr.SetShadowCast(true, false);

            //Rear Right Wheel
            m_rr = scene.CreateMeshBuilder("mrr");
            m_rr.LoadTVM(@"Models\wheel_r.tvm", true, true);
            m_rr.SetScale(scale, scale, scale);
            m_rr.SetMaterial(matWheels);
            m_rr.SetLightingMode(CONST_TV_LIGHTINGMODE.TV_LIGHTING_MANAGED);
            m_rr.SetTexture(globals.GetTex("Wheel"));
            m_rr.SetCullMode(CONST_TV_CULLING.TV_DOUBLESIDED);
            m_rr.SetShadowCast(true, false);
            m_rr.SetShadowCast(true, true);

            m_chassis.SetLightingMode(CONST_TV_LIGHTINGMODE.TV_LIGHTING_MANAGED);
            m_chassis.SetMaterial(matVehicleBody);
            m_chassis.ComputeNormals();
            m_chassis.ComputeBoundings();
            m_chassis.SetScale(scale, scale, scale);
            #endregion

            //Add The Physics to the chassis
            pbi_chassis = physics.CreateMeshBody(1500, m_chassis, CONST_TV_PHYSICSBODY_BOUNDING.TV_BODY_CONVEXHULL); //1500
            physics.SetAutoFreeze(pbi_chassis, false);
            physics.SetBodyPosition(pbi_chassis, 0f, 15, 0f);
            physics.SetBodyRotation(pbi_chassis, 0f, 0f, 0f);

            //Create The Vehicle
            car_ID = physics.CreateVehicle(pbi_chassis);

            //Do Suspention Settings
            float susheight = 1.5f; //distance from chassis to wheel 0.5f
            float susplen = 1.5f; // 10
            float susshock = 40f; //Springiness of suspension 10
            float susspring = 300f; //Stiffness of suspension 400
            flw = physics.AddVehicleWheelEx(car_ID, 25f, 0.5f * scale, 0.372f * scale + 0.1f, new TV_3DVECTOR(1, 0, 0), -0.8f * scale, -susheight * scale - 0.1f, 1.25f * scale + 0.5f, 1, 0, 0, susplen, susshock, susspring, m_fl); //fl
            frw = physics.AddVehicleWheelEx(car_ID, 25f, 0.5f * scale, 0.372f * scale + 0.1f, new TV_3DVECTOR(1, 0, 0), 0.8f * scale, -susheight * scale - 0.1f, 1.25f * scale + 0.5f, 1, 0, 0, susplen, susshock, susspring, m_fr); //fr
            rlw = physics.AddVehicleWheelEx(car_ID, 25f, 0.5f * scale, 0.372f * scale + 0.1f, new TV_3DVECTOR(1, 0, 0), -0.8f * scale, -susheight * scale - 0.1f, -1.425f * scale + 0.2f, 1, 0, 0, susplen, susshock, susspring, m_rl); //rl
            rrw = physics.AddVehicleWheelEx(car_ID, 25f, 0.5f * scale, 0.372f * scale + 0.1f, new TV_3DVECTOR(1, 0, 0), 0.8f * scale, -susheight * scale - 0.1f, -1.425f * scale + 0.2f, 1, 0, 0, susplen, susshock, susspring, m_rr); //rr

            //Change the car's center of mass / make it drive better
            physics.SetBodyCenterOfMass(car_ID, new TV_3DVECTOR(0, -1.0f, 10f));

            //Add wheel frictions
            //Note that this code will also stop sliding on slopes
            float sideslip = 0.1f;
            float sideslipcoef = 0f;
            float maxlongslide = 10000f;
            float maxlongslidecoef = 0f;
            physics.SetVehicleWheelParameters(car_ID, flw, sideslip, sideslipcoef, maxlongslide, maxlongslidecoef);
            physics.SetVehicleWheelParameters(car_ID, frw, sideslip, sideslipcoef, maxlongslide, maxlongslidecoef);
            physics.SetVehicleWheelParameters(car_ID, rlw, sideslip, sideslipcoef, maxlongslide, maxlongslidecoef);
            physics.SetVehicleWheelParameters(car_ID, rrw, sideslip, sideslipcoef, maxlongslide, maxlongslidecoef);
        }

        private void InitPhysicsMaterials()
        {
            //TerrainLandscape
            pmatTerrain = physics.CreateMaterialGroup("Terrain");
            physics.SetMaterialInteractionFriction(0, pmatTerrain, 0.9f, 1f);
            physics.SetMaterialInteractionSoftness(0, pmatTerrain, 1f);
            physics.SetMaterialInteractionBounciness(0, pmatTerrain, 0.1f);
            physics.SetBodyMaterialGroup(pbLand, pmatTerrain);

            //Chassis   
            pmatChassis = physics.CreateMaterialGroup("Chassis");
            physics.SetMaterialInteractionFriction(pmatChassis, pmatTerrain, 0.3f, 0.17f);
            physics.SetMaterialInteractionBounciness(pmatChassis, pmatTerrain, 0.1f);
            physics.SetMaterialInteractionSoftness(pmatChassis, pmatTerrain, 1000f);
            physics.SetBodyMaterialGroup(pbi_chassis, pmatChassis);
        }

        private void InitMaterials()
        {
            //Create Materials
            matLand = materialfactory.CreateMaterial("land");
            matWindow = materialfactory.CreateMaterial("CarWindows");
            matVehicleBody = materialfactory.CreateMaterial("matVehicleBody");
            matWheels = materialfactory.CreateMaterial("matWheels");

            //Land
            materialfactory.SetSpecular(matLand, 0.1f, 0.1f, 0.1f, 1f);

            //Car Windows
            materialfactory.SetAmbient(matWindow, 1, 1, 1, 1);
            materialfactory.SetDiffuse(matWindow, 1, 1, 1, 1);
            materialfactory.SetSpecular(matWindow, 0.8f, 0.8f, 0.8f, 1);
            materialfactory.SetPower(matWindow, 10);
            materialfactory.SetOpacity(matWindow, 1f);

            //Vehicle Body
            materialfactory.SetAmbient(matVehicleBody, 0.2f, 0.2f, 0.2f, 1);
            materialfactory.SetDiffuse(matVehicleBody, 0.9f, 0.9f, 0.9f, 1f);
            materialfactory.SetSpecular(matVehicleBody, 1f, 1f, 1f, 1);
            materialfactory.SetPower(matVehicleBody, 100);
            materialfactory.SetEmissive(matVehicleBody, 0, 0, 0.1f, 1);

            //Wheels
            materialfactory.SetAmbient(matWheels, 0.8f, 0.8f, 0.8f, 1);
            materialfactory.SetDiffuse(matWheels, 0.2f, 0.2f, 0.2f, 1f);
            materialfactory.SetSpecular(matWheels, 0.2f, 0.2f, 0.2f, 1);
            materialfactory.SetPower(matWheels, 200);
            materialfactory.SetEmissive(matWheels, 0f, 0f, 0, 1);
        }

        private void InitTextures()
        {
            #region Car
            //Create Very Small Colored Texture For Windows
            int i = texturefactory.CreateTexture(1, 1, true, "TintedWindows");
            texturefactory.SetPixel(i, 0, 0, Color.DarkGray.ToArgb()); //Color.DarkGray.ToArgb()

            //Buildings texture
            texturefactory.LoadTexture(@"Textures\pk9tex.bmp", "pk9tex");

            //Vehicle
            texturefactory.LoadTexture(@"Textures\Windows.bmp", "Windows");
            texturefactory.LoadTexture(@"Textures\UnderCarriage.bmp", "UnderCarriage");
            texturefactory.LoadTexture(@"Textures\ChassisSTI.bmp", "ChassisSTI");
            texturefactory.LoadTexture(@"Textures\Wheel.bmp", "Wheel");
            #endregion

            //Land
            texturefactory.LoadTexture(@"Textures\grass.jpg", "Grass");

            //Sky Box
            texturefactory.LoadTexture(@"Textures\skyup.jpg", "SkyTop");
            texturefactory.LoadTexture(@"Textures\skydown.jpg", "SkyBottom");
            texturefactory.LoadTexture(@"Textures\skyleft.jpg", "SkyLeft");
            texturefactory.LoadTexture(@"Textures\skyright.jpg", "SkyRight");
            texturefactory.LoadTexture(@"Textures\skyfront.jpg", "SkyFront");
            texturefactory.LoadTexture(@"Textures\skyback.jpg", "SkyBack");
        }

        private void InitLandscape()
        {
            Land = scene.CreateLandscape("Land");

            Land.SetAffineLevel(CONST_TV_LANDSCAPE_AFFINE.TV_AFFINE_LOW);
            int twidth = (64 * 8) / 256;
            int theight = (64 * 8) / 256;

            Land.GenerateTerrain(@"Heightmaps\heightmap.bmp", CONST_TV_LANDSCAPE_PRECISION.TV_PRECISION_HIGH, twidth / 2, theight / 2, 0, 0, 0, true);

            Land.SetTexture(globals.GetTex("Grass"));
            Land.SetMaterial(matLand);
            Land.SetTextureScale(0.3f, 0.3f);
            Land.SetPosition(-((twidth * 256) / 2) + 120, 0, -((theight * 256) / 2) + 120);
            pbLand = physics.CreateStaticTerrainBody(Land);
        }

        private void GameLoop()
        {

            while (bDoLoop)
            {
                if (GameFlag == false)
                {
                    //if (this.Focused)
                    //{
                    physics.Simulate(tv.TimeElapsed() * 0.0025f);
                    CheckInput();

                    //My Code
                    MTV3D65.TV_3DVECTOR carpos = physics.GetBodyPosition(pbi_chassis);//           .ToString();
                    MTV3D65.TV_3DVECTOR carang = physics.GetBodyRotation(pbi_chassis);
                    label81.Text = carpos.x.ToString("f1");
                    label80.Text = carpos.y.ToString("f1");
                    label79.Text = carpos.z.ToString("f1");
                    label78.Text = (((carang.y) * 180) / (Math.PI)).ToString("f2");

                    tv.Clear(false);

                    //Render Atmosphere
                    atmosphere.SkyBox_Render();

                    //Render Objets
                    pk_9.Render();
                    Land.Render();
                    m_chassis.Render();
                    m_fl.Render();
                    m_fr.Render();
                    m_rl.Render();
                    m_rr.Render();
                    //Render Transparent Objects

                    scene.FinalizeShadows();

                    //Lastly Render 2DText or Interface
                    DrawInterface();

                    tv.RenderToScreen();

                    //}
                    //else
                    //{
                    //    System.Threading.Thread.Sleep(100);
                    //}
                }
                Application.DoEvents();

            }
        }

        private void DrawInterface()
        {

            try
            {
                //Add Code Here
            }
            catch { }
        }

        private void CheckInput()
        {
            if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_ESCAPE))
            {
                bDoLoop = false;
            }
            float speed = 0.4f;
            float mousespeed = 0.3f;
            if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_W))
            {
                camera.MoveRelative(speed, 0, 0, true);
            }
            if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_S))
            {
                camera.MoveRelative(-speed, 0, 0, true);
            }
            if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_A))
            {
                camera.MoveRelative(0, 0, -speed, true);
            }
            if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_D))
            {
                camera.MoveRelative(0, 0, speed, true);
            }
            if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_E))
            {
                camera.MoveRelative(0, speed / 2, 0, true);
            }
            if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_Q))
            {
                camera.MoveRelative(0, -(speed / 2), 0, true);
            }
            if (camfol == true)
            {
                camera.SetPosition(physics.GetBodyPosition(pbi_chassis).x, physics.GetBodyPosition(pbi_chassis).y + 10, physics.GetBodyPosition(pbi_chassis).z - 30);
                camera.SetRotation(10, 0, 0);
            }
            if (camtop == true)
            {
                camera.SetPosition(physics.GetBodyPosition(pbi_chassis).x, physics.GetBodyPosition(pbi_chassis).y + 60, physics.GetBodyPosition(pbi_chassis).z);
            }

            if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_H))
            {
                //
            }

            //Accelerate and Brake
            CarPower = 800; //3000

            if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_I)) //Accellerate
            {
                physics.SetVehicleWheelTorque(car_ID, rlw, CarPower, -1000);
                physics.SetVehicleWheelTorque(car_ID, rrw, CarPower, -1000);
            }
            else
            {
                if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_K))
                {
                    physics.SetVehicleWheelTorque(car_ID, rlw, -CarPower, -1000);
                    physics.SetVehicleWheelTorque(car_ID, rrw, -CarPower, -1000);
                }
                else
                {
                    if (autrst == true)
                    {
                        physics.SetVehicleWheelTorque(car_ID, flw, 0, -10000);
                        physics.SetVehicleWheelTorque(car_ID, frw, 0, -10000);
                        physics.SetVehicleWheelTorque(car_ID, rlw, 0, -10000);
                        physics.SetVehicleWheelTorque(car_ID, rrw, 0, -10000);
                    }
                }
            }

            //Steering
            if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_J))
            {
                steerAngle -= 5;
                if (steerAngle < -45)
                    steerAngle = -45;

                physics.SetVehicleWheelSteering(car_ID, flw, steerAngle);
                physics.SetVehicleWheelSteering(car_ID, frw, steerAngle);
            }
            else
            {
                if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_L))
                {
                    steerAngle += 5;
                    if (steerAngle > 45)
                        steerAngle = 45;

                    physics.SetVehicleWheelSteering(car_ID, flw, steerAngle);
                    physics.SetVehicleWheelSteering(car_ID, frw, steerAngle);
                }
                else
                {
                    if (autrst == true)
                    {
                        steerAngle = 0;

                        physics.SetVehicleWheelSteering(car_ID, flw, steerAngle);
                        physics.SetVehicleWheelSteering(car_ID, frw, steerAngle);
                    }
                }
            }

            //Car Handbrake
            if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_RIGHTCONTROL))
            {
                physics.VehicleWheelHandBrake(car_ID, rlw, 1, 2000);
                physics.VehicleWheelHandBrake(car_ID, rrw, 1, 2000);
            }

            if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_P))
            {
                physics.VehicleReset(car_ID);
                physics.SetBodyRotation(pbi_chassis, 0, 0, 0);
                physics.SetBodyPosition(pbi_chassis, physics.GetBodyPosition(pbi_chassis).x, physics.GetBodyPosition(pbi_chassis).y + 0.2f, physics.GetBodyPosition(pbi_chassis).z);
            }

            if (input.IsKeyPressed(CONST_TV_KEY.TV_KEY_O))
            {
                physics.VehicleReset(car_ID);
                physics.SetBodyPosition(pbi_chassis, 0f, 5, 0f);
                physics.SetBodyRotation(pbi_chassis, 0f, 0f, 0f);
            }

            //Mouse
            int tmpMouseX = 0;
            int tmpMouseY = 0;
            //int tmpMouseScrollNew = 0;
            //bool tmpMouseB1 = false;
            //bool tmpMouseB2 = false;
            //bool tmpMouseB3 = false;
            //bool tmpMouseB4 = false;

            //Mouse Rotation
            //input.GetMouseState(ref tmpMouseX, ref tmpMouseY, ref tmpMouseB1, ref tmpMouseB2, ref tmpMouseB3, ref tmpMouseB4, ref tmpMouseScrollNew);
            camera.RotateY(tmpMouseX * (mousespeed * 2));
            camera.SetLookAt(camera.GetLookAt().x, camera.GetLookAt().y - (tmpMouseY * ((mousespeed * 2) / 100)), camera.GetLookAt().z);

        }

        private void button46_Click(object sender, EventArgs e)
        {
            camera.RotateX(-10);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            camera.RotateX(5);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            camera.RotateY(-10);
        }

        private void button43_Click(object sender, EventArgs e)
        {
            camera.RotateY(10);
        }

        private void button42_Click(object sender, EventArgs e)
        {
            camera.SetPosition(0, 15, -50);
        }

        private void On_Click(object sender, EventArgs e)
        {
            camfol = true;
            camtop = false;
        }

        private void button40_Click(object sender, EventArgs e)
        {
            camfol = false;
            camtop = false;
        }

        private void button41_Click(object sender, EventArgs e)
        {
            camfol = false;
            camtop = true;
            camera.SetRotation(89, 0, 0);
            camera.SetPosition(0, 100, 0);
        }

        private void button39_Click(object sender, EventArgs e)
        {
            physics.VehicleReset(car_ID);
            physics.SetBodyRotation(pbi_chassis, 0, 0, 0);
            physics.SetBodyPosition(pbi_chassis, physics.GetBodyPosition(pbi_chassis).x, physics.GetBodyPosition(pbi_chassis).y + 0.2f, physics.GetBodyPosition(pbi_chassis).z);
        }

        private void button38_Click(object sender, EventArgs e)
        {
            physics.VehicleReset(car_ID);
            physics.SetBodyPosition(pbi_chassis, 0f, 5, 0f);
            physics.SetBodyRotation(pbi_chassis, 0f, 0f, 0f);
        }

        private void button35_Click(object sender, EventArgs e)
        {
            steerAngle -= 5;
            if (steerAngle < -45)
            {
                steerAngle = -45;
            }
            physics.SetVehicleWheelSteering(car_ID, flw, steerAngle);
            physics.SetVehicleWheelSteering(car_ID, frw, steerAngle);
        }

        private void button34_Click(object sender, EventArgs e)
        {
            steerAngle += 5;
            if (steerAngle > 45)
            {
                steerAngle = 45;
            }
            physics.SetVehicleWheelSteering(car_ID, flw, steerAngle);
            physics.SetVehicleWheelSteering(car_ID, frw, steerAngle);
        }

        private void button37_Click(object sender, EventArgs e)
        {
            physics.SetVehicleWheelTorque(car_ID, rlw, CarPower, -1000);
            physics.SetVehicleWheelTorque(car_ID, rrw, CarPower, -1000);
        }

        private void button36_Click(object sender, EventArgs e)
        {
            physics.SetVehicleWheelTorque(car_ID, rlw, -CarPower, -1000);
            physics.SetVehicleWheelTorque(car_ID, rrw, -CarPower, -1000);
        }

        private void button33_Click(object sender, EventArgs e)
        {
            autrst = true;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            autrst = false;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (float.Parse(label5.Text) > 10 && float.Parse(label7.Text) > 10)
            {
                timer3D.Enabled = false;
                MessageBox.Show("Game Finish");
            }
        }

        private void timer3D_Tick(object sender, EventArgs e)
        {
            if (float.Parse(label81.Text) > 10 && float.Parse(label80.Text) > 10)
            {
                timer1.Enabled = false;
                MessageBox.Show("Game Finish");
            }
        }
        /////3D

   }
}