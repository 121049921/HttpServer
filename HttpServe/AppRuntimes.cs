using System;
using System.Collections.Generic;
using HttpMvc;
using System.IO;

using System.ComponentModel;
using System.Linq;
using Uitl;

namespace HttpServe
{

    [Description("MVC及其它信息")]
    public partial class AppRuntimes
    {
        private AppRuntimes() { }
        private static object objLocker = new object();
        private static AppRuntimes instance;
        private IContainer container;
        public string Url
        {
            get
            {
                return "http://127.0.0.1:8081/";
            }
            internal set { }
        }

        public IContainer Container
        {
            get
            {
                return container;
            }
            set
            {
                container = value;
            }
        }
        public IHttpHandler HttpHandler { get; set; }
        public static AppRuntimes Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (objLocker)
                    {
                        if (instance == null)
                        {
                            instance = new AppRuntimes();

                        }
                    }
                }
                return instance;
            }
            set => instance = value;
        }

       

        public void ApplicattionStart()
        {
            RegisterIoC();
            //逻辑这里应该是在判断是MvcHandler才起调RegisterRoute,但很次调用很耗时这里,直接调用让程序加载启动
            RegisterRoute();
        }
        private void RegisterIoC()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.Register<MvcHandler>();
            containerBuilder.Register<OtherHandler>();
            container = containerBuilder.Builder();

        }

        public void RegisterRoute()
        {
            List<Dictionary<string, object>> dicRouteList = new List<Dictionary<string, object>>();
            List<RouteConfig> routeConfigList = new List<RouteConfig>();
            var routeConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "RouteConfig.json");
            if (File.Exists(routeConfigPath))
            {
                StreamReader sr = new StreamReader(routeConfigPath);
                if (sr != null)
                {
                    string json = sr.ReadToEnd();
                    routeConfigList = JsonHelper.ToObject<List<RouteConfig>>(json);
                }
                //1.默认区域和 Controller等,在MVC在添加区域时会自动添加区域等陆由的cs类,所以这里我们要改写成配置,原理一样
                Dictionary<string, object> defaultDicRoute = new Dictionary<string, object>();
                defaultDicRoute.Add(nameof(RouteModel.Controller), "Home");
                defaultDicRoute.Add(nameof(RouteModel.Action), "Index");
                defaultDicRoute.Add(nameof(RouteModel.Parameter), null);
                defaultDicRoute.Add(nameof(RouteModel.DllName), "defualt.dll");

                dicRouteList.Add(defaultDicRoute);


                foreach (var routeConfigItem in routeConfigList)
                {
                    Dictionary<string, object> dicRoute = new Dictionary<string, object>();
                    dicRoute.Add(nameof(RouteModel.Controller), string.Empty);
                    dicRoute.Add(nameof(RouteModel.Action), string.Empty);
                    dicRoute.Add(nameof(RouteModel.Parameter), null);
                    dicRoute.Add(nameof(RouteModel.DllName), routeConfigItem.Dll);
                    dicRouteList.Add(dicRoute);
                }
                //2.默认路由规则(默认路由规则,如果重写多种规则,把下面defaultRouteUrlRule参数改成集合,注意添加的到集合中的顺利,遍历一旦匹配,必须break,MVC的机制)
                string defaultRouteUrlRule = "{Controller}/{Action}/{Parameter}";

                Route route = new Route(defaultRouteUrlRule, dicRouteList);
                RoutTable.Routes.Add("defaultRoute", route);

                
            }
            else
            {
                throw new FileNotFoundException(routeConfigPath, "文件不存在");
            }

        }

        public string RootPath
        {

            get { return AppDomain.CurrentDomain.BaseDirectory; }


        }


    }


    
    public partial class AppRuntimes
    {

        public string ReadLog()
        {
            string errorMsg = string.Empty;
            var dirLog = Path.Combine(RootPath, "Log");
            if (Directory.Exists(dirLog))
            {
                List<string> files = Directory.GetFiles(dirLog).ToList();
                foreach (var file in files)
                {
                    DateTime creationTime = File.GetCreationTime(file);
                    if (creationTime.Date.Equals(DateTime.Now.Date))
                    {
                        errorMsg = File.ReadAllText(file);
                        break;
                    }
                }
            }
            return errorMsg;
        }
    }

}
