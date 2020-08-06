using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpServe
{
    public class Resolver
    {
        public Type InstanceType { get; set; }

        public object GetHandlerInstance()
        {
            return Activator.CreateInstance(InstanceType);
        }
    }
}
