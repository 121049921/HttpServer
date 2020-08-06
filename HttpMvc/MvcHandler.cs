using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpMvc
{
    public class MvcHandler : IHttpHandler
    {
        public MvcHandler() { }
        private HttpListenerContext Context { get; set; }
        private HttpListenerRequest Request { get; set; }

        private HttpListenerResponse response;
        public HttpListenerResponse Response { get => response; set => response = value; }

        private RouteData routeData;
        public RouteData RouteData { get => routeData; set => routeData = value; }

       

        //1.处理请求
        public void ProcessReqeust(HttpListenerContext context, string rootPath)
        {

            Context = context;
            Request = context.Request;
            Response = context.Response;
            UrlRoutingModule urlRoutingModule = new UrlRoutingModule();
            var routeConlection = urlRoutingModule.RouteConlection;
            RouteData = urlRoutingModule.BeginInvoke(context, rootPath);
        }
        //2.解析请求
        public void ProcessResponse()
        {

            ActionResult result = (ActionResult)RouteData.RouteValue[nameof(ActionResult)];

            //  RouteData传进去主要是为了解决ViewResult
            result.ExecuteResult(response, routeData);
           
        }

    }
}
