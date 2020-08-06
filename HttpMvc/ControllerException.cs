using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public class ControllerException : Exception
    {

    
        public ControllerException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public ControllerException()
        {
        }
        public ControllerException(string message) : base(message)
        {
        }
    }
}
