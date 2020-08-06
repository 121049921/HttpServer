using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace HttpMvc
{
    public class UrlRoutingModule : IHttpModule
    {

        public UrlRoutingModule() { }
        private RouteConlection routeConlection;
        public RouteConlection RouteConlection
        {
            get
            {
                if (routeConlection == null)
                {
                    routeConlection = new RouteConlection();
                }
                else
                {
                    routeConlection = RoutTable.Routes;
                }
                return routeConlection;
            }
        }


        public RouteData BeginInvoke(HttpListenerContext context, string rootPath)
        {

            // object result = new object();
            RouteData routeData = RouteConlection.GetRouteData(context);

            if (routeData.RouteValue != null && routeData.RouteValue.Count > 0)
            {
                // Cache<T>//未实现,这里要从缓存中取,提升效率
                string controllerName = routeData.RouteValue[nameof(RouteModel.Controller)].ToString();

                //1.创建控制器
                IControllerFactory controllerFactory = new ControllerFactory();
                routeData = controllerFactory.CreateController(routeData, controllerName, rootPath, RouteConlection.Route);
                Type controllerType = (Type)routeData.RouteValue[nameof(RouteModel.NamespaceClass)];

                if (controllerType != null)
                {
                    BaseController baseController = new BaseController();
                    baseController.RouteData = routeData;
                    baseController.ControllerType = controllerType;

                    //这里可以接管注入
                    //2.执行方法
                    //a.执行之前
                    baseController.OnActionExecuting(routeData, controllerType);

                    IActionExecute action = new ActionExecute();
                    routeData = action.ExecuteMethod(routeData, controllerType);

                    //b.执行之后

                    baseController.OnActionExecuted(routeData, controllerType);
                }
                else
                {
                    ErrorResult result = new ErrorResult()
                    {
                        Code = 500,
                        Msg = "找不到" + controllerName + "控制器",
                    };
                    routeData.RouteValue[nameof(ActionResult)] = result;
                }
            }
            else
            {
                ErrorResult result = new ErrorResult()
                {
                    Code = 500,
                    Msg = "找不到控制器",
                };
                routeData.RouteValue[nameof(ActionResult)] = result;
            }

            return routeData;
        }
    }
}
