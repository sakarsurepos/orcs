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
    public class wfrm_Receive : Form
    {
        private Label    mt_OutputDevice    = null;
        private ComboBox m_pOutputDevice    = null;
        private Label    mt_Codec           = null;
        private TextBox  m_pCodec           = null;
        private Label    mt_PacketsReceived = null;
        private Label    m_pPacketsReceived = null;
        private Label    mt_PacketsLost     = null;
        private Label    m_pPacketsLost     = null;
        private Label    mt_KBReceived      = null;
        private Label    m_pKBReceived      = null;

        private RTP_ReceiveStream          m_pStream = null;
        private AudioOut                   m_pPlayer = null;
        private Dictionary<int,AudioCodec> m_RtpMap  = null;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="stream">RTP receive stream.</param>
        public wfrm_Receive(RTP_ReceiveStream stream)
        {
            if(stream == null){
                throw new ArgumentNullException("stream");
            }

            // Force window handle creation.
            IntPtr handle = this.Handle;

            m_pStream = stream;
            m_pStream.PacketReceived += new EventHandler<RTP_PacketEventArgs>(m_pStream_PacketReceived);

            m_RtpMap = new Dictionary<int,AudioCodec>();
            m_RtpMap.Add(0,new PCMU());
            m_RtpMap.Add(8,new PCMA());
                        
            InitUI();
        }
        
        #region method InitUI

        /// <summary>
        /// Creates and initializes UI.
        /// </summary>
        private void InitUI()
        {
            this.Size = new Size(350,170);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Text = "Receiving from SSRC: " + m_pStream.SSRC.SSRC;

            mt_OutputDevice = new Label();
            mt_OutputDevice.Size = new Size(100,20);
            mt_OutputDevice.Location = new Point(0,10);
            mt_OutputDevice.Text = "Output device:";
            mt_OutputDevice.TextAlign = ContentAlignment.MiddleRight;

            m_pOutputDevice = new ComboBox();
            m_pOutputDevice.Size = new Size(200,20);
            m_pOutputDevice.Location = new Point(105,10);
            m_pOutputDevice.DropDownStyle = ComboBoxStyle.DropDownList;
            m_pOutputDevice.SelectedIndexChanged += new EventHandler(m_pOutputDevice_SelectedIndexChanged);
            foreach(AudioOutDevice device in AudioOut.Devices){
                m_pOutputDevice.Items.Add(device.Name);
            }
            if(m_pOutputDevice.Items.Count > 0){
                m_pOutputDevice.SelectedIndex = 0;
            }

            mt_Codec = new Label();
            mt_Codec.Size = new Size(100,20);
            mt_Codec.Location = new Point(0,35);
            mt_Codec.Text = "Codec:";
            mt_Codec.TextAlign = ContentAlignment.MiddleRight;

            m_pCodec = new TextBox();
            m_pCodec.Size = new Size(100,20);
            m_pCodec.Location = new Point(105,35);
            m_pCodec.ReadOnly = true;

            mt_PacketsReceived = new Label();
            mt_PacketsReceived.Size = new Size(100,20);
            mt_PacketsReceived.Location = new Point(0,60);
            mt_PacketsReceived.Text = "Packets received:";
            mt_PacketsReceived.TextAlign = ContentAlignment.MiddleRight;

            m_pPacketsReceived = new Label();
            m_pPacketsReceived.Size = new Size(100,20);
            m_pPacketsReceived.Location = new Point(105,60);
            m_pPacketsReceived.Text = "0";
            m_pPacketsReceived.TextAlign = ContentAlignment.MiddleLeft;

            mt_PacketsLost = new Label();
            mt_PacketsLost.Size = new Size(100,20);
            mt_PacketsLost.Location = new Point(0,80);
            mt_PacketsLost.Text = "Packets lost:";
            mt_PacketsLost.TextAlign = ContentAlignment.MiddleRight;

            m_pPacketsLost = new Label();
            m_pPacketsLost.Size = new Size(100,20);
            m_pPacketsLost.Location = new Point(105,80);
            m_pPacketsLost.Text = "0";
            m_pPacketsLost.TextAlign = ContentAlignment.MiddleLeft;

            mt_KBReceived = new Label();
            mt_KBReceived.Size = new Size(100,20);
            mt_KBReceived.Location = new Point(0,100);
            mt_KBReceived.Text = "KB received:";
            mt_KBReceived.TextAlign = ContentAlignment.MiddleRight;

            m_pKBReceived = new Label();
            m_pKBReceived.Size = new Size(100,20);
            m_pKBReceived.Location = new Point(105,100);
            m_pKBReceived.Text = "0";
            m_pKBReceived.TextAlign = ContentAlignment.MiddleLeft;

            this.Controls.Add(mt_OutputDevice);
            this.Controls.Add(m_pOutputDevice);
            this.Controls.Add(mt_Codec);
            this.Controls.Add(m_pCodec);
            this.Controls.Add(mt_PacketsReceived);
            this.Controls.Add(m_pPacketsReceived);
            this.Controls.Add(mt_PacketsLost);
            this.Controls.Add(m_pPacketsLost);
            this.Controls.Add(mt_KBReceived);
            this.Controls.Add(m_pKBReceived);

        }
                
        #endregion


        #region Events handling

        #region method m_pOutputDevice_SelectedIndexChanged

        /// <summary>
        /// This method is called when output device has changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Event data.</param>
        private void m_pOutputDevice_SelectedIndexChanged(object sender,EventArgs e)
        {
            m_pPlayer = new AudioOut(AudioOut.Devices[m_pOutputDevice.SelectedIndex],8000,16,1);
        }

        #endregion

        #region method m_pStream_PacketReceived

        /// <summary>
        /// This method is called when new RTP packets recieved.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">Eevent data.</param>
        private void m_pStream_PacketReceived(object sender,RTP_PacketEventArgs e)
        {
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
            
                this.BeginInvoke(new MethodInvoker(delegate(){
                    m_pCodec.Text = codec != null ? codec.Name : "unknown";
                    m_pPacketsReceived.Text = m_pStream.PacketsReceived.ToString();
                    m_pKBReceived.Text = (m_pStream.BytesReceived / 1000).ToString("n2");
                }));
            }
            catch{
                // Decoding or player error, skip it.
            }
        }

        #endregion

        #endregion

    }
}
