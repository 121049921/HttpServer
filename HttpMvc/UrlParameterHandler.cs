using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public class UrlParameterHandler
    {


        public static Type GetUrlParameterType(object value)
        {

            bool flag = false;
            if (value != null)
            {
                string tempValue = value.ToString();


                int result;
                flag = int.TryParse(tempValue, out result);
                if (flag)
                {
                    return typeof(int);
                }
                DateTime DateTimeResult;
                flag = DateTime.TryParse(tempValue, out DateTimeResult);
                if (flag)
                {
                    return typeof(DateTime);
                }
            }
            return typeof(string);
        }
    }

   
}
