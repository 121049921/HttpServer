using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{

    public class JsonResult : ActionResult
    {
        public JsonResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.DenyGet;
        }

        public JsonRequestBehavior JsonRequestBehavior { get; set; }

        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; } = "application/json";

        public object Data { get; set; }

        public override void ExecuteResult(HttpListenerResponse response, RouteData routeData)
        {
            string json = JsonUitl.ToJson(Data);
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

    public enum JsonRequestBehavior
    {
        AllowGet,
        DenyGet,
    }
}
