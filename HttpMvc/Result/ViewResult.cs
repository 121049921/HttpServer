using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
namespace HttpMvc
{
    public class ViewResult : ActionResult
    {
        public string Content { get; set; }
        public Encoding ContentEncoding { get; set; } = Encoding.UTF8;
        public string ContentType { get; set; } = "text/html";
        private string actionName;
        public object Data { get; set; }

        public string viewPath;



        public override void ExecuteResult(HttpListenerResponse response, RouteData routeData)
        {
            // 当前View下Action同名的html或者cshtml 
            var controllerName = routeData.RouteValue[nameof(RouteModel.Controller)];
            actionName = (string)routeData.RouteValue[nameof(RouteModel.Action)];

            string html = string.Empty;
            var pagePostfix = "html,aspx,cshtml";
            List<string> htmlPageType = new List<string>();
            pagePostfix.Split(',').ToList().ForEach(postfix =>
            {
                string tempHtmlPage = $"View/{actionName}.{postfix}";
                htmlPageType.Add(tempHtmlPage);
            });
            var extDir = AppDomain.CurrentDomain.BaseDirectory;

            string htmlPath = string.Empty;
            foreach (var item in htmlPageType)
            {
                htmlPath = Path.Combine(extDir, item);
                if (File.Exists(htmlPath))
                {
                    break;
                }
            }
            if (!string.IsNullOrEmpty(htmlPath))
            {
                html = File.ReadAllText(htmlPath, Encoding.UTF8);
            }
            response.ContentType = ContentType;
            response.SendChunked = true;
            response.ContentEncoding = ContentEncoding;
            response.StatusCode = 200;
            StreamWriter writer = new StreamWriter(response.OutputStream, Encoding.UTF8);
            writer.WriteLine(html);
            writer.Flush();
            writer.Close();
            response.Close();
        }
    }
}
