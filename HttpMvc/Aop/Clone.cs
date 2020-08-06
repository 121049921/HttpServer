using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public partial class Clone
    {
        public T Copy<T>(T entity)
        {
            return (T)this.MemberwiseClone();
        }
    }

   
}
