using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HttpMvc
{
    internal class XmlResult : ActionResult
    {
        public override void ExecuteResult(HttpListenerResponse response, RouteData routeData)
        {
            throw new NotImplementedException();
        }
    }
}
