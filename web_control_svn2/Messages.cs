using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    class Messages
    {
        public Messages()
        {
        }

        private string Control_Chars_Send = "$A7";
        private string End_Bytes = Convert.ToChar(13).ToString() + Convert.ToChar(10).ToString();   //<CR><LF>

        public string MotionForward(int value)
        {
            string data  = Convert.ToChar(value).ToString();
            return Control_Chars_Send + "M!" + data + End_Bytes;
        }

        public string MotionBackward(int value)
        {
            string data = Convert.ToChar(value).ToString();
            return Control_Chars_Send + "M%" + data + End_Bytes;
        }

        public string MotionStop()
        {
            return Control_Chars_Send + "M)_" + End_Bytes;
        }

        public string Direction(int value)
        {
            string data = Convert.ToChar(127-value).ToString();
            return Control_Chars_Send + "DX" + data + End_Bytes;
        }

        public string GPS_ON()
        {
            return Control_Chars_Send + "G1_" + End_Bytes;
        }

        public string GPS_OFF()
        {
            return Control_Chars_Send + "G0_" + End_Bytes;
        }

        public string CameraVertical(int value)
        {
            string data = Convert.ToChar(127-value).ToString();
            return Control_Chars_Send + "C1" + data + End_Bytes;
        }

        public string CameraHorizontal(int value)
        {
            string data = Convert.ToChar(127-value).ToString();
            return Control_Chars_Send + "C2" + data + End_Bytes;
        }

        public string CameraServos_ON()
        {
            return Control_Chars_Send + "C31" + End_Bytes;
        }

        public string CameraServos_OFF()
        {
            return Control_Chars_Send + "C30" + End_Bytes;
        }

        public string DirectionServo_ON()
        {
            return Control_Chars_Send + "D1_" + End_Bytes;
        }

        public string DirectionServo_OFF()
        {
            return Control_Chars_Send + "D0_" + End_Bytes;
        }
        
        public string CAMERA_ON()
        {
            return Control_Chars_Send + "C51" + End_Bytes;
        }

        public string CAMERA_OFF()
        {
            return Control_Chars_Send + "C50" + End_Bytes;
        }

        public string LIGHTS_ON()
        {
            return Control_Chars_Send + "C61" + End_Bytes;
        }

        public string LIGHTS_OFF()
        {
            return Control_Chars_Send + "C60" + End_Bytes;
        }

        public string LASER_ON()
        {
            return Control_Chars_Send + "C71" + End_Bytes;
        }

        public string LASER_OFF()
        {
            return Control_Chars_Send + "C70" + End_Bytes;
        }

    }
}
