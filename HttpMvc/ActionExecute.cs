using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HttpMvc
{
    public class ActionExecute : IActionExecute
    {


        public RouteData ExecuteMethod(RouteData routeData, Type controllerType)
        {
            object result = null;
            MethodBase method = null;
            try
            {
                object controllerInstance = Activator.CreateInstance(controllerType);

                List<Type> executeMethodParameterTypeList = new List<Type>();
                object tempActionName = null;
                if (routeData.RouteValue.ContainsKey(nameof(RouteModel.Action)))
                {
                    tempActionName = routeData.RouteValue[nameof(RouteModel.Action)];
                }

                Dictionary<string, object> dicParameters = null;
                if (routeData.RouteValue.ContainsKey(nameof(RouteModel.Parameter)))
                {
                    dicParameters = (Dictionary<string, object>)routeData.RouteValue[nameof(RouteModel.Parameter)];
                }

                object[] parameters = null;    //规则:定义方法时把对象参数放到最后面
                if (tempActionName != null)
                {
                    ActionMethodSelector actionMethodSelector = new ActionMethodSelector(controllerType);
                    if (!string.IsNullOrEmpty(tempActionName.ToString()))
                    {
                        string actionName = tempActionName.ToString();
                        method = actionMethodSelector.ActionMethods.FirstOrDefault(p => p.Name == actionName);
                    }
                    if (method != null)
                    {
                        ParameterInfo[] parameteList = method.GetParameters();
                       
                        parameters = new object[parameteList.Length];
                        if (dicParameters != null)
                        {
                            int i = 0;
                            foreach (KeyValuePair<string, object> paramter in dicParameters)
                            {
                                //判断是Body参数
                                if (paramter.Key == nameof(RouteModel.BodyJson))
                                {
                                    if (paramter.Value != null)
                                    {
                                        if (!string.IsNullOrEmpty(paramter.Value.ToString()))
                                        {
                                            string json = paramter.Value != null ? paramter.Value.ToString() : string.Empty;
                                            StringBuilder tempJson = new StringBuilder();
                                            ParameterInfo propertyInfo = parameteList.LastOrDefault();
                                            if (propertyInfo.ParameterType.IsEnum
                                                || propertyInfo.ParameterType.IsAssignableFrom(typeof(int)) 
                                                || propertyInfo.ParameterType.IsAssignableFrom(typeof(decimal))
                                                || propertyInfo.ParameterType.IsAssignableFrom(typeof(double))
                                                || propertyInfo.ParameterType.IsAssignableFrom(typeof(DateTime))
                                                || propertyInfo.ParameterType.IsAssignableFrom(typeof(string))
                                                )
                                            {

                                            }
                                            else
                                            {
                                                var name = propertyInfo.Name;
                                                tempJson.Append("{");
                                                tempJson.Append("\"" + name + "\"").Append(":");
                                                tempJson.Append(json).Append("}");
                                                json = tempJson.ToString();
                                            }
                                            JObject jObject = JObject.Parse(json);
                                            JToken parameterValue = jObject.GetValue(propertyInfo.Name, StringComparison.OrdinalIgnoreCase);
                                            if (parameterValue != null)
                                            {
                                                object entity = parameterValue.ToObject(propertyInfo.ParameterType);
                                                parameters[i] = entity;
                                                break;
                                            }
                                            else
                                            {
                                                parameters[i] = null;
                                            }
                                        }
                                    }
                                }
                                else//Url参数
                                {
                                    var vlaue = paramter.Value;
                                    Type parameterType = UrlParameterHandler.GetUrlParameterType(paramter.Value);
                                    if (parameterType.Equals(typeof(int)))
                                    {
                                        parameters[i] = int.Parse(paramter.Value.ToString());
                                    }
                                    if (parameterType.Equals(typeof(decimal)))
                                    {
                                        parameters[i] = decimal.Parse(paramter.Value.ToString());
                                    }
                                    if (parameterType.Equals(typeof(double)))
                                    {
                                        parameters[i] = double.Parse(paramter.Value.ToString());
                                    }
                                    if (parameterType.Equals(typeof(DateTime)))
                                    {
                                        parameters[i] = DateTime.Parse(paramter.Value.ToString());
                                    }
                                    if (parameterType.Equals(typeof(string)))
                                    {
                                        parameters[i] = paramter.Value.ToString();
                                    }
                                }
                                i++;
                            }

                            result = method.Invoke(controllerInstance, parameters.ToArray());
                        }
                        else
                        {
                            result = method.Invoke(controllerInstance, null);
                        }
                    }
                    else
                    {
                        result = new ErrorResult()
                        {
                            Code = 500,
                            Msg = "找不到" + tempActionName + "控制器",
                        };
                        routeData.RouteValue[nameof(ActionResult)] = result;
                    }
                }
                else
                {
                    result = new ErrorResult()
                    {
                        Code = 500,
                        Msg = "找不到" + tempActionName + "控制器",
                    };
                    routeData.RouteValue[nameof(ActionResult)] = result;
                }
                routeData.RouteValue[nameof(ActionResult)] = result;
            }
            catch (Exception ex)
            {

                result = new ErrorResult()
                {
                    Code = 500,
                    Msg = ex.ToString(),
                };
                routeData.RouteValue[nameof(ActionResult)] = result;

            }
            return routeData;
        }



    }
}
