using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{
    [TypeName("DOORBELL")]
    public class DoorBell : Device
    {
        
    }

    [TypeName("STATE_DOORBELL")]
    public class DoorBellState : DeviceState
    {
        public bool Alert { get; set; }
    }
}
