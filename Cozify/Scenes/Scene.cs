using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify.Scenes
{
    [TypeName("SCENE")]
    public class Scene
    {
        public string Category { get; set; }
        public string[] ExcludedIds { get; set; }
        public string Icon { get; set; }
        public string Id { get; set; }
        public bool IsOn { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
