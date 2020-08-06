using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public interface IHttpModule
    {
        RouteData BeginInvoke(HttpListenerContext context, string rootPath);
    }
}
