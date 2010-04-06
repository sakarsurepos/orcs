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
    public class wfrm_SendMic2
    {        
        private RTP_Session    m_pSession    = null;
        private AudioIn        m_pWaveIn     = null;
        private RTP_SendStream m_pSendStream = null;
        private long           m_PacketsSent = 0;
        private long           m_TotalSent   = 0;
        private bool           m_Send        = false;
        private Robot1         m_pMainUI     = null;
        private RTP_Packet     m_pRtpPacket  = null;
        public string          Codec         = "None";
        public string          PacketsSent   = "0";
        public string          KBSent        = "0";  

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="owner">Main UI.</param>
        /// <param name="session">RTP session.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>owner</b> or <b>session</b> is null reference.</exception>
        public wfrm_SendMic2(Robot1 owner, RTP_Session session, ComboBox m_pInDevices)
        {
            if(owner == null){
                throw new ArgumentNullException("owner");
            }
            if(session == null){
                throw new ArgumentNullException("session");
            }

            m_pMainUI  = owner;
            m_pSession = session;

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

        public void m_pToggleSend_Click2(ComboBox m_pInDevices, Button m_pToggleSend)  //Start Send Voice
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

        public void SendMic() //Send Audio Stream
        {
            String s;
            byte[] readBuffer = new byte[400];
            while (m_Send)
            {
                try
                {
                    // We have not enough data for RTP packet, wait more to be available.
                    if (m_pWaveIn.Available < readBuffer.Length)
                    {
                        Thread.Sleep(1);
                    }
                    else
                    {
                        m_pWaveIn.Read(readBuffer, 0, readBuffer.Length);

                        m_pRtpPacket.Data = m_pMainUI.ActiveCodec.Encode(readBuffer, 0, readBuffer.Length);
                        m_pRtpPacket.Timestamp = m_pSendStream.Session.RtpClock.RtpTimestamp;
                        m_pSendStream.Send(m_pRtpPacket);

                        m_PacketsSent++;
                        m_TotalSent += m_pRtpPacket.Data.Length;

                            Codec = m_pMainUI.ActiveCodec.Name;
                            PacketsSent = m_PacketsSent.ToString();
                            KBSent = Convert.ToString(m_TotalSent / 1000);
                            s = KBSent.ToString();
                            
                            m_pMainUI.Invoke(m_pMainUI.m_DelegateAddString, new Object[] { s });
                    }
                }
                catch
                {
                }
            }
        }

        public void wfrm_SendMic_FormClosing() //Dispose Send Audio
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



    }
}
