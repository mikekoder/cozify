using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{
    [TypeName("UI_DEVICE")]
    public class UIDevice : Device
    {
    }

    [TypeName("STATE_UI_DEVICE")]
    public class UIDeviceState : DeviceState
    {
        public bool Session { get; set; }
        public string User { get; set; }
    }
}
