using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cozify
{
    public class Response
    {
        public bool Success { get; set; }
        public HttpStatusCode Status { get; set; }
    }
    public class Response<T> : Response
    {   
        public T Data { get; set; }
    }
}
