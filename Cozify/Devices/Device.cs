using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{
    [JsonConverter(typeof(CozifyJsonConverter))]
    public abstract class Device
    {
        public Set Capabilities { get; set; }
        public string Description { get; set; }
        //public string Type { get; set; }
        public object[] Groups { get; set; }
        public string Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public string[] Room { get; set; }
        public DeviceState State { get; set; }
        public DateTime Timestamp { get; set; }
        public string[] Zones { get; set; }


    }
    [JsonConverter(typeof(CozifyJsonConverter))]
    public abstract class DeviceState
    {
        public DateTime LastSeen { get; set; }
        public bool Reachable { get; set; }
    }
}
