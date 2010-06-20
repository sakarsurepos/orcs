using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Robot
{
    public class Communication
    {
        public Communication()
        {
        }
        public TcpClient Client1; //static public
        public TcpListener Server;
        public NetworkStream strm_1, strm_2;
        public Socket soc_1, soc_2;
        public Thread Client_Thread; //static public
        public Thread Server_Thread;
        ASCIIEncoding Encode_1, Encode_2;
        ASCIIEncoding Encode_3, Encode_4;

        public delegate void ClientDelegate();
        public delegate void ServerDelegate();
        public event ClientDelegate ClientDataReceived;
        public event ServerDelegate ServerDataReceived;

        public string data_from_client;
        public string data_from_server;

        //------------------------------------CLIENT-------------------------------------------

        public void TCP_Client_Start(string IP_address, int port_number)
        {
                Client1 = new TcpClient();
                Client1.Connect(IP_address, port_number);
                Client_Thread = new Thread(new ThreadStart(TCP_Client_Loop));
                Client_Thread.Start();
        }

        public void TCP_Client_Stop()
        {
                Client_Thread.Abort();
                Client1.Close();
        }

        private void TCP_Client_Loop()
        {
            try
            {

                byte[] data = new byte[256];
                Encode_1 = new ASCIIEncoding();
                strm_1 = Client1.GetStream();

                while (true)
                {

                    int bytes = strm_1.Read(data, 0, data.Length);
                    data_from_client = Encode_1.GetString(data, 0, bytes);



                    if (ClientDataReceived != null)
                    {
                        ClientDataReceived();
                    }
                }
            }
            catch
            {
                //TCP_Client_Stop();
                //Client1.Close();
            }
            finally
            {
               // TCP_Client_Stop();
               // Client1.Close();
            }
        }
        public void Send_Data_By_Client(string data_for_send_by_Client)
        {
            byte[] data = new byte[256];
            try
            {
                strm_2 = Client1.GetStream();
                Encode_2 = new ASCIIEncoding();
                data = Encode_2.GetBytes(data_for_send_by_Client);
                strm_2.Write(data, 0, data.Length);
            }
            catch
            {
                //TCP_Client_Stop();
                //Client1.Close();
            }
            finally
            {
             //   TCP_Client_Stop();
             //   Client1.Close();
            }
        }

        public bool ClientConnectingStatus()
        {
            bool status = true;

                if (Client1.Connected)
                    status = true;
                else
                    status = false;

            return status;
        }

        //-----------------------------------------SERVER-----------------------------------------

        public void TCP_Server_Start(string IP_Address, int port_number)
        {
            try
            {
                IPAddress ipAd = IPAddress.Parse(IP_Address);
                Server = new TcpListener(ipAd, port_number);
                Server.Start();

                Server_Thread = new Thread(new ThreadStart(TCP_Server_Loop));
                Server_Thread.Start();

            }
            catch
            {
            }
        }

        private void TCP_Server_Loop()
        {
            soc_1 = Server.AcceptSocket();
            byte[] data = new byte[256];
            Encode_3 = new ASCIIEncoding();


            while (true)
            {
                try
                {
                    int bytes = soc_1.Receive(data);
                    data_from_server = Encode_3.GetString(data, 0, bytes);

                    if (ServerDataReceived != null)
                    {

                        ServerDataReceived();
                    }
                }
                catch
                {
                }
            }
        }
        public void Send_Data_By_Server(string data_for_send_by_Server)
        {
            byte[] data = new byte[256];
            soc_2 = Server.AcceptSocket();

            Encode_4 = new ASCIIEncoding();
            data = Encode_4.GetBytes(data_for_send_by_Server);
            soc_2.Send(data);
        }

        public void TCP_Server_Stop()
        {
            //Server_Thread.Abort();
            //Server.Stop();
        }

        public bool ServerConnectingStatus()
        {
            bool status;

            if (Client1.Connected)
                status = true;

            else
                status = false;

            return status;
        }

        ~Communication()
        {
            //Client1.Close();
        }
    }
}
