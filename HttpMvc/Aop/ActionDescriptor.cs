using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public class ActionDescriptor : Clone
    {
        public string Name { get; set; }

        public MethodInfo Member { get; set; }

        public ParameterInfo[] Parameters { get; set; }
       
        public object Value { get; internal set; }
     
        public override string ToString()
        {
            return this.Value?.ToString();
        }
        public IActionAttribute[] Attributes { get; set; }

        public object Clone()
        {
          
            return Copy(this);

        }
    }
}
