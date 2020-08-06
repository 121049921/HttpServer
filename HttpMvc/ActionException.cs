using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public class ActionException : Exception
    {

    
        public ActionException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public ActionException()
        {
        }
        public ActionException(string message) : base(message)
        {
        }
    }
}
