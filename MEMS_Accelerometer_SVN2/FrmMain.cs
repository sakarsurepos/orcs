/////////////////////////////////////////////////////////////
//
//  Filename: FrmMain.cs
//  Author:   Travis Feirtag
//  Date:     05/12/2006 11:30:36
//  CLR ver:  2.0.50727.42
//  Project:  Accelerometer01
//  Modified: Kamil Zidek 10bit ADC MMA7260QT
//
//  Copyright © 2006 Feirtech Inc.  All rights reserved.
// 
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
////////////////////////////////////////////////////////////

namespace Accelerometer01
{
    #region Using Statements
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.IO.Ports;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using ZedGraph;
    #endregion

    /// <summary>
    /// Summary of FrmMain class
    /// </summary>
    public sealed class FrmMain : Form
    {
        #region Constants

        #endregion

        #region Fields
        public static Int32 ADCset = 4; //10 bit ADC
        private SerialPort mySerialPort = null;
        private Byte[] buffer = new Byte[5000];
        private Char[] sendChars = new Char[] { 'G' };
        private Int32 nMinX = 320; //10000; max-min = cca 500
        private Int32 nMaxX = 820;//-10000;
        private Int32 nMinY = 340;//10000; max-min = cca 500
        private Int32 nMaxY = 840;//-10000;
        private Int32 nMinZ = 260;//10000; max-min = cca 500
        private Int32 nMaxZ = 760;//-10000;
        private Queue byteQueue = new Queue();

        private bool b3dMode = false;
        private UsrCtrlAxis2D usrCtrlAxis2D = null;
        private UsrCtrlAxis3D usrCtrlAxis3D = null;

        private bool bUseSmoothing = false;
        private int nSmoothingDelta = 5;
        private int nPrevX = 0;
        private int nPrevY = 0;
        private int nPrevZ = 0;

        Byte byte01;// $
        Byte byte02;// X
        Byte byte03;// X1
        Byte byte04;// Z //CHANGE
        Byte byte05;// Z1
        Byte byte06;// Y
        Byte byte07;// Y1
        Byte byte08;// 13
        Byte byte09;// 10

        Int32 nXaxis = 512;
        Int32 nYaxis = 512;
        Int32 nZaxis = 512;
        Int32 selAxis = 512;
      
        //NEW
        public static float Xtran = 0;
        public static float Ytran = 0;
        public static float Ztran = 0;
        public static bool lockX = false;
        public static bool lockY = false;
        public static bool lockZ = false;
        public static float Gsel = 1.5f;
        public static float GX = 0;
        public static float GY = 0;
        public static float GZ = 0;
        public static int incdel = 1024; //how many increment
        //NEW

        public static float fAnglex0 = 90;
        public static float fAngley0 = 90;
        public static float fAnglez0 = 90;

        int num = 0;

        public static int radio = 1;

        Thread trd;

        private TextBox txtOutput;
        private TextBox txtByte01;
        private TextBox txtByte02;
        private TextBox txtByte03;
        private TextBox txtByte04;
        private TextBox txtBytesRead;
        private TextBox txtRefreshRate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private TextBox txtXaxis;
        private TextBox txtMinX;
        private TextBox txtMaxX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private Button btnMode;
        private CheckBox chkBoxSmoothing;
        private TextBox txtZaxis;
        private TextBox txtMinZ;
        private TextBox txtMaxZ;
        private Button button1;
        private Button button2;
        private System.Windows.Forms.Label label13;
        private TextBox txtAngleX;
        private TextBox txtAngleZ;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Panel panel1;
        private RadioButton radioButtonXmems;
        private System.Windows.Forms.Label label14;
        private GroupBox groupBox4;
        private RadioButton radioButtonYmems;
        private TextBox txtAngleY;
        private TextBox txtMaxY;
        private TextBox txtMinY;
        private TextBox txtYaxis;
        private CheckBox checkBoxZtran;
        private TextBox textBoxZg;
        private System.Windows.Forms.Label label8;
        private CheckBox checkBoxXtran;
        private CheckBox checkBoxYtran;
        private CheckBox checkBoxAll;
        private CheckBox checkBoxAngleLock;
        private CheckBox checkBoxTransXY;
        private TextBox textBoxYg;
        private TextBox textBoxXg;
        private GroupBox groupBox5;
        private Button buttonGTest;
        private Button button6G;
        private Button button4G;
        private Button button1G;
        private Button button2G;
        private System.Windows.Forms.Label labelSel;
        private System.Windows.Forms.Label labelSelG;
        private TextBox txtByte05;
        private TextBox txtByte06;
        private TextBox txtByte07;
        private ZedGraphControl zg1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        #endregion

