using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{

    public abstract class DeviceCommand : Command
    {
        [JsonProperty("id")]
        public string DeviceId { get; set; }
    }
    [TypeName("CMD_DEVICE_ON")]
    public class DeviceOnCommand : DeviceCommand
    {
    }
    [TypeName("CMD_DEVICE_OFF")]
    public class DeviceOffCommand : DeviceCommand
    {
    }
    [TypeName("CMD_DEVICE")]
    public class DeviceChangeStateCommand : DeviceCommand
    {
        public DeviceState State { get; set; }
    }
}
