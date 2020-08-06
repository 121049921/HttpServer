using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{


    public class BaseController
    {
        public RouteData RouteData { get; set; }
        public Type ControllerType { get; set; }


        protected virtual JsonResult Json(object data, JsonRequestBehavior jsonBehavior)
        {
            JsonResult result = new JsonResult()
            {
                Data = data,
                JsonRequestBehavior = jsonBehavior
            };
            return result;
        }
        protected virtual JsonResult Json(object data)
        {
            JsonResult result = new JsonResult()
            {
                Data = data,
            };
            return result;
        }
        protected virtual ContentResult Content(string content, string contentType, Encoding contentEncoding)
        {

            ContentResult result = new ContentResult()
            {
                Content = content,
                ContentType = contentType,
                ContentEncoding = contentEncoding
            };
            return result;
        }

        protected virtual EmtiyResult Emtiy(object @object)
        {
            EmtiyResult result = new EmtiyResult() { };
            return result;

        }
        protected virtual DownFileResult DownFile(string path)
        {
            return new DownFileResult()
            {
                FilePath = path,
            };

        }

        protected virtual ViewResult View(string viewPath = "")
        {
            return new ViewResult() { viewPath = viewPath };

        }


        public virtual void OnActionExecuting(RouteData routeData, Type controllerType)
        {
            //ToDO

        }
        public virtual void OnActionExecuted(RouteData routeData, Type controllerType)
        {
            //ToDO
        }

    }
}
