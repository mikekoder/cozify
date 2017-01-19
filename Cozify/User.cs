using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cozify
{
    [TypeName("USER")]
    public class User
    {
        public string NickName { get; set; }
        public bool Defaults { get; set; }
        public string Phone { get; set; }
        public string Uid { get; set; }
        public string Email { get; set; }
    }
}
