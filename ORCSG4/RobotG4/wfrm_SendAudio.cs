using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

using LumiSoft.Net.RTP;

namespace Robot
{
    /// <summary>
    /// Send PCM audio window.
    /// </summary>
    public class wfrm_SendAudio : Form
    {
        private Label   mt_Codec       = null;
        private TextBox m_pCodec       = null;
        private Label   mt_PacketsSent = null;
        private Label   m_pPacketsSent = null;
        private Label   mt_KBSent      = null;
        private Label   m_pKBSent      = null;

        private Robot1 m_pMainUI = null;
        private RTP_Session m_pSession = null;
        private string      m_SendFile = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="owner">Main UI.</param>
        /// <param name="session">RTP session.</param>
        /// <param name="sendFile">File which data to send.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>owner</b>, <b>session</b> or <b>sendFile</b> is null reference.</exception>
        public wfrm_SendAudio(Robot1 owner, RTP_Session session, string sendFile)
        {
            if(owner == null){
                throw new ArgumentNullException("owner");
            }
            if(session == null){
                throw new ArgumentNullException("session");
            }
            if(sendFile == null){
                throw new ArgumentNullException("sendFile");
            }

            m_pMainUI  = owner;
            m_pSession = session;
            m_SendFile = sendFile;

            InitUI();

            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate(object state){
                SendAudio();
            }));
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
            this.Text = "Sending audio file: " + Path.GetFileName(m_SendFile);

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

            this.Controls.Add(mt_Codec);
            this.Controls.Add(m_pCodec);
            this.Controls.Add(mt_PacketsSent);
            this.Controls.Add(m_pPacketsSent);
            this.Controls.Add(mt_KBSent);
            this.Controls.Add(m_pKBSent);
        }

        #endregion


        #region method SendAudio

        /// <summary>
        /// Sends audio to RTP session target(s).
        /// </summary>
        private void SendAudio()
        {
            try{                
                using(FileStream fs = File.OpenRead(m_SendFile)){
                    RTP_SendStream sendStream   = m_pSession.CreateSendStream();
                    byte[]         buffer       = new byte[400];
                    int            readedCount  = fs.Read(buffer,0,buffer.Length);
                    long           lastSendTime = DateTime.Now.Ticks;
                    long           packetsSent  = 0;
                    long           totalSent    = 0;
                    while(readedCount > 0){
                        if(m_pMainUI.ActiveCodec != null){
                            byte[] encodedData = m_pMainUI.ActiveCodec.Encode(buffer,0,buffer.Length);

                            // Send audio frame.
                            RTP_Packet packet = new RTP_Packet();
                            packet.Timestamp = m_pSession.RtpClock.RtpTimestamp;
                            packet.Data = encodedData;
                            sendStream.Send(packet);

                            // Read next audio frame.
                            readedCount = fs.Read(buffer,0,buffer.Length);
                            totalSent += encodedData.Length;
                            packetsSent++;

                            this.BeginInvoke(new MethodInvoker(delegate(){
                                m_pCodec.Text       = m_pMainUI.ActiveCodec.Name;
                                m_pPacketsSent.Text = packetsSent.ToString();
                                m_pKBSent.Text      = Convert.ToString(totalSent / 1000);
                            }));
                        }           

                        Thread.Sleep(25);

                        lastSendTime = DateTime.Now.Ticks;
                    }
                    sendStream.Close();
                }
            }
            catch(Exception x){
                string dummy = x.Message;
            }
        }

        #endregion
    }
}
