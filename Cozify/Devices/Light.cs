using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{
    [TypeName("LIGHT")]
    public class Light : Device
    {
    }
    [TypeName("STATE_LIGHT")]
    public class LightState : DeviceState
    {
        public decimal? Brightness { get; set; }
        public string ColorMode { get; set; }
        public decimal? Hue { get; set; }
        public bool IsOn { get; set; }
        public DateTime LastChange { get; set; }
        public decimal? MaxTemperature { get; set; }
        public decimal? MinTemperature { get; set; }
        public decimal? Saturation { get; set; }
        public decimal? Temperature { get; set; }
        public int? TransitionMsec { get; set; }
    }
}
