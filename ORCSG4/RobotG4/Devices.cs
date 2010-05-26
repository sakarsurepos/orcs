using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    class Devices
    {
        public Devices()
        {
        }

        Messages message = new Messages();

        public string GPS_ON_OFF(bool check)
        {
            string outputMessage = null;

                if (check)
                {
                    outputMessage = message.GPS_ON();
                }
                else
                {
                    outputMessage = message.GPS_OFF();
                }
                
            return outputMessage;
        }
                
        public string Direction_Servo_ON_OFF(bool check)
        {
            string outputMessage = null;

                if (check)
                {
                    outputMessage = message.DirectionServo_ON();
                }
                else
                {
                    outputMessage = message.DirectionServo_OFF();
                }
            
            return outputMessage;
        }

        public string LASER_ON_OFF(bool check)
        {
            string outputMessage = null;

            if (check)
            {
                outputMessage = message.LASER_ON();
            }
            else
            {
                outputMessage = message.LASER_OFF();
            }

            return outputMessage;
        }

        public string LIGHTS_ON_OFF(bool check)
        {
            string outputMessage = null;

            if (check)
            {
                outputMessage = message.LIGHTS_ON();
            }
            else
            {
                outputMessage = message.LIGHTS_OFF();
            }

            return outputMessage;
        }
        
    }
}