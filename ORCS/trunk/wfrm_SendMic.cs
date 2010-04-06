using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

using LumiSoft.Net.RTP;
using LumiSoft.Net.Media;

namespace Robot
{
    /// <summary>
    /// Send microphone audio window.
    /// </summary>
    public class wfrm_SendMic : Form
    {        
        private Label    mt_InDevices   = null;
        private ComboBox m_pInDevices   = null;
        private Label    mt_Codec       = null;
        private TextBox  m_pCodec       = null;
        private Label    mt_PacketsSent = null;
        private Label    m_pPacketsSent = null;
        private Label    mt_KBSent      = null;
        private Label    m_pKBSent      = null;
        private Button   m_pToggleSend  = null;

        private Robot1         m_pMainUI = null;
        private RTP_Session    m_pSession    = null;
        private AudioIn        m_pWaveIn     = null;
        private RTP_SendStream m_pSendStream = null;
        private long           m_PacketsSent = 0;
        private long           m_TotalSent   = 0;
        private bool           m_Send        = false;
        private RTP_Packet     m_pRtpPacket  = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="owner">Main UI.</param>
        /// <param name="session">RTP session.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>owner</b> or <b>session</b> is null reference.</exception>
        public wfrm_SendMic(Robot1 owner, RTP_Session session)
        {
            if(owner == null){
                throw new ArgumentNullException("owner");
            }
            if(session == null){
                throw new ArgumentNullException("session");
            }

            m_pMainUI  = owner;
            m_pSession = session;

            InitUI();

            // Load input devices.
            m_pInDevices.Items.Clear();
            foreach(AudioInDevice device in AudioIn.Devices){
                m_pInDevices.Items.Add(device.Name);
            }
            if(m_pInDevices.Items.Count > 0){
                m_pInDevices.SelectedIndex = 0;
            }

            m_pRtpPacket = new RTP_Packet();
            m_pRtpPacket.Data = new byte[400];
        }

        #region method InitUI

        /// <summary>
        /// Creates and initializes UI.
        /// </summary>
        private void InitUI()
        {
            this.Size = new Size(350,150);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Text = "Sending microphone";
            this.FormClosing += new FormClosingEventHandler(wfrm_SendMic_FormClosing);

            mt_InDevices = new Label();
            mt_InDevices.Size = new Size(100,20);
            mt_InDevices.Location = new Point(0,10);
            mt_InDevices.Text = "Output device:";
            mt_InDevices.TextAlign = ContentAlignment.MiddleRight;

            m_pInDevices = new ComboBox();
            m_pInDevices.Size = new Size(200,20);
            m_pInDevices.Location = new Point(105,10);
            m_pInDevices.DropDownStyle = ComboBoxStyle.DropDownList;

            mt_Codec = new Label();
            mt_Codec.Location = new Point(0,35);
            mt_Codec.Size = new Size(100,20);
            mt_Codec.Text = "Codec:";
            mt_Codec.TextAlign = ContentAlignment.MiddleRight;

            m_pCodec = new TextBox();
            m_pCodec.Location = new Point(105,35);
            m_pCodec.Size = new Size(100,20);
            m_pCodec.ReadOnly = true;

            mt_PacketsSent = new Label();
            mt_PacketsSent.Location = new Point(0,60);
            mt_PacketsSent.Size = new Size(100,20);
            mt_PacketsSent.Text = "Packets sent:";
            mt_PacketsSent.TextAlign = ContentAlignment.MiddleRight;

            m_pPacketsSent = new Label();
            m_pPacketsSent.Location = new Point(105,60);
            m_pPacketsSent.Size = new Size(100,20);
            m_pPacketsSent.Text = "0";
            m_pPacketsSent.TextAlign = ContentAlignment.MiddleLeft;

            mt_KBSent = new Label();
            mt_KBSent.Location = new Point(0,85);
            mt_KBSent.Size = new Size(100,20);
            mt_KBSent.Text = "KB sent:";
            mt_KBSent.TextAlign = ContentAlignment.MiddleRight;

            m_pKBSent = new Label();
            m_pKBSent.Location = new Point(105,85);
            m_pKBSent.Size = new Size(100,20);
            m_pKBSent.Text = "0";
            m_pKBSent.TextAlign = ContentAlignment.MiddleLeft;

            m_pToggleSend = new Button();
            m_pToggleSend.Size = new Size(70,20);
            m_pToggleSend.Location = new Point(235,85);
            m_pToggleSend.Text = "Send";
            m_pToggleSend.Click += new EventHandler(m_pToggleSend_Click);

            this.Controls.Add(mt_InDevices);
            this.Controls.Add(m_pInDevices);
            this.Controls.Add(mt_Codec);
            this.Controls.Add(m_pCodec);
            this.Controls.Add(mt_PacketsSent);
            this.Controls.Add(m_pPacketsSent);
            this.Controls.Add(mt_KBSent);
            this.Controls.Add(m_pKBSent);
            this.Controls.Add(m_pToggleSend);
        }
                                
        #endregion


        #region Events handling

        #region method m_pToggleSend_Click

        private void m_pToggleSend_Click(object sender,EventArgs e)
        {
            if(m_pWaveIn == null){
                m_pSendStream = m_pSession.CreateSendStream();

                m_pWaveIn = new AudioIn(AudioIn.Devices[m_pInDevices.SelectedIndex],8000,16,1);
                //m_pWaveIn.Start();

                m_pToggleSend.Text = "Stop";
                m_Send = true;

                Thread tr = new Thread(this.SendMic);
                tr.Start();
            }
            else{
                m_pSendStream.Close();
                m_pSendStream = null;
                m_pWaveIn.Dispose();
                m_pWaveIn = null;

                m_pToggleSend.Text = "Send";
                m_Send = false;
            }
        }
                
        #endregion


        #region method wfrm_SendMic_FormClosing

        private void wfrm_SendMic_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Send = false;
            if(m_pWaveIn != null){
                m_pWaveIn.Dispose();
                m_pWaveIn = null;
            }
            if(m_pSendStream != null){
                m_pSendStream.Close();
                m_pSendStream = null;
            }
        }

        #endregion

        #endregion


        #region method SendMic

        private void SendMic()
        {
            byte[] readBuffer = new byte[400];
            while(m_Send){
                try{
                    // We have not enough data for RTP packet, wait more to be available.
                    if(m_pWaveIn.Available < readBuffer.Length){
                        Thread.Sleep(1);
                    }
                    else{
                        m_pWaveIn.Read(readBuffer,0,readBuffer.Length);

                        m_pRtpPacket.Data = m_pMainUI.ActiveCodec.Encode(readBuffer,0,readBuffer.Length);
                        m_pRtpPacket.Timestamp = m_pSendStream.Session.RtpClock.RtpTimestamp;
                        m_pSendStream.Send(m_pRtpPacket);

                        m_PacketsSent++;
                        m_TotalSent += m_pRtpPacket.Data.Length;

                        this.BeginInvoke(new MethodInvoker(delegate(){
                            m_pCodec.Text       = m_pMainUI.ActiveCodec.Name;
                            m_pPacketsSent.Text = m_PacketsSent.ToString();
                            m_pKBSent.Text      = Convert.ToString(m_TotalSent / 1000);
                        }));
                    }
                }
                catch{
                }
            }
        }

        #endregion
    }
}
