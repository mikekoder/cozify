using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{
    [TypeName("CONTACT")]
    public class ReedSwitch : Device
    {
    }
    [TypeName("STATE_CONTACT")]
    public class ReedSwitchState : DeviceState
    {
        public DateTime? LastChange { get; set; }
        public bool Open { get; set; }
    }
}
