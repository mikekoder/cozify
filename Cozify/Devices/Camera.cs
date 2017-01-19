using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{
    [TypeName("CAMERA")]
    public class Camera : Device
    {
        
    }
    [TypeName("STATE_CAMERA")]
    public class CameraState : DeviceState
    {
        public string ConnectionState { get; set; }
        public bool IsOn { get; set; }
        public string Media { get; set; }
        public bool Recording { get; set; }
        public int ViewerCount { get; set; }
    }
}
