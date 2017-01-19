using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{
    [TypeName("DEVICE_DELTA")]
    public class DeviceDelta : Poll
    {
        public Dictionary<string, Device> Devices { get; set; }
    }
}
