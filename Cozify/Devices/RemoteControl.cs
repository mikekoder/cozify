using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{
    [TypeName("REMOTE_CONTROL")]
    public class RemoteControl : Device
    {
    }
    [TypeName("STATE_RC")]
    public class RemoteControlState : DeviceState
    {
        public int? Button { get; set; }
    }
}
