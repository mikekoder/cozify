using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Scenes
{
    [TypeName("SCENE_DELTA")]
    public class SceneDelta : Poll
    {
        public Dictionary<string,Scene> Scenes { get; set; }
    }
}
