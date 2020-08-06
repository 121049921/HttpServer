using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace HttpMvc
{
    public class RouteModel
    {
        public string DllName { get; set; }
        public string DllNameFullPath { get; set; }
        public Type NamespaceClass { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public object Parameter { get; set; }
        public object BodyJson { get; set; }

    }

    public class Route
    {
        /// <summary>
        /// 默认路由规则,如果重写多种规则,把下面改成集合
        /// </summary>
        public string DefaultRouteUrlRule { get; set; }
        public List<Dictionary<string, object>> DicRouteList { get; set; }
        public Route(string defaultRouteUrlRule, List<Dictionary<string, object>> dicRouteList)
        {
            DefaultRouteUrlRule = defaultRouteUrlRule;
            DicRouteList = dicRouteList;

        }
    }
}
