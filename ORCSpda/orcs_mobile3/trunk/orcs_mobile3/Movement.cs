using System;
using System.Collections.Generic;
using System.Text;

namespace orcs_mobile3
{
    class Movement
    {
        //----------------------------------Basic movements---------------------------------

        public Movement()
        {
        }

        Messages message            = new Messages();

        public string Motion_Foreward_Backward(int value)
        {
            string outputMessage    = null;
            int correction;

                if (value < 63)
                {
                    correction      = 63 - value;
                    outputMessage   = message.MotionBackward(correction);
                }
                else if (value > 63)
                {
                    correction      = value - 63;
                    outputMessage   = message.MotionForeward(correction);
                }
                else if (value == 63)
                {
                    outputMessage   = message.MotionStop();
                }
            
            return outputMessage;
        }

        public string Stop_Motion()
        {
            return message.MotionStop();
        }

        public string Direction_Right_Left(int value)
        {
            return message.Direction(value);
        }

        //----------------------------------Camera movements---------------------------------

        public string CAM_Up_Down(int value)
        {
            return message.CameraVertical(value);
        }

        public string CAM_Left_Right(int value)
        {
            return message.CameraHorizontal(value);
        }

    }
}
