using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{
    [TypeName("ALARM_DEVICE")]
    public class AlarmDevice : Device
    {
    }
    [TypeName("STATE_ALARM")]
    public class AlarmState : DeviceState
    {
        public bool IsOn { get; set; }
    }
}
