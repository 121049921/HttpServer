using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApiMethod
{
    /// <summary>
    /// 适配器模式:转化成适配对接
    /// </summary>
    public interface IAdapter
    {
   
        List<string> GetString();
        List<User> GetList();
    }
}
