using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Hubs
{
    public class Hub
    {
        [JsonProperty("hubId")]
        public string Id { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }
        public bool Connected { get; set; }
        public string State { get; set; }
    }
}
