using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify
{
    [JsonConverter(typeof(CozifyJsonConverter))]
    public abstract class Poll
    {
        public DateTime Timestamp { get; set; }
    }
}
