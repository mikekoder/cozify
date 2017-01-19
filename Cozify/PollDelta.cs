using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify
{
    [JsonConverter(typeof(CozifyJsonConverter))]
    [TypeName("POLL_DELTA")]
    public class PollDelta
    {
        public bool Full { get; set; }
        public Poll[] Polls { get; set; }
    }
}
