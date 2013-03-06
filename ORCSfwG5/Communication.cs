using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;


namespace Robot
{
    class Communication
    {
        TcpClient                 Client;
        TcpListener               Server;
        NetworkStream             strm_1          ,strm_2;
        Socket                    soc_1;
        Thread                    Client_Thread;
        Thread                    Server_Thread;
        ASCIIEncoding             Encode_1        ,Encode_2;
        ASCIIEncoding             Encode_3        ,Encode_4;

        public delegate void      ClientDelegate();
        public delegate void      ServerDelegate();
        public event              ClientDelegate  ClientDataReceived;
        public event              ServerDelegate  ServerDataReceived;

        public string             data_from_client;
        public string             data_from_server;
        
        
        public Communication()
        {            
        }

        //------------------------------------CLIENT-------------------------------------------
        
        
        public void TCP_Client_Start(string IP_address, int port_number)
        {
            try
            {   
                Client          = new TcpClient ();
                Client.Connect(IP_address, port_number);
                
                Client_Thread   = new Thread(new ThreadStart(TCP_Client_Loop));
                Client_Thread.Start();
                
            }
            catch 
            {
            MessageBox.Show("TCP_Client_Start Problem");
            }           
        }

        public void TCP_Client_Stop()
        {
            Client_Thread.Interrupt();
            Client_Thread.Abort();
            Client.Close();
        }

        private void TCP_Client_Loop()
        {
            byte[] data = new byte[256];
            Encode_1        = new ASCIIEncoding();
            strm_1          = Client.GetStream();

            while (true)
            {
                try
                {
                    int bytes           = strm_1.Read(data, 0, data.Length);
                    data_from_client    = Encode_1.GetString(data, 0, bytes);
                   
                    if (ClientDataReceived != null)
                    {
                        ClientDataReceived();
                    }
                }
                catch
                {
                    //MessageBox.Show("TCP_Client_Loop Problem");
                }
            }
        }
        public void Send_Data_By_Client(string data_for_send_by_Client)
        {
            byte[] data = new byte[256];
            strm_2          = Client.GetStream();
            Encode_2        = new ASCIIEncoding();
            data            = Encode_2.GetBytes(data_for_send_by_Client);
            
            strm_2.Write(data, 0, data.Length);
        }
        
        public bool ClientConnectingStatus()
        {
            return Client.Connected;
        }
        
        //-----------------------------------------SERVER-----------------------------------------

        public void TCP_Server_Start(string IPAddr ,int port_number)
        {
            try
            {
                IPAddress ipAd  = IPAddress.Parse(IPAddr);
                Server          = new TcpListener(ipAd,port_number);
                Server.Start();

                Server_Thread   = new Thread(new ThreadStart(TCP_Server_Loop));
                Server_Thread.Start();
            }
            catch
            {
            }
        }
        
        private void TCP_Server_Loop()
        {
            soc_1           = Server.AcceptSocket();
            byte[] data = new byte[256];
            Encode_3        = new ASCIIEncoding();
            
            while (true)
            {
                try
                {
                    int bytes           = soc_1.Receive(data);
                    data_from_server    = Encode_3.GetString(data, 0, bytes);
                    
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
            Encode_4            = new ASCIIEncoding();
            data                = Encode_4.GetBytes(data_for_send_by_Server);
            soc_1.Send(data);
        }

        public void TCP_Server_Stop()
        {
            Server.Stop();
            Server_Thread.Abort();
            
        }
                            
    }
}
