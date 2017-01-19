using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{
    [TypeName("MOTION")]
    public class MotionSensor : Device
    {
        
    }
    [TypeName("STATE_MOTION")]
    public class MotionSensorState : DeviceState
    {
        public DateTime LastMotion { get; set; }
        public bool Motion { get; set; }
    }
}
