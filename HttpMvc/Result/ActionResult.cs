using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{


    public abstract class ActionResult
    {
        public abstract void ExecuteResult(HttpListenerResponse response, RouteData routeData);

    }
}
