using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{

    /// <summary>
    /// 执行方法之前
    /// </summary>
    public interface IActionAttribute
    {
        void BeforeRequest(RouteData context);
    }
}
