using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify
{
    public class TypeNameAttribute : Attribute
    {
        public string Type { get; set; }
        public TypeNameAttribute(string type)
        {
            this.Type = type;
        }
    }
}
