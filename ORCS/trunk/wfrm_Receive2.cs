using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using LumiSoft.Net.RTP;
using LumiSoft.Net.Media;
using LumiSoft.Net.Media.Codec.Audio;

namespace Robot
{
    /// <summary>
    /// Audio receiver window.
    /// </summary>
    public class wfrm_Receive2
    {
        private RTP_ReceiveStream          m_pStream = null;
        private AudioOut                   m_pPlayer = null;
        private Dictionary<int,AudioCodec> m_RtpMap  = null;
        public string Codec = "None";
        public string PacketsReceived = "0";
        public string KBReceived = "0";
        private Robot1 m_pMainUI = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="stream">RTP receive stream.</param>
        public wfrm_Receive2(RTP_ReceiveStream stream, Robot1 owner)
        {
            if(stream == null){
                throw new ArgumentNullException("stream");
            }
            if (owner == null)
            {
                throw new ArgumentNullException("owner");
            }

            m_pMainUI = owner;

            // Force window handle creation.
            ////////////////////////////////IntPtr handle = this.Handle;

            m_pStream = stream;
            m_pStream.PacketReceived += new EventHandler<RTP_PacketEventArgs>(m_pStream_PacketReceived);

            m_RtpMap = new Dictionary<int,AudioCodec>();
            m_RtpMap.Add(0,new PCMU());
            m_RtpMap.Add(8,new PCMA());
        }

        /// <summary>
        /// This method is called when new RTP packets recieved.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Eevent data.</param>
        private void m_pStream_PacketReceived(object sender,RTP_PacketEventArgs e)
        {
            String s1;
            try{
                AudioCodec codec = null;
                m_RtpMap.TryGetValue(e.Packet.PayloadType,out codec);
                if(codec != null){
                    byte[] decodedData = codec.Decode(e.Packet.Data,0,e.Packet.Data.Length);
                    m_pPlayer.Write(decodedData,0,decodedData.Length);
                }
                // Unknown RTP payload.
                //else{

                // Record if recording enabled.
                // if(m_pRecordStream != null){
                //       m_pRecordStream.Write(decodedData,0,decodedData.Length);
                //}
            
                //this.BeginInvoke(new MethodInvoker(delegate(){
                //    m_pCodec.Text = codec != null ? codec.Name : "unknown";
                //    m_pPacketsReceived.Text = m_pStream.PacketsReceived.ToString();
                //    m_pKBReceived.Text = (m_pStream.BytesReceived / 1000).ToString("n2");
                //}));
                
                Codec = codec != null ? codec.Name : "unknown";
                PacketsReceived = m_pStream.PacketsReceived.ToString();
                KBReceived = (m_pStream.BytesReceived / 1000).ToString("n2");
                s1 = KBReceived.ToString();

                m_pMainUI.Invoke(m_pMainUI.m_DelegateAddString1, new Object[] { s1 });
            }
            catch{
                // Decoding or player error, skip it.
            }
        }


        
    }
}
