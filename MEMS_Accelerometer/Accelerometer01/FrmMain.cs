/////////////////////////////////////////////////////////////
//
//  Filename: FrmMain.cs
//  Author:   Travis Feirtag
//  Date:     05/12/2006 11:30:36
//  CLR ver:  2.0.50727.42
//  Project:  Accelerometer01
//  Modified: Kamil Zidek
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
    #endregion

    /// <summary>
    /// Summary of FrmMain class
    /// </summary>
    public sealed class FrmMain : Form
    {
        #region Constants

        #endregion

        #region Fields
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
        private UsrCtrlAxis2D usrCtrlAxis2D = null;
        private UsrCtrlAxis3D usrCtrlAxis3D = null;

        private bool bUseSmoothing = false;
        private int nSmoothingDelta = 5;
        private int nPrevX = 0;
        private int nPrevY = 0;
        private int nPrevZ = 0;

        Byte byte01 = 127; //
        Byte byte02 = 127; //X
        Byte byte03 = 127; //Z //CHANGE
        Byte byte04 = 127; //Y
        Byte byte05 = 127; //13
        Byte byte06 = 127; //10

        Int32 nXaxis = 127;
        Int32 nYaxis = 127;
        Int32 nZaxis = 127;

        //delayed values

        Byte byte010 = 127; //
        Byte byte020 = 127; //X
        Byte byte030 = 127; //Z //CHANGE
        Byte byte040 = 127; //Y

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

        private TextBox txtOutput;
        private System.Windows.Forms.Timer timerMain;
        private TextBox txtByte01;
        private TextBox txtByte02;
        private TextBox txtByte03;
        private TextBox txtByte04;
        private TextBox txtBytesRead;
        private TextBox txtRefreshRate;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox txtXaxis;
        private TextBox txtMinX;
        private TextBox txtMaxX;
        private Label label9;
        private Label label10;
        private Label label11;
        private Button btnMode;
        private CheckBox chkBoxSmoothing;
        private TextBox txtZaxis;
        private TextBox txtMinZ;
        private TextBox txtMaxZ;
        private Button button1;
        private Button button2;
        private Label label13;
        private TextBox txtAngleX;
        private TextBox txtAngleZ;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Panel panel1;
        private RadioButton radioButton3;
        private RadioButton radioButton1;
        private Label label14;
        private GroupBox groupBox4;
        private RadioButton radioButton2;
        private TextBox txtAngleY;
        private TextBox txtMaxY;
        private TextBox txtMinY;
        private TextBox txtYaxis;
        private Label label7;
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
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.txtByte01 = new System.Windows.Forms.TextBox();
            this.txtByte02 = new System.Windows.Forms.TextBox();
            this.txtByte03 = new System.Windows.Forms.TextBox();
            this.txtByte04 = new System.Windows.Forms.TextBox();
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
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.txtAngleY = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtMaxY = new System.Windows.Forms.TextBox();
            this.txtMinY = new System.Windows.Forms.TextBox();
            this.txtYaxis = new System.Windows.Forms.TextBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtOutput
            // 
            this.txtOutput.AcceptsReturn = true;
            this.txtOutput.AcceptsTab = true;
            this.txtOutput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtOutput.Location = new System.Drawing.Point(-1, 350);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(352, 59);
            this.txtOutput.TabIndex = 0;
            // 
            // timerMain
            // 
            this.timerMain.Interval = 40;
            this.timerMain.Tick += new System.EventHandler(this.TimerMain_Tick);
            // 
            // txtByte01
            // 
            this.txtByte01.Location = new System.Drawing.Point(82, 13);
            this.txtByte01.Name = "txtByte01";
            this.txtByte01.Size = new System.Drawing.Size(57, 20);
            this.txtByte01.TabIndex = 1;
            // 
            // txtByte02
            // 
            this.txtByte02.Location = new System.Drawing.Point(82, 39);
            this.txtByte02.Name = "txtByte02";
            this.txtByte02.Size = new System.Drawing.Size(57, 20);
            this.txtByte02.TabIndex = 2;
            // 
            // txtByte03
            // 
            this.txtByte03.Location = new System.Drawing.Point(82, 93);
            this.txtByte03.Name = "txtByte03";
            this.txtByte03.Size = new System.Drawing.Size(57, 20);
            this.txtByte03.TabIndex = 3;
            // 
            // txtByte04
            // 
            this.txtByte04.Location = new System.Drawing.Point(82, 65);
            this.txtByte04.Name = "txtByte04";
            this.txtByte04.Size = new System.Drawing.Size(57, 20);
            this.txtByte04.TabIndex = 4;
            // 
            // txtBytesRead
            // 
            this.txtBytesRead.Location = new System.Drawing.Point(88, 18);
            this.txtBytesRead.Name = "txtBytesRead";
            this.txtBytesRead.Size = new System.Drawing.Size(57, 20);
            this.txtBytesRead.TabIndex = 5;
            // 
            // txtRefreshRate
            // 
            this.txtRefreshRate.Location = new System.Drawing.Point(88, 44);
            this.txtRefreshRate.Name = "txtRefreshRate";
            this.txtRefreshRate.Size = new System.Drawing.Size(57, 20);
            this.txtRefreshRate.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "$";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Z";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "X";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Y";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Bytes Read";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Refresh Rate";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtXaxis
            // 
            this.txtXaxis.Location = new System.Drawing.Point(55, 36);
            this.txtXaxis.Name = "txtXaxis";
            this.txtXaxis.Size = new System.Drawing.Size(57, 20);
            this.txtXaxis.TabIndex = 13;
            // 
            // txtMinX
            // 
            this.txtMinX.Location = new System.Drawing.Point(55, 62);
            this.txtMinX.Name = "txtMinX";
            this.txtMinX.Size = new System.Drawing.Size(57, 20);
            this.txtMinX.TabIndex = 17;
            // 
            // txtMaxX
            // 
            this.txtMaxX.Location = new System.Drawing.Point(55, 88);
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.Size = new System.Drawing.Size(57, 20);
            this.txtMaxX.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Current";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Min";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(25, 91);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Max";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnMode
            // 
            this.btnMode.Location = new System.Drawing.Point(378, 115);
            this.btnMode.Name = "btnMode";
            this.btnMode.Size = new System.Drawing.Size(122, 29);
            this.btnMode.TabIndex = 24;
            this.btnMode.Text = "Switch to 3D";
            this.btnMode.UseVisualStyleBackColor = true;
            this.btnMode.Click += new System.EventHandler(this.btnMode_Click);
            // 
            // chkBoxSmoothing
            // 
            this.chkBoxSmoothing.AutoSize = true;
            this.chkBoxSmoothing.Location = new System.Drawing.Point(389, 194);
            this.chkBoxSmoothing.Name = "chkBoxSmoothing";
            this.chkBoxSmoothing.Size = new System.Drawing.Size(85, 17);
            this.chkBoxSmoothing.TabIndex = 25;
            this.chkBoxSmoothing.Text = "Fail suspend";
            this.chkBoxSmoothing.UseVisualStyleBackColor = true;
            this.chkBoxSmoothing.CheckedChanged += new System.EventHandler(this.chkBoxSmoothing_CheckedChanged);
            // 
            // txtZaxis
            // 
            this.txtZaxis.Location = new System.Drawing.Point(18, 35);
            this.txtZaxis.Name = "txtZaxis";
            this.txtZaxis.Size = new System.Drawing.Size(64, 20);
            this.txtZaxis.TabIndex = 26;
            // 
            // txtMinZ
            // 
            this.txtMinZ.Location = new System.Drawing.Point(18, 61);
            this.txtMinZ.Name = "txtMinZ";
            this.txtMinZ.Size = new System.Drawing.Size(64, 20);
            this.txtMinZ.TabIndex = 27;
            // 
            // txtMaxZ
            // 
            this.txtMaxZ.Location = new System.Drawing.Point(18, 87);
            this.txtMaxZ.Name = "txtMaxZ";
            this.txtMaxZ.Size = new System.Drawing.Size(64, 20);
            this.txtMaxZ.TabIndex = 28;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(378, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 32);
            this.button1.TabIndex = 30;
            this.button1.Text = "Open Serial Port";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(378, 69);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 33);
            this.button2.TabIndex = 31;
            this.button2.Text = "Close Serial Port";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(18, 120);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 32;
            this.label13.Text = "Angle";
            // 
            // txtAngleX
            // 
            this.txtAngleX.Location = new System.Drawing.Point(55, 117);
            this.txtAngleX.Name = "txtAngleX";
            this.txtAngleX.Size = new System.Drawing.Size(57, 20);
            this.txtAngleX.TabIndex = 33;
            // 
            // txtAngleZ
            // 
            this.txtAngleZ.Location = new System.Drawing.Point(41, 115);
            this.txtAngleZ.Name = "txtAngleZ";
            this.txtAngleZ.Size = new System.Drawing.Size(41, 20);
            this.txtAngleZ.TabIndex = 35;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
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
            this.groupBox1.Location = new System.Drawing.Point(372, 251);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 144);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MEMS Angle Values";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(55, 14);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(54, 17);
            this.radioButton1.TabIndex = 37;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "X Axis";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(127, 14);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(54, 17);
            this.radioButton2.TabIndex = 43;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Y Axis";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // txtAngleY
            // 
            this.txtAngleY.Location = new System.Drawing.Point(124, 117);
            this.txtAngleY.Name = "txtAngleY";
            this.txtAngleY.Size = new System.Drawing.Size(57, 20);
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
            this.txtMaxY.Location = new System.Drawing.Point(124, 88);
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.Size = new System.Drawing.Size(57, 20);
            this.txtMaxY.TabIndex = 41;
            // 
            // txtMinY
            // 
            this.txtMinY.Location = new System.Drawing.Point(124, 62);
            this.txtMinY.Name = "txtMinY";
            this.txtMinY.Size = new System.Drawing.Size(57, 20);
            this.txtMinY.TabIndex = 40;
            // 
            // txtYaxis
            // 
            this.txtYaxis.Location = new System.Drawing.Point(124, 36);
            this.txtYaxis.Name = "txtYaxis";
            this.txtYaxis.Size = new System.Drawing.Size(57, 20);
            this.txtYaxis.TabIndex = 39;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(21, 15);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(54, 17);
            this.radioButton3.TabIndex = 39;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Z Axis";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtRefreshRate);
            this.groupBox2.Controls.Add(this.txtBytesRead);
            this.groupBox2.Location = new System.Drawing.Point(512, 164);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 72);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Info";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.txtByte04);
            this.groupBox3.Controls.Add(this.txtByte03);
            this.groupBox3.Controls.Add(this.txtByte02);
            this.groupBox3.Controls.Add(this.txtByte01);
            this.groupBox3.Location = new System.Drawing.Point(515, 22);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(157, 120);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Raw Data";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(351, 355);
            this.panel1.TabIndex = 39;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton3);
            this.groupBox4.Controls.Add(this.txtZaxis);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtMinZ);
            this.groupBox4.Controls.Add(this.txtMaxZ);
            this.groupBox4.Controls.Add(this.txtAngleZ);
            this.groupBox4.Location = new System.Drawing.Point(572, 251);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(100, 144);
            this.groupBox4.TabIndex = 40;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "MEMS Free Fall";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "Fall";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 407);
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
            this.Text = "Robot Tilt Accelerometer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
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
            while (inkr < 5000)
            {
                Thread.Sleep(200);

                this.txtByte01.Text = String.Format("{0:x2}", byte010); //$
                this.txtByte02.Text = String.Format("{0:x2}", byte020); //x 
                this.txtByte03.Text = String.Format("{0:x2}", byte030); //z  //CHANGE
                this.txtByte04.Text = String.Format("{0:x2}", byte040); //y

                this.txtXaxis.Text = String.Format("{0}", nXaxis0);
                this.txtYaxis.Text = String.Format("{0}", nYaxis0);
                this.txtZaxis.Text = String.Format("{0}", nZaxis0); //NEW fail

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

                ///NEW
                if (fAngley0 < 50 || fAngley0 > 130) //WARNING Y(Z)
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
                //NEW

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

        #region OnClosing Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancelEventArgs"></param>
        protected override void OnClosing(CancelEventArgs cancelEventArgs)
        {
            this.timerMain.Enabled = false;
            this.mySerialPort.Close();

            base.OnClosing(cancelEventArgs);
        }
        #endregion

        #region TimerMain_Tick Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void TimerMain_Tick(object sender, EventArgs eventArgs)
        {
            if (this.mySerialPort.IsOpen == false)
                this.txtOutput.Text = "Serial port is not opened.";
            else
            {
//                this.mySerialPort.Write(this.sendChars, 0, this.sendChars.Length);
            }
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
            if (this.byteQueue.Count < 6)
                return;

            byte01 = (Byte)this.byteQueue.Dequeue(); //$
            if (byte01 != 36)
                return;
            byte02 = (Byte)this.byteQueue.Dequeue(); //X
            byte03 = (Byte)this.byteQueue.Dequeue(); //Z //CHANGE
            byte04 = (Byte)this.byteQueue.Dequeue(); //Y
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
            nYaxis = Convert.ToInt32(byte04); //CHANGE SUPER
            nZaxis = Convert.ToInt32(byte03);

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

            //delayed write
            inkr = inkr + 1;

            if (inkr >= 1)
            {
                byte010 = byte01; //
                byte020 = byte02; //X
                byte030 = byte03; //Z //CHANGE
                byte040 = byte04; //Y

                nXaxis0 = nXaxis;
                nYaxis0 = nYaxis;
                nZaxis0 = nZaxis;  //NEW repair

                nMinX0 = nMinX;//10000;
                nMaxX0 = nMaxX;//-10000;
                nMinY0 = nMinY;//10000;
                nMaxY0 = nMaxY;//-10000;
                nMinZ0 = nMinZ;//10000;
                nMaxZ0 = nMaxZ;//-10000;

                inkr = 0;
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
                if (radio == 3)
                {
                    this.usrCtrlAxis2D.SetCurrentValue(nZaxis);
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
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Utility.Timer(DirectXTimer.Stop);
            this.mySerialPort.Close();
            //trd.Suspend();
            //trd.Abort();
            //this.Dispose();
        }

    } // End of FrmMain class

    #region UpdateSerialData Delegate
    /// <summary>
    /// Helper delegate to update UI and be thread safe.
    /// </summary>
    public delegate void UpdateSerialData(Object objSender, SerialDataReceivedEventArgs dataReceivedEventArgs);
    #endregion

} // End of Accelerometer01 namespace