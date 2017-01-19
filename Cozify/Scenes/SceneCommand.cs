using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Devices
{

    public abstract class SceneCommand : Command
    {
        [JsonProperty("id")]
        public string SceneId { get; set; }
    }
    [TypeName("CMD_SCENE_ON")]
    public class SceneOnCommand : SceneCommand
    {
    }
    [TypeName("CMD_SCENE_OFF")]
    public class SceneOffCommand : SceneCommand
    {
    }
}
