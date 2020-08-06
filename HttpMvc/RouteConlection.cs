using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using System.IO;

namespace HttpMvc
{
    public class RouteConlection
    {
        public string Name { get; set; }
        public Route Route { get; set; }

        public void Add(string defaultName, Route route)
        {

            Name = defaultName;
            Route = route;

        }

        public RouteData GetRouteData(HttpListenerContext context)
        {

            RouteData routeData = new RouteData();
            HttpListenerRequest request = context.Request;
            string rawUrl = request.RawUrl;

            if (rawUrl.Contains("?"))
            {
                var whIndex = rawUrl.IndexOf("?");
                rawUrl = rawUrl.Substring(0, whIndex);
            }
            List<string> controllerActionArray = rawUrl.Split(new string[] { "{", "}", "/" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            //匹配路由规则
            List<string> defaultRouteUrlRuleArray = Route.DefaultRouteUrlRule.Split(new string[] { "{", "}", "/" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            routeData.RouteValue = new Dictionary<string, object>();

            for (int i = 0; i < defaultRouteUrlRuleArray.Count; i++)
            {
                var temp = controllerActionArray.Count > i ? controllerActionArray[i] : string.Empty;
                if (string.IsNullOrEmpty(temp))
                {
                    break;
                }
                routeData.RouteValue[defaultRouteUrlRuleArray[i]] = temp;
            }

            Dictionary<string, object> dicParameters = new Dictionary<string, object>();

            //1.URL参数
            NameValueCollection requestParametes = request.QueryString;
            if (requestParametes != null)
            {

                if (requestParametes.HasKeys())
                {
                    string[] allKey = requestParametes.AllKeys;

                    foreach (var key in allKey)
                    {
                        var value = requestParametes[key];
                        Type parameterType = UrlParameterHandler.GetUrlParameterType(value);
                        if (parameterType.Equals(typeof(int)))
                        {

                            dicParameters[key] = int.Parse(value.ToString());
                        }
                        if (parameterType.Equals(typeof(decimal)))
                        {
                            dicParameters[key] = decimal.Parse(value.ToString());
                        }
                        if (parameterType.Equals(typeof(double)))
                        {
                            dicParameters[key] = double.Parse(value.ToString());
                        }
                        if (parameterType.Equals(typeof(DateTime)))
                        {
                            dicParameters[key] = DateTime.Parse(value.ToString());
                        }
                        if (parameterType.Equals(typeof(string)))
                        {
                            dicParameters[key] =value.ToString();
                        }
                      


                    }
                }
            }

            //2.Request-Boby参数

            using (Stream inputStream = context.Request.InputStream)
            {
                using (StreamReader reader = new StreamReader(inputStream))
                {
                    string requestBobyJson = reader.ReadToEnd();
                    if (!string.IsNullOrEmpty(requestBobyJson))
                    {
                        dicParameters[nameof(RouteModel.BodyJson)] = requestBobyJson;
                    }
                }
            }

            if (dicParameters != null && dicParameters.Count > 0)
            {
                routeData.RouteValue[nameof(RouteModel.Parameter)] = dicParameters;
            }
            return routeData;
        }
    }
}
