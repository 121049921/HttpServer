using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public interface IControllerFactory
    {
        Type DefaulteController( string contorllerName, string rootPath, Route route);
        RouteData CreateController(RouteData routeData,  string controllerName, string rootPath, Route route);
        
    }
}
