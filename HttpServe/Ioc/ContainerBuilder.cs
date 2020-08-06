using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServe
{
    public class ContainerBuilder
    {
        private Dictionary<Type, Resolver> dicContainer = new Dictionary<Type, Resolver>();
        private Type HandlerInstanceType;
        public ContainerBuilder Register<T>() where T : class, new()
        {
            // Activator.CreateInstance<T>();
            HandlerInstanceType = typeof(T);
            Resolver resolver = new Resolver()
            {
                InstanceType = HandlerInstanceType
            };
            if (!dicContainer.ContainsKey(HandlerInstanceType))
            {
                dicContainer[HandlerInstanceType] = resolver;
            }
            return this;

        }
        public IContainer Builder()
        {
            Container container = new Container(dicContainer);
            return container;
        }
    }
}
