namespace SerialPortTerminal
{
  partial class frmTerminal
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTerminal));
        this.rtfTerminal = new System.Windows.Forms.RichTextBox();
        this.txtSendData = new System.Windows.Forms.TextBox();
        this.lblSend = new System.Windows.Forms.Label();
        this.btnSend = new System.Windows.Forms.Button();
        this.cmbPortName = new System.Windows.Forms.ComboBox();
        this.cmbBaudRate = new System.Windows.Forms.ComboBox();
        this.rbHex = new System.Windows.Forms.RadioButton();
        this.rbText = new System.Windows.Forms.RadioButton();
        this.gbMode = new System.Windows.Forms.GroupBox();
        this.lblComPort = new System.Windows.Forms.Label();
        this.lblBaudRate = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.cmbParity = new System.Windows.Forms.ComboBox();
        this.lblDataBits = new System.Windows.Forms.Label();
        this.cmbDataBits = new System.Windows.Forms.ComboBox();
        this.lblStopBits = new System.Windows.Forms.Label();
        this.cmbStopBits = new System.Windows.Forms.ComboBox();
        this.btnOpenPort = new System.Windows.Forms.Button();
        this.gbPortSettings = new System.Windows.Forms.GroupBox();
        this.lnkAbout = new System.Windows.Forms.LinkLabel();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.chkRTS = new System.Windows.Forms.CheckBox();
        this.chkCD = new System.Windows.Forms.CheckBox();
        this.chkDSR = new System.Windows.Forms.CheckBox();
        this.chkCTS = new System.Windows.Forms.CheckBox();
        this.chkDTR = new System.Windows.Forms.CheckBox();
        this.btnClear = new System.Windows.Forms.Button();
        this.chkClearOnOpen = new System.Windows.Forms.CheckBox();
        this.chkClearWithDTR = new System.Windows.Forms.CheckBox();
        this.tmrCheckComPorts = new System.Windows.Forms.Timer(this.components);
        this.toolTip = new System.Windows.Forms.ToolTip(this.components);
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.button4 = new System.Windows.Forms.Button();
        this.button5 = new System.Windows.Forms.Button();
        this.button6 = new System.Windows.Forms.Button();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.label4 = new System.Windows.Forms.Label();
        this.textBoxRZ = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.textBoxRY = new System.Windows.Forms.TextBox();
        this.label2 = new System.Windows.Forms.Label();
        this.textBoxRX = new System.Windows.Forms.TextBox();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.button7 = new System.Windows.Forms.Button();
        this.label5 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.gbMode.SuspendLayout();
        this.gbPortSettings.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        this.SuspendLayout();
        // 
        // rtfTerminal
        // 
        this.rtfTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.rtfTerminal.Location = new System.Drawing.Point(12, 12);
        this.rtfTerminal.Name = "rtfTerminal";
        this.rtfTerminal.Size = new System.Drawing.Size(466, 83);
        this.rtfTerminal.TabIndex = 0;
        this.rtfTerminal.Text = "";
        // 
        // txtSendData
        // 
        this.txtSendData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.txtSendData.Location = new System.Drawing.Point(76, 198);
        this.txtSendData.Name = "txtSendData";
        this.txtSendData.Size = new System.Drawing.Size(402, 20);
        this.txtSendData.TabIndex = 2;
        this.txtSendData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSendData_KeyDown);
        this.txtSendData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSendData_KeyPress);
        // 
        // lblSend
        // 
        this.lblSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.lblSend.AutoSize = true;
        this.lblSend.Location = new System.Drawing.Point(12, 201);
        this.lblSend.Name = "lblSend";
        this.lblSend.Size = new System.Drawing.Size(61, 13);
        this.lblSend.TabIndex = 1;
        this.lblSend.Text = "Send &Data:";
        // 
        // btnSend
        // 
        this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnSend.Location = new System.Drawing.Point(293, 54);
        this.btnSend.Name = "btnSend";
        this.btnSend.Size = new System.Drawing.Size(75, 23);
        this.btnSend.TabIndex = 3;
        this.btnSend.Text = "Send";
        this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
        // 
        // cmbPortName
        // 
        this.cmbPortName.FormattingEnabled = true;
        this.cmbPortName.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6"});
        this.cmbPortName.Location = new System.Drawing.Point(13, 35);
        this.cmbPortName.Name = "cmbPortName";
        this.cmbPortName.Size = new System.Drawing.Size(67, 21);
        this.cmbPortName.TabIndex = 1;
        this.cmbPortName.Text = "COM1";
        // 
        // cmbBaudRate
        // 
        this.cmbBaudRate.FormattingEnabled = true;
        this.cmbBaudRate.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "28800",
            "36000",
            "115000"});
        this.cmbBaudRate.Location = new System.Drawing.Point(86, 35);
        this.cmbBaudRate.Name = "cmbBaudRate";
        this.cmbBaudRate.Size = new System.Drawing.Size(69, 21);
        this.cmbBaudRate.TabIndex = 3;
        this.cmbBaudRate.Text = "9600";
        this.cmbBaudRate.Validating += new System.ComponentModel.CancelEventHandler(this.cmbBaudRate_Validating);
        // 
        // rbHex
        // 
        this.rbHex.AutoSize = true;
        this.rbHex.Location = new System.Drawing.Point(12, 39);
        this.rbHex.Name = "rbHex";
        this.rbHex.Size = new System.Drawing.Size(44, 17);
        this.rbHex.TabIndex = 1;
        this.rbHex.Text = "Hex";
        this.rbHex.CheckedChanged += new System.EventHandler(this.rbHex_CheckedChanged);
        // 
        // rbText
        // 
        this.rbText.AutoSize = true;
        this.rbText.Location = new System.Drawing.Point(12, 19);
        this.rbText.Name = "rbText";
        this.rbText.Size = new System.Drawing.Size(46, 17);
        this.rbText.TabIndex = 0;
        this.rbText.Text = "Text";
        this.rbText.CheckedChanged += new System.EventHandler(this.rbText_CheckedChanged);
        // 
        // gbMode
        // 
        this.gbMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.gbMode.Controls.Add(this.rbText);
        this.gbMode.Controls.Add(this.rbHex);
        this.gbMode.Location = new System.Drawing.Point(388, 224);
        this.gbMode.Name = "gbMode";
        this.gbMode.Size = new System.Drawing.Size(89, 64);
        this.gbMode.TabIndex = 5;
        this.gbMode.TabStop = false;
        this.gbMode.Text = "Data &Mode";
        // 
        // lblComPort
        // 
        this.lblComPort.AutoSize = true;
        this.lblComPort.Location = new System.Drawing.Point(12, 19);
        this.lblComPort.Name = "lblComPort";
        this.lblComPort.Size = new System.Drawing.Size(56, 13);
        this.lblComPort.TabIndex = 0;
        this.lblComPort.Text = "COM Port:";
        // 
        // lblBaudRate
        // 
        this.lblBaudRate.AutoSize = true;
        this.lblBaudRate.Location = new System.Drawing.Point(85, 19);
        this.lblBaudRate.Name = "lblBaudRate";
        this.lblBaudRate.Size = new System.Drawing.Size(61, 13);
        this.lblBaudRate.TabIndex = 2;
        this.lblBaudRate.Text = "Baud Rate:";
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(163, 19);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(36, 13);
        this.label1.TabIndex = 4;
        this.label1.Text = "Parity:";
        // 
        // cmbParity
        // 
        this.cmbParity.FormattingEnabled = true;
        this.cmbParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
        this.cmbParity.Location = new System.Drawing.Point(161, 35);
        this.cmbParity.Name = "cmbParity";
        this.cmbParity.Size = new System.Drawing.Size(60, 21);
        this.cmbParity.TabIndex = 5;
        this.cmbParity.Text = "Even";
        // 
        // lblDataBits
        // 
        this.lblDataBits.AutoSize = true;
        this.lblDataBits.Location = new System.Drawing.Point(229, 19);
        this.lblDataBits.Name = "lblDataBits";
        this.lblDataBits.Size = new System.Drawing.Size(53, 13);
        this.lblDataBits.TabIndex = 6;
        this.lblDataBits.Text = "Data Bits:";
        // 
        // cmbDataBits
        // 
        this.cmbDataBits.FormattingEnabled = true;
        this.cmbDataBits.Items.AddRange(new object[] {
            "7",
            "8",
            "9"});
        this.cmbDataBits.Location = new System.Drawing.Point(227, 35);
        this.cmbDataBits.Name = "cmbDataBits";
        this.cmbDataBits.Size = new System.Drawing.Size(60, 21);
        this.cmbDataBits.TabIndex = 7;
        this.cmbDataBits.Text = "8";
        this.cmbDataBits.Validating += new System.ComponentModel.CancelEventHandler(this.cmbDataBits_Validating);
        // 
        // lblStopBits
        // 
        this.lblStopBits.AutoSize = true;
        this.lblStopBits.Location = new System.Drawing.Point(295, 19);
        this.lblStopBits.Name = "lblStopBits";
        this.lblStopBits.Size = new System.Drawing.Size(52, 13);
        this.lblStopBits.TabIndex = 8;
        this.lblStopBits.Text = "Stop Bits:";
        // 
        // cmbStopBits
        // 
        this.cmbStopBits.FormattingEnabled = true;
        this.cmbStopBits.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
        this.cmbStopBits.Location = new System.Drawing.Point(293, 35);
        this.cmbStopBits.Name = "cmbStopBits";
        this.cmbStopBits.Size = new System.Drawing.Size(65, 21);
        this.cmbStopBits.TabIndex = 9;
        this.cmbStopBits.Text = "Two";
        // 
        // btnOpenPort
        // 
        this.btnOpenPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnOpenPort.Location = new System.Drawing.Point(397, 294);
        this.btnOpenPort.Name = "btnOpenPort";
        this.btnOpenPort.Size = new System.Drawing.Size(75, 23);
        this.btnOpenPort.TabIndex = 6;
        this.btnOpenPort.Text = "&Open Port";
        this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
        // 
        // gbPortSettings
        // 
        this.gbPortSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.gbPortSettings.Controls.Add(this.cmbPortName);
        this.gbPortSettings.Controls.Add(this.cmbBaudRate);
        this.gbPortSettings.Controls.Add(this.cmbStopBits);
        this.gbPortSettings.Controls.Add(this.cmbParity);
        this.gbPortSettings.Controls.Add(this.cmbDataBits);
        this.gbPortSettings.Controls.Add(this.lblComPort);
        this.gbPortSettings.Controls.Add(this.lblStopBits);
        this.gbPortSettings.Controls.Add(this.lblBaudRate);
        this.gbPortSettings.Controls.Add(this.lblDataBits);
        this.gbPortSettings.Controls.Add(this.label1);
        this.gbPortSettings.Location = new System.Drawing.Point(12, 224);
        this.gbPortSettings.Name = "gbPortSettings";
        this.gbPortSettings.Size = new System.Drawing.Size(370, 64);
        this.gbPortSettings.TabIndex = 4;
        this.gbPortSettings.TabStop = false;
        this.gbPortSettings.Text = "COM Serial Port Settings";
        // 
        // lnkAbout
        // 
        this.lnkAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.lnkAbout.AutoSize = true;
        this.lnkAbout.Location = new System.Drawing.Point(437, 329);
        this.lnkAbout.Name = "lnkAbout";
        this.lnkAbout.Size = new System.Drawing.Size(35, 13);
        this.lnkAbout.TabIndex = 8;
        this.lnkAbout.TabStop = true;
        this.lnkAbout.Text = "&About";
        this.lnkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAbout_LinkClicked);
        // 
        // groupBox1
        // 
        this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.groupBox1.Controls.Add(this.chkRTS);
        this.groupBox1.Controls.Add(this.chkCD);
        this.groupBox1.Controls.Add(this.chkDSR);
        this.groupBox1.Controls.Add(this.chkCTS);
        this.groupBox1.Controls.Add(this.chkDTR);
        this.groupBox1.Location = new System.Drawing.Point(12, 294);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(272, 48);
        this.groupBox1.TabIndex = 7;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "&Line Signals";
        // 
        // chkRTS
        // 
        this.chkRTS.AutoSize = true;
        this.chkRTS.Location = new System.Drawing.Point(65, 20);
        this.chkRTS.Name = "chkRTS";
        this.chkRTS.Size = new System.Drawing.Size(48, 17);
        this.chkRTS.TabIndex = 1;
        this.chkRTS.Text = "RTS";
        this.toolTip.SetToolTip(this.chkRTS, "Pin 7 on DB9, Output, Request to Send");
        this.chkRTS.UseVisualStyleBackColor = true;
        this.chkRTS.CheckedChanged += new System.EventHandler(this.chkRTS_CheckedChanged);
        // 
        // chkCD
        // 
        this.chkCD.AutoSize = true;
        this.chkCD.Enabled = false;
        this.chkCD.Location = new System.Drawing.Point(226, 20);
        this.chkCD.Name = "chkCD";
        this.chkCD.Size = new System.Drawing.Size(41, 17);
        this.chkCD.TabIndex = 4;
        this.chkCD.Text = "CD";
        this.toolTip.SetToolTip(this.chkCD, "Pin 1 on DB9, Input, Data Carrier Detect");
        this.chkCD.UseVisualStyleBackColor = true;
        // 
        // chkDSR
        // 
        this.chkDSR.AutoSize = true;
        this.chkDSR.Enabled = false;
        this.chkDSR.Location = new System.Drawing.Point(172, 20);
        this.chkDSR.Name = "chkDSR";
        this.chkDSR.Size = new System.Drawing.Size(49, 17);
        this.chkDSR.TabIndex = 3;
        this.chkDSR.Text = "DSR";
        this.toolTip.SetToolTip(this.chkDSR, "Pin 6 on DB9, Input, Data Set Ready");
        this.chkDSR.UseVisualStyleBackColor = true;
        // 
        // chkCTS
        // 
        this.chkCTS.AutoSize = true;
        this.chkCTS.Enabled = false;
        this.chkCTS.Location = new System.Drawing.Point(119, 20);
        this.chkCTS.Name = "chkCTS";
        this.chkCTS.Size = new System.Drawing.Size(47, 17);
        this.chkCTS.TabIndex = 2;
        this.chkCTS.Text = "CTS";
        this.toolTip.SetToolTip(this.chkCTS, "Pin 8 on DB9, Input, Clear to Send");
        this.chkCTS.UseVisualStyleBackColor = true;
        // 
        // chkDTR
        // 
        this.chkDTR.AutoSize = true;
        this.chkDTR.Location = new System.Drawing.Point(10, 20);
        this.chkDTR.Name = "chkDTR";
        this.chkDTR.Size = new System.Drawing.Size(49, 17);
        this.chkDTR.TabIndex = 0;
        this.chkDTR.Text = "DTR";
        this.toolTip.SetToolTip(this.chkDTR, "Pin 4 on DB9, Output, Data Terminal Ready");
        this.chkDTR.UseVisualStyleBackColor = true;
        this.chkDTR.CheckedChanged += new System.EventHandler(this.chkDTR_CheckedChanged);
        // 
        // btnClear
        // 
        this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnClear.Location = new System.Drawing.Point(385, 54);
        this.btnClear.Name = "btnClear";
        this.btnClear.Size = new System.Drawing.Size(75, 23);
        this.btnClear.TabIndex = 9;
        this.btnClear.Text = "&Clear";
        this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
        // 
        // chkClearOnOpen
        // 
        this.chkClearOnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkClearOnOpen.AutoSize = true;
        this.chkClearOnOpen.Location = new System.Drawing.Point(290, 304);
        this.chkClearOnOpen.Name = "chkClearOnOpen";
        this.chkClearOnOpen.Size = new System.Drawing.Size(94, 17);
        this.chkClearOnOpen.TabIndex = 10;
        this.chkClearOnOpen.Text = "Clear on Open";
        this.chkClearOnOpen.UseVisualStyleBackColor = true;
        // 
        // chkClearWithDTR
        // 
        this.chkClearWithDTR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.chkClearWithDTR.AutoSize = true;
        this.chkClearWithDTR.Location = new System.Drawing.Point(290, 325);
        this.chkClearWithDTR.Name = "chkClearWithDTR";
        this.chkClearWithDTR.Size = new System.Drawing.Size(98, 17);
        this.chkClearWithDTR.TabIndex = 11;
        this.chkClearWithDTR.Text = "Clear with DTR";
        this.chkClearWithDTR.UseVisualStyleBackColor = true;
        // 
        // tmrCheckComPorts
        // 
        this.tmrCheckComPorts.Enabled = true;
        this.tmrCheckComPorts.Interval = 500;
        this.tmrCheckComPorts.Tick += new System.EventHandler(this.tmrCheckComPorts_Tick);
        // 
        // button1
        // 
        this.button1.Location = new System.Drawing.Point(5, 19);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 12;
        this.button1.Text = "Control ON";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.button1_Click);
        // 
        // button2
        // 
        this.button2.Location = new System.Drawing.Point(83, 19);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 13;
        this.button2.Text = "Control OFF";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(this.button2_Click);
        // 
        // button3
        // 
        this.button3.Location = new System.Drawing.Point(161, 19);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(75, 23);
        this.button3.TabIndex = 14;
        this.button3.Text = "Servo ON";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(this.button3_Click);
        // 
        // button4
        // 
        this.button4.Location = new System.Drawing.Point(241, 19);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(75, 22);
        this.button4.TabIndex = 15;
        this.button4.Text = "Servo OFF";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(this.button4_Click);
        // 
        // button5
        // 
        this.button5.Location = new System.Drawing.Point(322, 19);
        this.button5.Name = "button5";
        this.button5.Size = new System.Drawing.Size(75, 23);
        this.button5.TabIndex = 16;
        this.button5.Text = "Set Point";
        this.button5.UseVisualStyleBackColor = true;
        this.button5.Click += new System.EventHandler(this.button5_Click);
        // 
        // button6
        // 
        this.button6.Location = new System.Drawing.Point(403, 19);
        this.button6.Name = "button6";
        this.button6.Size = new System.Drawing.Size(57, 23);
        this.button6.TabIndex = 17;
        this.button6.Text = "Move";
        this.button6.UseVisualStyleBackColor = true;
        this.button6.Click += new System.EventHandler(this.button6_Click);
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.label4);
        this.groupBox2.Controls.Add(this.textBoxRZ);
        this.groupBox2.Controls.Add(this.label3);
        this.groupBox2.Controls.Add(this.btnClear);
        this.groupBox2.Controls.Add(this.textBoxRY);
        this.groupBox2.Controls.Add(this.label2);
        this.groupBox2.Controls.Add(this.textBoxRX);
        this.groupBox2.Controls.Add(this.button1);
        this.groupBox2.Controls.Add(this.button6);
        this.groupBox2.Controls.Add(this.button2);
        this.groupBox2.Controls.Add(this.btnSend);
        this.groupBox2.Controls.Add(this.button5);
        this.groupBox2.Controls.Add(this.button3);
        this.groupBox2.Controls.Add(this.button4);
        this.groupBox2.Location = new System.Drawing.Point(12, 101);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(466, 89);
        this.groupBox2.TabIndex = 18;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "Robot Control";
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(189, 58);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(14, 13);
        this.label4.TabIndex = 23;
        this.label4.Text = "Z";
        // 
        // textBoxRZ
        // 
        this.textBoxRZ.Location = new System.Drawing.Point(208, 55);
        this.textBoxRZ.Name = "textBoxRZ";
        this.textBoxRZ.Size = new System.Drawing.Size(65, 20);
        this.textBoxRZ.TabIndex = 22;
        this.textBoxRZ.Text = "550.00";
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(101, 59);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(14, 13);
        this.label3.TabIndex = 21;
        this.label3.Text = "Y";
        // 
        // textBoxRY
        // 
        this.textBoxRY.Location = new System.Drawing.Point(120, 56);
        this.textBoxRY.Name = "textBoxRY";
        this.textBoxRY.Size = new System.Drawing.Size(65, 20);
        this.textBoxRY.TabIndex = 20;
        this.textBoxRY.Text = "0.00";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(11, 59);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(14, 13);
        this.label2.TabIndex = 19;
        this.label2.Text = "X";
        // 
        // textBoxRX
        // 
        this.textBoxRX.Location = new System.Drawing.Point(30, 56);
        this.textBoxRX.Name = "textBoxRX";
        this.textBoxRX.Size = new System.Drawing.Size(65, 20);
        this.textBoxRX.TabIndex = 18;
        this.textBoxRX.Text = "232.00";
        // 
        // pictureBox1
        // 
        this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
        this.pictureBox1.Location = new System.Drawing.Point(484, 12);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(401, 276);
        this.pictureBox1.TabIndex = 19;
        this.pictureBox1.TabStop = false;
        this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
        this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
        // 
        // button7
        // 
        this.button7.Location = new System.Drawing.Point(484, 300);
        this.button7.Name = "button7";
        this.button7.Size = new System.Drawing.Size(75, 23);
        this.button7.TabIndex = 20;
        this.button7.Text = "Draw";
        this.button7.UseVisualStyleBackColor = true;
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(850, 294);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(35, 13);
        this.label5.TabIndex = 21;
        this.label5.Text = "label5";
        // 
        // label6
        // 
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(850, 315);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(35, 13);
        this.label6.TabIndex = 22;
        this.label6.Text = "label6";
        // 
        // frmTerminal
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(897, 354);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.button7);
        this.Controls.Add(this.pictureBox1);
        this.Controls.Add(this.groupBox2);
        this.Controls.Add(this.chkClearWithDTR);
        this.Controls.Add(this.chkClearOnOpen);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.lnkAbout);
        this.Controls.Add(this.gbPortSettings);
        this.Controls.Add(this.btnOpenPort);
        this.Controls.Add(this.gbMode);
        this.Controls.Add(this.lblSend);
        this.Controls.Add(this.txtSendData);
        this.Controls.Add(this.rtfTerminal);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MinimumSize = new System.Drawing.Size(505, 250);
        this.Name = "frmTerminal";
        this.Text = "SerialPort Terminal";
        this.Load += new System.EventHandler(this.frmTerminal_Load);
        this.Shown += new System.EventHandler(this.frmTerminal_Shown);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTerminal_FormClosing);
        this.gbMode.ResumeLayout(false);
        this.gbMode.PerformLayout();
        this.gbPortSettings.ResumeLayout(false);
        this.gbPortSettings.PerformLayout();
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RichTextBox rtfTerminal;
    private System.Windows.Forms.TextBox txtSendData;
    private System.Windows.Forms.Label lblSend;
    private System.Windows.Forms.Button btnSend;
    private System.Windows.Forms.ComboBox cmbPortName;
    private System.Windows.Forms.ComboBox cmbBaudRate;
    private System.Windows.Forms.RadioButton rbHex;
    private System.Windows.Forms.RadioButton rbText;
    private System.Windows.Forms.GroupBox gbMode;
    private System.Windows.Forms.Label lblComPort;
    private System.Windows.Forms.Label lblBaudRate;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox cmbParity;
    private System.Windows.Forms.Label lblDataBits;
    private System.Windows.Forms.ComboBox cmbDataBits;
    private System.Windows.Forms.Label lblStopBits;
    private System.Windows.Forms.ComboBox cmbStopBits;
    private System.Windows.Forms.Button btnOpenPort;
    private System.Windows.Forms.GroupBox gbPortSettings;
		private System.Windows.Forms.LinkLabel lnkAbout;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox chkCD;
		private System.Windows.Forms.CheckBox chkDSR;
		private System.Windows.Forms.CheckBox chkCTS;
		private System.Windows.Forms.CheckBox chkDTR;
		private System.Windows.Forms.CheckBox chkRTS;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.CheckBox chkClearOnOpen;
		private System.Windows.Forms.CheckBox chkClearWithDTR;
		private System.Windows.Forms.Timer tmrCheckComPorts;
		private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxRZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxRY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRX;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
  }
}

