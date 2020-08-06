using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace HttpMvc
{
    public class ControllerFactory : IControllerFactory
    {
        public Type DefaulteController(string contorllerName, string rootPath, Route route)
        {
            throw new NotImplementedException();
        }
        public RouteData CreateController(RouteData routData, string controllerName, string rootPath, Route route)
        {
            Type controllerType = null;
            if (!string.IsNullOrEmpty(controllerName))
            {
                //1在配置多个dll中 多个dll中或者有同名的方法 例如: aa.dll中有Index方法,bb.dll中也有Index方法,这里没有特别处理,MVC中处理很要在AreaHandler处理
                List<Dictionary<string, object>> defaultDicRouteList = route.DicRouteList;
                foreach (var defaultDicRoute in defaultDicRouteList)
                {
                    object dllName = defaultDicRoute[nameof(RouteModel.DllName)];
                    if (dllName != null)
                    {
                        string dllFullPath = Path.Combine(rootPath, dllName.ToString());
                        if (File.Exists(dllFullPath))
                        {
                            Assembly assembly = Assembly.LoadFile(dllFullPath);
                            string tempDllName = dllName.ToString().Contains(".dll") ? dllName.ToString().Replace(".dll", "").TrimEnd() : dllName.ToString();
                            if (!controllerName.Contains(nameof(RouteModel.Controller)))
                            {
                                controllerName = string.Concat(controllerName, nameof(RouteModel.Controller));
                            }
                            string className = string.Concat(tempDllName, ".", controllerName);

                            controllerType = assembly.GetType(className);
                            routData.RouteValue[nameof(RouteModel.DllName)] = dllName;
                            routData.RouteValue[nameof(RouteModel.DllNameFullPath)] = dllFullPath;
                            routData.RouteValue[nameof(RouteModel.NamespaceClass)] = controllerType;
                            break;
                        }
                    }
                }
            }
            return routData;
        }
    }
}

