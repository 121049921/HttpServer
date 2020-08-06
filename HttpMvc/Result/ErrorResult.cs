using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public sealed class ErrorResult : ActionResult
    {

        public int Code = 500;
        public string Msg { get; set; }
        public object Data { get; set; }
        public override void ExecuteResult(HttpListenerResponse response, RouteData routeData)
        {
            var obj = new { Code,Msg,Data };
            string json = JsonUitl.ToJson(obj);
            response.SendChunked = true;
            response.ContentEncoding = Encoding.UTF8;
            response.StatusCode = 200;
            response.ContentType = "application/json";
            StreamWriter writer = new StreamWriter(response.OutputStream, Encoding.UTF8);
            writer.WriteLine(json);
            writer.Flush();
            writer.Close();
            response.Close();
        }
    }
}
