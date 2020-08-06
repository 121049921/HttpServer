using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpMvc;

namespace HttpApiMethod
{
  
    /// <summary>
    /// 自定义输出
    /// </summary>
    public class ResponseResult
    {
        public int Code { get; set; }
        public string Msg { get; set; }

        public object Result { get; set; }
        public ResponseResult()
        {
            Code = 200;
        }
        public ResponseResult(int code, string msg, object result)
        {
            Code = code;
            Msg = msg;
            Result = result;
        }

        public static ResponseResult Failed(string msg)
        {
            return new ResponseResult(500, msg, null);
        }

        public static ResponseResult OK(object result)
        {
            return new ResponseResult(200, string.Empty, result);
        }

       
    }
}
