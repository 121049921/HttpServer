using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public interface IActionExecute
    {
        RouteData ExecuteMethod(RouteData routeData, Type controllerType);

      
    }
}
