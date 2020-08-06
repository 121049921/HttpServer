using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServe
{
    public class Container : IContainer
    {
      private Dictionary<Type, Resolver> dicContainer = new Dictionary<Type, Resolver>();

        public Container(Dictionary<Type, Resolver> dicContainer)
        {
            this.dicContainer = dicContainer;
        }

        public T Resolve<T>() where T : class
        {
            return (T) dicContainer[typeof(T)].GetHandlerInstance();
        }
    }
}
