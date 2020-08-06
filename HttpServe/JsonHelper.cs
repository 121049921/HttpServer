using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HttpServe
{
    public class JsonHelper
    {/// <summary>
     /// 类对像转换成json格式
     /// </summary> 
     /// <returns></returns>
        public static string ToJson(object t)
        {
            return JsonConvert.SerializeObject(t, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }

        /// <summary>
        /// 字符串转成json对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objJosn"></param>
        /// <returns></returns>
        public static T ToObject<T>(string objJosn)
        {
            return JsonConvert.DeserializeObject<T>(objJosn);
        }

    }
}
