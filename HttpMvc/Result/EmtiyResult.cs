using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public  class EmtiyResult : ActionResult
    {
        public string Content { get; set; }
        public Encoding ContentEncoding { get; set; } = Encoding.UTF8;
        public string ContentType { get; set; }
        public override void ExecuteResult(HttpListenerResponse response, RouteData routeData)
        {

            if (!string.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "text/plain";
            }
            string text = string.Empty;
            response.SendChunked = true;
            response.ContentEncoding = ContentEncoding;
            response.StatusCode = 200;
            StreamWriter writer = new StreamWriter(response.OutputStream, Encoding.UTF8);
            writer.WriteLine(text);
            writer.Flush();
            writer.Close();
            response.Close();
        }
    }
}
