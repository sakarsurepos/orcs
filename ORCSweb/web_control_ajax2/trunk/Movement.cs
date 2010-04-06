using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Robot
{
    class Movement
    {
        //----------------------------------Basic movements---------------------------------

        public Movement()
        {
        }

        Messages message = new Messages();
        Thread TimeThread;
        int delay;

        public string Motion_Forward_Backward(int value)
        {
            string outputMessage = null;
            int correction;

                if (value < 63)
                {
                    correction = 63 - value;
                    outputMessage = message.MotionBackward(correction);
                }
                else if (value > 63)
                {
                    correction = value - 63;
                    outputMessage = message.MotionForward(correction);
                }
                else if (value == 63)
                {
                    outputMessage = message.MotionStop();
                }

            return outputMessage;
        }

        public string Motion_Forward_Backward(int value,int time)
        {
            string outputMessage = null;
            int correction;

            if (value < 63)
            {
                correction = 63 - value;
                outputMessage = message.MotionBackward(correction);
            }
            else if (value > 63)
            {
                correction = value - 63;
                outputMessage = message.MotionForward(correction);
            }
            else if (value == 63)
            {
                outputMessage = message.MotionStop();
            }
            TimeThread = new Thread(new ThreadStart(TimeThreadFcn));
            TimeThread.Start();
            delay = time;
            return outputMessage;
        }

        private void TimeThreadFcn()
        {
            TimeThread.Join(delay);
            Stop_Motion();
            TimeThread.Abort();
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
