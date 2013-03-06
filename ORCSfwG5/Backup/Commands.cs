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
    class Commands
    {
        //----------------------------------Basic Commands---------------------------------

        public Commands()
        {
        }

        Movement movement = new Movement();
        Thread Time_Thread;
        public int time;
        public bool command_flag = false;      
        
        public void Command_Forward_Backward(int speed, int times, ref Communication TcpClnt)
        {
            TcpClnt.Send_Data_By_Client(movement.Motion_Foreward_Backward(speed));
            time = times;
            Time_Thread = new Thread(new ParameterizedThreadStart(Time_Thread_Fcn));
            Time_Thread.Start(TcpClnt);
        }

        public void Command_Direction_Right_Left(int speed, int angle, int times, ref Communication TcpClnt)
        {
            TcpClnt.Send_Data_By_Client(movement.Direction_Right_Left(angle));
            TcpClnt.Send_Data_By_Client(movement.Motion_Foreward_Backward(speed));
            time = times;
            Time_Thread = new Thread(new ParameterizedThreadStart(Time_Thread_Fcn));
            Time_Thread.Start(TcpClnt);

        }

        public void Command_Stop(ref Communication TcpClnt)
        {
            TcpClnt.Send_Data_By_Client(movement.Motion_Foreward_Backward(63));
            TcpClnt.Send_Data_By_Client(movement.Direction_Right_Left(63));
        }

        public void Time_Thread_Fcn(object TcpClntobj)
        {
            command_flag = true; //set active command
            Thread.Sleep(time);
            Communication tcp = (Communication)TcpClntobj;
            tcp.Send_Data_By_Client(movement.Motion_Foreward_Backward(63));
            tcp.Send_Data_By_Client(movement.Direction_Right_Left(63));
            command_flag = false;
            Time_Thread.Abort();
            
        }
    }
}
