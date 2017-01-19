using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{
    [TypeName("POWER_SOCKET")]
    public class PowerSocket : Device
    {
    }
    [TypeName("STATE_SOCKET")]
    public class PowerSocketState : DeviceState
    {
        public decimal? Brightness { get; set; }
        public bool IsOn { get; set; }
        public DateTime LastChange { get; set; }
    }
}
