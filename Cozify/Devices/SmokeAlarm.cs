using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{
    [TypeName("SMOKE_ALARM")]
    public class SmokeAlarm : Device
    {
    }
    [TypeName("STATE_SMOKE_ALARM")]
    public class SmokeAlarmState : DeviceState
    {
        public bool Alert { get; set; }
        public DateTime AlertAt { get; set; }
    }
}