        #region Constructor
        /// <summary>
        /// 
        /// </summary>
        public FrmMain()
        {
            this.usrCtrlAxis2D = new UsrCtrlAxis2D();
            this.usrCtrlAxis2D.Size = new Size(350, 350);
            this.usrCtrlAxis2D.Location = new Point(0, 0);
            this.usrCtrlAxis2D.BackColor = Color.AliceBlue;
            this.Controls.Add(this.usrCtrlAxis2D);

            this.usrCtrlAxis3D = new UsrCtrlAxis3D();
            this.usrCtrlAxis3D.Size = new Size(350, 350);
            this.usrCtrlAxis3D.Location = new Point(0, 0);
            this.Controls.Add(this.usrCtrlAxis3D);

            InitializeComponent();

            this.mySerialPort = new SerialPort("COM5", 9600, Parity.None, 8, StopBits.One);
            this.mySerialPort.Handshake = Handshake.None;

            this.mySerialPort.RtsEnable = true;
            this.mySerialPort.DataReceived += new SerialDataReceivedEventHandler(MySerialPort_DataReceived);
        }
        #endregion

        #region Dispose Method
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtByte01 = new System.Windows.Forms.TextBox();
            this.txtByte02 = new System.Windows.Forms.TextBox();
            this.txtByte03 = new System.Windows.Forms.TextBox();
            this.txtByte04 = new System.Windows.Forms.TextBox();
            this.txtByte05 = new System.Windows.Forms.TextBox();
            this.txtByte06 = new System.Windows.Forms.TextBox();
            this.txtByte07 = new System.Windows.Forms.TextBox();
            this.txtBytesRead = new System.Windows.Forms.TextBox();
            this.txtRefreshRate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtXaxis = new System.Windows.Forms.TextBox();
            this.txtMinX = new System.Windows.Forms.TextBox();
            this.txtMaxX = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnMode = new System.Windows.Forms.Button();
            this.chkBoxSmoothing = new System.Windows.Forms.CheckBox();
            this.txtZaxis = new System.Windows.Forms.TextBox();
            this.txtMinZ = new System.Windows.Forms.TextBox();
            this.txtMaxZ = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtAngleX = new System.Windows.Forms.TextBox();
            this.txtAngleZ = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxYg = new System.Windows.Forms.TextBox();
            this.textBoxXg = new System.Windows.Forms.TextBox();
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.checkBoxYtran = new System.Windows.Forms.CheckBox();
            this.checkBoxXtran = new System.Windows.Forms.CheckBox();
            this.radioButtonXmems = new System.Windows.Forms.RadioButton();
            this.radioButtonYmems = new System.Windows.Forms.RadioButton();
            this.txtAngleY = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtMaxY = new System.Windows.Forms.TextBox();
            this.txtMinY = new System.Windows.Forms.TextBox();
            this.txtYaxis = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxZg = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxZtran = new System.Windows.Forms.CheckBox();
            this.checkBoxAngleLock = new System.Windows.Forms.CheckBox();
            this.checkBoxTransXY = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button1G = new System.Windows.Forms.Button();
            this.button2G = new System.Windows.Forms.Button();
            this.buttonGTest = new System.Windows.Forms.Button();
            this.button6G = new System.Windows.Forms.Button();
            this.button4G = new System.Windows.Forms.Button();
            this.labelSel = new System.Windows.Forms.Label();
            this.labelSelG = new System.Windows.Forms.Label();
            this.zg1 = new ZedGraph.ZedGraphControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.AcceptsReturn = true;
            this.txtOutput.AcceptsTab = true;
            this.txtOutput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtOutput.Location = new System.Drawing.Point(366, 322);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(306, 40);
            this.txtOutput.TabIndex = 0;
            // 
            // txtByte01
            // 
            this.txtByte01.Location = new System.Drawing.Point(41, 18);
            this.txtByte01.Name = "txtByte01";
            this.txtByte01.Size = new System.Drawing.Size(61, 20);
            this.txtByte01.TabIndex = 1;
            // 
            // txtByte02
            // 
            this.txtByte02.Location = new System.Drawing.Point(41, 39);
            this.txtByte02.Name = "txtByte02";
            this.txtByte02.Size = new System.Drawing.Size(30, 20);
            this.txtByte02.TabIndex = 2;
            // 
            // txtByte03
            // 
            this.txtByte03.Location = new System.Drawing.Point(72, 39);
            this.txtByte03.Name = "txtByte03";
            this.txtByte03.Size = new System.Drawing.Size(30, 20);
            this.txtByte03.TabIndex = 3;
            // 
            // txtByte04
            // 
            this.txtByte04.Location = new System.Drawing.Point(41, 61);
            this.txtByte04.Name = "txtByte04";
            this.txtByte04.Size = new System.Drawing.Size(30, 20);
            this.txtByte04.TabIndex = 4;
            // 
            // txtByte05
            // 
            this.txtByte05.Location = new System.Drawing.Point(72, 61);
            this.txtByte05.Name = "txtByte05";
            this.txtByte05.Size = new System.Drawing.Size(30, 20);
            this.txtByte05.TabIndex = 13;
            // 
            // txtByte06
            // 
            this.txtByte06.Location = new System.Drawing.Point(41, 84);
            this.txtByte06.Name = "txtByte06";
            this.txtByte06.Size = new System.Drawing.Size(30, 20);
            this.txtByte06.TabIndex = 12;
            // 
            // txtByte07
            // 
            this.txtByte07.Location = new System.Drawing.Point(72, 84);
            this.txtByte07.Name = "txtByte07";
            this.txtByte07.Size = new System.Drawing.Size(30, 20);
            this.txtByte07.TabIndex = 11;
            // 
            // txtBytesRead
            // 
            this.txtBytesRead.Location = new System.Drawing.Point(88, 15);
            this.txtBytesRead.Name = "txtBytesRead";
            this.txtBytesRead.Size = new System.Drawing.Size(57, 20);
            this.txtBytesRead.TabIndex = 5;
            // 
            // txtRefreshRate
            // 
            this.txtRefreshRate.Location = new System.Drawing.Point(88, 37);
            this.txtRefreshRate.Name = "txtRefreshRate";
            this.txtRefreshRate.Size = new System.Drawing.Size(57, 20);
            this.txtRefreshRate.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "$";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Z";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "X";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Y";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Bytes Read";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Refresh Rate";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtXaxis
            // 
            this.txtXaxis.Location = new System.Drawing.Point(55, 33);
            this.txtXaxis.Name = "txtXaxis";
            this.txtXaxis.Size = new System.Drawing.Size(69, 20);
            this.txtXaxis.TabIndex = 13;
            // 
            // txtMinX
            // 
            this.txtMinX.Location = new System.Drawing.Point(55, 54);
            this.txtMinX.Name = "txtMinX";
            this.txtMinX.Size = new System.Drawing.Size(69, 20);
            this.txtMinX.TabIndex = 17;
            // 
            // txtMaxX
            // 
            this.txtMaxX.Location = new System.Drawing.Point(55, 75);
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.Size = new System.Drawing.Size(69, 20);
            this.txtMaxX.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 36);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Current";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Min";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Max";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnMode
            // 
            this.btnMode.Location = new System.Drawing.Point(371, 65);
            this.btnMode.Name = "btnMode";
            this.btnMode.Size = new System.Drawing.Size(99, 25);
            this.btnMode.TabIndex = 24;
            this.btnMode.Text = "Switch to 3D";
            this.btnMode.UseVisualStyleBackColor = true;
            this.btnMode.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // chkBoxSmoothing
            // 
            this.chkBoxSmoothing.AutoSize = true;
            this.chkBoxSmoothing.Location = new System.Drawing.Point(374, 115);
            this.chkBoxSmoothing.Name = "chkBoxSmoothing";
            this.chkBoxSmoothing.Size = new System.Drawing.Size(85, 17);
            this.chkBoxSmoothing.TabIndex = 25;
            this.chkBoxSmoothing.Text = "Fail suspend";
            this.chkBoxSmoothing.UseVisualStyleBackColor = true;
            this.chkBoxSmoothing.CheckedChanged += new System.EventHandler(this.chkBoxSmoothing_CheckedChanged);
            // 
            // txtZaxis
            // 
            this.txtZaxis.Location = new System.Drawing.Point(18, 34);
            this.txtZaxis.Name = "txtZaxis";
            this.txtZaxis.Size = new System.Drawing.Size(69, 20);
            this.txtZaxis.TabIndex = 26;
            // 
            // txtMinZ
            // 
            this.txtMinZ.Location = new System.Drawing.Point(18, 55);
            this.txtMinZ.Name = "txtMinZ";
            this.txtMinZ.Size = new System.Drawing.Size(69, 20);
            this.txtMinZ.TabIndex = 27;
            // 
            // txtMaxZ
            // 
            this.txtMaxZ.Location = new System.Drawing.Point(18, 76);
            this.txtMaxZ.Name = "txtMaxZ";
            this.txtMaxZ.Size = new System.Drawing.Size(69, 20);
            this.txtMaxZ.TabIndex = 28;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(371, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 25);
            this.button1.TabIndex = 30;
            this.button1.Text = "Open Serial Port";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(371, 35);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 25);
            this.button2.TabIndex = 31;
            this.button2.Text = "Close Serial Port";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 92);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(39, 26);
            this.label13.TabIndex = 32;
            this.label13.Text = "Angle/\r\nSpeed";
            // 
            // txtAngleX
            // 
            this.txtAngleX.Location = new System.Drawing.Point(55, 96);
            this.txtAngleX.Name = "txtAngleX";
            this.txtAngleX.Size = new System.Drawing.Size(30, 20);
            this.txtAngleX.TabIndex = 33;
            // 
            // txtAngleZ
            // 
            this.txtAngleZ.Location = new System.Drawing.Point(41, 96);
            this.txtAngleZ.Name = "txtAngleZ";
            this.txtAngleZ.Size = new System.Drawing.Size(23, 20);
            this.txtAngleZ.TabIndex = 35;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxYg);
            this.groupBox1.Controls.Add(this.textBoxXg);
            this.groupBox1.Controls.Add(this.checkBoxAll);
            this.groupBox1.Controls.Add(this.checkBoxYtran);
            this.groupBox1.Controls.Add(this.checkBoxXtran);
            this.groupBox1.Controls.Add(this.radioButtonXmems);
            this.groupBox1.Controls.Add(this.radioButtonYmems);
            this.groupBox1.Controls.Add(this.txtAngleY);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.txtMaxY);
            this.groupBox1.Controls.Add(this.txtAngleX);
            this.groupBox1.Controls.Add(this.txtMinY);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtYaxis);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtMaxX);
            this.groupBox1.Controls.Add(this.txtMinX);
            this.groupBox1.Controls.Add(this.txtXaxis);
            this.groupBox1.Location = new System.Drawing.Point(366, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 141);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MEMS Angle/Speed Values";
            // 
            // textBoxYg
            // 
            this.textBoxYg.Location = new System.Drawing.Point(154, 96);
            this.textBoxYg.Name = "textBoxYg";
            this.textBoxYg.Size = new System.Drawing.Size(39, 20);
            this.textBoxYg.TabIndex = 48;
            // 
            // textBoxXg
            // 
            this.textBoxXg.Location = new System.Drawing.Point(86, 96);
            this.textBoxXg.Name = "textBoxXg";
            this.textBoxXg.Size = new System.Drawing.Size(38, 20);
            this.textBoxXg.TabIndex = 47;
            // 
            // checkBoxAll
            // 
            this.checkBoxAll.AutoSize = true;
            this.checkBoxAll.Location = new System.Drawing.Point(20, 118);
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.Size = new System.Drawing.Size(37, 17);
            this.checkBoxAll.TabIndex = 46;
            this.checkBoxAll.Text = "All";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            this.checkBoxAll.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // checkBoxYtran
            // 
            this.checkBoxYtran.AutoSize = true;
            this.checkBoxYtran.Location = new System.Drawing.Point(131, 118);
            this.checkBoxYtran.Name = "checkBoxYtran";
            this.checkBoxYtran.Size = new System.Drawing.Size(55, 17);
            this.checkBoxYtran.TabIndex = 45;
            this.checkBoxYtran.Text = "Y Axis";
            this.checkBoxYtran.UseVisualStyleBackColor = true;
            // 
            // checkBoxXtran
            // 
            this.checkBoxXtran.AutoSize = true;
            this.checkBoxXtran.Location = new System.Drawing.Point(61, 118);
            this.checkBoxXtran.Name = "checkBoxXtran";
            this.checkBoxXtran.Size = new System.Drawing.Size(55, 17);
            this.checkBoxXtran.TabIndex = 44;
            this.checkBoxXtran.Text = "X Axis";
            this.checkBoxXtran.UseVisualStyleBackColor = true;
            // 
            // radioButtonXmems
            // 
            this.radioButtonXmems.AutoSize = true;
            this.radioButtonXmems.Checked = true;
            this.radioButtonXmems.Location = new System.Drawing.Point(55, 14);
            this.radioButtonXmems.Name = "radioButtonXmems";
            this.radioButtonXmems.Size = new System.Drawing.Size(54, 17);
            this.radioButtonXmems.TabIndex = 37;
            this.radioButtonXmems.TabStop = true;
            this.radioButtonXmems.Text = "X Axis";
            this.radioButtonXmems.UseVisualStyleBackColor = true;
            // 
            // radioButtonYmems
            // 
            this.radioButtonYmems.AutoSize = true;
            this.radioButtonYmems.Location = new System.Drawing.Point(127, 14);
            this.radioButtonYmems.Name = "radioButtonYmems";
            this.radioButtonYmems.Size = new System.Drawing.Size(54, 17);
            this.radioButtonYmems.TabIndex = 43;
            this.radioButtonYmems.TabStop = true;
            this.radioButtonYmems.Text = "Y Axis";
            this.radioButtonYmems.UseVisualStyleBackColor = true;
            // 
            // txtAngleY
            // 
            this.txtAngleY.Location = new System.Drawing.Point(124, 96);
            this.txtAngleY.Name = "txtAngleY";
            this.txtAngleY.Size = new System.Drawing.Size(29, 20);
            this.txtAngleY.TabIndex = 42;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 16);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(34, 13);
            this.label14.TabIndex = 36;
            this.label14.Text = "Show";
            // 
            // txtMaxY
            // 
            this.txtMaxY.Location = new System.Drawing.Point(124, 75);
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.Size = new System.Drawing.Size(69, 20);
            this.txtMaxY.TabIndex = 41;
            // 
            // txtMinY
            // 
            this.txtMinY.Location = new System.Drawing.Point(124, 54);
            this.txtMinY.Name = "txtMinY";
            this.txtMinY.Size = new System.Drawing.Size(69, 20);
            this.txtMinY.TabIndex = 40;
            // 
            // txtYaxis
            // 
            this.txtYaxis.Location = new System.Drawing.Point(124, 33);
            this.txtYaxis.Name = "txtYaxis";
            this.txtYaxis.Size = new System.Drawing.Size(69, 20);
            this.txtYaxis.TabIndex = 39;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtRefreshRate);
            this.groupBox2.Controls.Add(this.txtBytesRead);
            this.groupBox2.Location = new System.Drawing.Point(512, 112);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 63);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Info";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtByte05);
            this.groupBox3.Controls.Add(this.txtByte06);
            this.groupBox3.Controls.Add(this.txtByte07);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtByte03);
            this.groupBox3.Controls.Add(this.txtByte02);
            this.groupBox3.Controls.Add(this.txtByte04);
            this.groupBox3.Controls.Add(this.txtByte01);
            this.groupBox3.Location = new System.Drawing.Point(554, -1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(118, 113);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Raw Data";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(7, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(351, 355);
            this.panel1.TabIndex = 39;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxZg);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.checkBoxZtran);
            this.groupBox4.Controls.Add(this.txtZaxis);
            this.groupBox4.Controls.Add(this.txtMinZ);
            this.groupBox4.Controls.Add(this.txtMaxZ);
            this.groupBox4.Controls.Add(this.txtAngleZ);
            this.groupBox4.Location = new System.Drawing.Point(566, 175);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(106, 141);
            this.groupBox4.TabIndex = 40;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "MEMS Free Fall";
            // 
            // textBoxZg
            // 
            this.textBoxZg.Location = new System.Drawing.Point(64, 96);
            this.textBoxZg.Name = "textBoxZg";
            this.textBoxZg.Size = new System.Drawing.Size(23, 20);
            this.textBoxZg.TabIndex = 47;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 99);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 46;
            this.label8.Text = "Speed";
            // 
            // checkBoxZtran
            // 
            this.checkBoxZtran.AutoSize = true;
            this.checkBoxZtran.Location = new System.Drawing.Point(24, 118);
            this.checkBoxZtran.Name = "checkBoxZtran";
            this.checkBoxZtran.Size = new System.Drawing.Size(58, 17);
            this.checkBoxZtran.TabIndex = 45;
            this.checkBoxZtran.Text = " Z Axis";
            this.checkBoxZtran.UseVisualStyleBackColor = true;
            // 
            // checkBoxAngleLock
            // 
            this.checkBoxAngleLock.AutoSize = true;
            this.checkBoxAngleLock.Location = new System.Drawing.Point(374, 132);
            this.checkBoxAngleLock.Name = "checkBoxAngleLock";
            this.checkBoxAngleLock.Size = new System.Drawing.Size(80, 17);
            this.checkBoxAngleLock.TabIndex = 48;
            this.checkBoxAngleLock.Text = "Angle Lock";
            this.checkBoxAngleLock.UseVisualStyleBackColor = true;
            // 
            // checkBoxTransXY
            // 
            this.checkBoxTransXY.AutoSize = true;
            this.checkBoxTransXY.Location = new System.Drawing.Point(374, 149);
            this.checkBoxTransXY.Name = "checkBoxTransXY";
            this.checkBoxTransXY.Size = new System.Drawing.Size(98, 17);
            this.checkBoxTransXY.TabIndex = 49;
            this.checkBoxTransXY.Text = "Translation X,Y";
            this.checkBoxTransXY.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button1G);
            this.groupBox5.Controls.Add(this.button2G);
            this.groupBox5.Controls.Add(this.buttonGTest);
            this.groupBox5.Controls.Add(this.button6G);
            this.groupBox5.Controls.Add(this.button4G);
            this.groupBox5.Location = new System.Drawing.Point(476, -1);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(75, 113);
            this.groupBox5.TabIndex = 50;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Setup";
            // 
            // button1G
            // 
            this.button1G.Location = new System.Drawing.Point(8, 14);
            this.button1G.Name = "button1G";
            this.button1G.Size = new System.Drawing.Size(59, 20);
            this.button1G.TabIndex = 4;
            this.button1G.Text = "1,5G";
            this.button1G.UseVisualStyleBackColor = true;
            this.button1G.Click += new System.EventHandler(this.button1G_Click);
            // 
            // button2G
            // 
            this.button2G.Location = new System.Drawing.Point(8, 33);
            this.button2G.Name = "button2G";
            this.button2G.Size = new System.Drawing.Size(59, 20);
            this.button2G.TabIndex = 3;
            this.button2G.Text = "2G";
            this.button2G.UseVisualStyleBackColor = true;
            this.button2G.Click += new System.EventHandler(this.button2G_Click);
            // 
            // buttonGTest
            // 
            this.buttonGTest.Location = new System.Drawing.Point(8, 90);
            this.buttonGTest.Name = "buttonGTest";
            this.buttonGTest.Size = new System.Drawing.Size(59, 20);
            this.buttonGTest.TabIndex = 2;
            this.buttonGTest.Text = "Test";
            this.buttonGTest.UseVisualStyleBackColor = true;
            this.buttonGTest.Click += new System.EventHandler(this.buttonGTest_Click);
            // 
            // button6G
            // 
            this.button6G.Location = new System.Drawing.Point(8, 71);
            this.button6G.Name = "button6G";
            this.button6G.Size = new System.Drawing.Size(59, 20);
            this.button6G.TabIndex = 1;
            this.button6G.Text = "6G";
            this.button6G.UseVisualStyleBackColor = true;
            this.button6G.Click += new System.EventHandler(this.button6G_Click);
            // 
            // button4G
            // 
            this.button4G.Location = new System.Drawing.Point(8, 52);
            this.button4G.Name = "button4G";
            this.button4G.Size = new System.Drawing.Size(59, 20);
            this.button4G.TabIndex = 0;
            this.button4G.Text = "4G";
            this.button4G.UseVisualStyleBackColor = true;
            this.button4G.Click += new System.EventHandler(this.button4G_Click);
            // 
            // labelSel
            // 
            this.labelSel.AutoSize = true;
            this.labelSel.Location = new System.Drawing.Point(379, 95);
            this.labelSel.Name = "labelSel";
            this.labelSel.Size = new System.Drawing.Size(53, 13);
            this.labelSel.TabIndex = 51;
            this.labelSel.Text = "SeleledG:";
            // 
            // labelSelG
            // 
            this.labelSelG.AutoSize = true;
            this.labelSelG.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSelG.Location = new System.Drawing.Point(430, 95);
            this.labelSelG.Name = "labelSelG";
            this.labelSelG.Size = new System.Drawing.Size(34, 13);
            this.labelSelG.TabIndex = 52;
            this.labelSelG.Text = "1,5G";
            // 
            // zg1
            // 
            this.zg1.Location = new System.Drawing.Point(7, 371);
            this.zg1.Name = "zg1";
            this.zg1.ScrollGrace = 0;
            this.zg1.ScrollMaxX = 0;
            this.zg1.ScrollMaxY = 0;
            this.zg1.ScrollMaxY2 = 0;
            this.zg1.ScrollMinX = 0;
            this.zg1.ScrollMinY = 0;
            this.zg1.ScrollMinY2 = 0;
            this.zg1.Size = new System.Drawing.Size(665, 223);
            this.zg1.TabIndex = 53;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 602);
            this.Controls.Add(this.zg1);
            this.Controls.Add(this.labelSelG);
            this.Controls.Add(this.labelSel);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.checkBoxTransXY);
            this.Controls.Add(this.checkBoxAngleLock);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkBoxSmoothing);
            this.Controls.Add(this.btnMode);
            this.Controls.Add(this.txtOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Robot Tilt 3 Axis Accelerometer MMA7260QT SVN2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region OnLoad Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        protected override void OnLoad(EventArgs eventArgs)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        #endregion

        private void drawvalue()
        {
            while (true)
            {
                Thread.Sleep(200);

                this.txtByte01.Text = byte01.ToString(); //$ String.Format("{0:x2}", byte01);
                this.txtByte02.Text = byte02.ToString(); //x
                this.txtByte03.Text = byte03.ToString(); //x1
                this.txtByte04.Text = byte04.ToString(); //z  //CHANGE
                this.txtByte05.Text = byte05.ToString(); //z1
                this.txtByte06.Text = byte06.ToString(); //y
                this.txtByte07.Text = byte07.ToString(); //y1

                this.txtXaxis.Text = String.Format("{0}", nXaxis);
                this.txtYaxis.Text = String.Format("{0}", nYaxis);
                this.txtZaxis.Text = String.Format("{0}", nZaxis); //NEW fail
                textBoxXg.Text = GX.ToString();
                textBoxYg.Text = GY.ToString();
                textBoxZg.Text = GZ.ToString();

                this.txtMinX.Text = this.nMinX.ToString();
                this.txtMaxX.Text = this.nMaxX.ToString();

                this.txtMinY.Text = this.nMinY.ToString();
                this.txtMaxY.Text = this.nMaxY.ToString();

                this.txtMinZ.Text = this.nMinZ.ToString();
                this.txtMaxZ.Text = this.nMaxZ.ToString();

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

                if (fAngley0 < 50 || fAngley0 > 130) //WARNING Y
                {
                    num = num + 1;
                    txtAngleY.BackColor = Color.Red;
                    txtOutput.AppendText(num.ToString() + ". Z axis warning dangerous fall");
                    txtOutput.AppendText("\n");
                }
                else
                {
                    txtAngleY.BackColor = Color.White;
                }

                if (fAnglez0 < 50 || fAnglez0 > 130) //WARNING Z
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

                if (radioButtonXmems.Checked == true)
                {
                    radio = 1;
                    selAxis = nXaxis;
                }
                if (radioButtonYmems.Checked == true)
                {
                    radio = 2;
                    selAxis = nYaxis;
                }

                if (checkBoxXtran.Checked == true)
                {
                    lockX = true;
                }
                else { lockX = false; }

                if (checkBoxYtran.Checked == true)
                {
                    lockY = true;
                }
                else { lockY = false; }

                if (checkBoxAngleLock.Checked == true)
                {
                    lockZ = true;
                }
                else { lockZ = false; }
                //if (radioButton3.Checked == true)
                //{
                //    radio = 3;
                //    selAxis = nZaxis;
                //}
            }
            
        }

        #region OnClosing Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancelEventArgs"></param>
        protected override void OnClosing(CancelEventArgs cancelEventArgs)
        {
            this.mySerialPort.Close();

            base.OnClosing(cancelEventArgs);
        }
        #endregion

        #region MySerialPort_DataReceived Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        #endregion

        #region ProcessData Method
        /// <summary>
        /// 
        /// </summary>
        private void ProcessData()
        {
            if (this.byteQueue.Count < 9)
                return;

            byte01 = (Byte)this.byteQueue.Dequeue(); //$
            if (byte01 != 36)
                return;
            byte02 = (Byte)this.byteQueue.Dequeue(); //X
            byte03 = (Byte)this.byteQueue.Dequeue(); //X1
            byte04 = (Byte)this.byteQueue.Dequeue(); //Z //CHANGE
            byte05 = (Byte)this.byteQueue.Dequeue(); //Z1
            byte06 = (Byte)this.byteQueue.Dequeue(); //Y
            byte07 = (Byte)this.byteQueue.Dequeue(); //Y1
            byte08 = (Byte)this.byteQueue.Dequeue(); //13
            byte09 = (Byte)this.byteQueue.Dequeue(); //10
            
            //refresh  11

            //this.txtByte05.Text = String.Format("{0:x2}", byte05);
            //this.txtByte06.Text = String.Format("{0:x2}", byte06);

            // PWM % based on documentation formula
            //float fXaxis = (256 * this.buffer[0] + this.buffer[1]) / 100;
            //float fYaxis = (256 * this.buffer[2] + this.buffer[3]) / 100;
            //this.txtXaxis.Text = String.Format("{0:f4}", fXaxis);
            //this.txtYaxis.Text = String.Format("{0:f4}", fYaxis);
            
            nXaxis = Convert.ToInt32(byte02) << 8 | Convert.ToInt32(byte03);  //Convert.ToInt32(byte02);
            nYaxis = Convert.ToInt32(byte04) << 8 | Convert.ToInt32(byte05);  //Convert.ToInt32(byte04); //CHANGE SUPER
            nZaxis = Convert.ToInt32(byte06) << 8 | Convert.ToInt32(byte07);  //Convert.ToInt32(byte03);

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
                    this.usrCtrlAxis3D.SetAxesValues(nXaxis, nYaxis, nZaxis);  //dorobit z
                else
                    this.usrCtrlAxis2D.SetCurrentValue(selAxis);

                this.nPrevX = nXaxis;
                this.nPrevY = nYaxis;
                this.nPrevZ = nZaxis;

                return;
            }

            // Default
            if (this.b3dMode == true)
                this.usrCtrlAxis3D.SetAxesValues(nXaxis, nYaxis, nZaxis);
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
                if (checkBoxXtran.Checked == true ) //&& radioButtonXmems.Checked == true
                {
                    Xtran = nXaxis - 585;
                    GX = ((nXaxis * ((Gsel * Gsel) / incdel) - Gsel+0.24f)*1.942f)*100;
                }
                if (checkBoxYtran.Checked == true ) //&& radioButtonYmems.Checked == true
                {
                    Ytran = nYaxis - 595;
                    GY = ((nYaxis * ((Gsel * Gsel) / incdel) - Gsel + 0.215f)*1.938f)*100;
                }
                if (checkBoxZtran.Checked == true)
                {
                    Ztran = nZaxis - 145 * ADCset;
                    GZ = (nZaxis * ((Gsel * Gsel) / incdel) - Gsel);
                }
            }

        } // End of ProcessData method
        #endregion

        #region btnMode_Click Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void btnMode_Click(object sender, EventArgs eventArgs)
        {
            this.b3dMode = !this.b3dMode;

            if (this.b3dMode == true)
            {
                this.btnMode.Text = "Use 2D";

                this.usrCtrlAxis2D.Visible = false;
                this.usrCtrlAxis3D.Visible = true;
            }
            else
            {
                this.btnMode.Text = "Use 3D";
                this.usrCtrlAxis2D.Visible = true;
                this.usrCtrlAxis3D.Visible = false;
            }
        }
        #endregion

        #region chkBoxSmoothing_CheckedChanged Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void chkBoxSmoothing_CheckedChanged(object sender, EventArgs eventArgs)
        {
            this.bUseSmoothing = this.chkBoxSmoothing.Checked;
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            this.mySerialPort.Close();
            trd.Abort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.mySerialPort.Open();

                Thread.Sleep(2000);

                Utility.Timer(DirectXTimer.Start);

                this.txtOutput.Text += "Serial port opened...\r\n";
            }
            catch (Exception ex)
            {
                this.txtOutput.Text += String.Format("{0}\r\n", ex.Message);
            }
            trd = new Thread(new ThreadStart(this.drawvalue));
            trd.Start();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Utility.Timer(DirectXTimer.Stop);
            this.mySerialPort.Close();
            //trd.Suspend();
            //trd.Abort();
            //this.Dispose();
        }

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAll.Checked == false)
            {
                checkBoxXtran.Checked = false;
                checkBoxYtran.Checked = false;
                checkBoxZtran.Checked = false;
            }
            else
            {
                checkBoxXtran.Checked = true;
                checkBoxYtran.Checked = true;
                checkBoxZtran.Checked = true;
            }

        }

        private void button1G_Click(object sender, EventArgs e)
        {
            labelSelG.Text = "1,5G";
            Gsel = 1.5f;
            this.mySerialPort.Write(this.sendChars, 0, this.sendChars.Length);
        }

        private void button2G_Click(object sender, EventArgs e)
        {
            labelSelG.Text = "2G";
            Gsel = 2f;
            this.mySerialPort.Write(this.sendChars, 0, this.sendChars.Length);
        }

        private void button4G_Click(object sender, EventArgs e)
        {
            labelSelG.Text = "4G";
            Gsel = 4f;
            this.mySerialPort.Write(this.sendChars, 0, this.sendChars.Length);
        }

        private void button6G_Click(object sender, EventArgs e)
        {
            labelSelG.Text = "6G";
            Gsel = 6f;
            this.mySerialPort.Write(this.sendChars, 0, this.sendChars.Length);
        }

        private void buttonGTest_Click(object sender, EventArgs e)
        {
            labelSelG.Text = "Test";
            Gsel = 0f;
            this.mySerialPort.Write(this.sendChars, 0, this.sendChars.Length);
        }

    } // End of FrmMain class

    #region UpdateSerialData Delegate
    /// <summary>
    /// Helper delegate to update UI and be thread safe.
    /// </summary>
    public delegate void UpdateSerialData(Object objSender, SerialDataReceivedEventArgs dataReceivedEventArgs);
    #endregion

} // End of Accelerometer01 namespace