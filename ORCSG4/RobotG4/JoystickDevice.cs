using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using System.Threading;


namespace Robot
{
    class JoystickDevice
    {
        Device              Joystick;
        Thread                  JoystickThread;
        
        public delegate void    JoystickDelagate();
        public event            JoystickDelagate JoystickEvent;
        
        public bool             joystickStatus;
        public int              directionValue;
        public int              motionValue;
        public int              cameraHorizontalValue;
        public int              cameraVerticalValue;

        public void Init_Joystick_Device()
        {

            //create joystick device.
            foreach (DeviceInstance di in Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly))
            {
                Joystick        = new Device(di.InstanceGuid);
                break;
            }

            if (Joystick != null)
            {
                joystickStatus  = true;
            }

            //Set joystick axis ranges.
            foreach (DeviceObjectInstance doi in Joystick.Objects)
            {
                if ((doi.ObjectId & (int)DeviceObjectTypeFlags.Axis) != 0)
                {
                    Joystick.Properties.SetRange(ParameterHow.ById, doi.ObjectId, new InputRange(1, 126));
                }
            }

            //Set joystick axis mode absolute.
            Joystick.Properties.AxisModeAbsolute = true;
            //Joystick.SetCooperativeLevel(this, CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Background);

            //Acquire devices for capturing.
            Joystick.Acquire();
            
        }

        public void Joystick_START()
        {
            JoystickThread = new Thread(new ThreadStart(Update_Joystick_Device));
            JoystickThread.Start();
        }

        public void Joystick_STOP()
        {
            Joystick.Unacquire();
            JoystickThread.Abort();
        }

        public string JoystickName()
        {
            return Joystick.DeviceInformation.ProductName.ToString();
        }

        private void Update_Joystick_Device()
        {
            while (true)
            {
                JoystickState state = Joystick.CurrentJoystickState;
                try
                {
                    JoystickThread.Join(10);        // Pause 10ms
                    
                    byte[]  buttons         = state.GetButtons();
                    int[]   slider1         = state.GetSlider();
                    int[]   Slid_Up_Down    = state.GetSlider();

                    directionValue          = state.X;
                    motionValue             = 127 - state.Y;
                    cameraHorizontalValue   = state.Rz;
                    cameraVerticalValue     = Slid_Up_Down[0];

                    if (JoystickEvent != null)
                    {
                        JoystickEvent();
                    }
                }
                catch
                {
                }

            }
        }
    }
}
