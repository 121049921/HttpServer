using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public class ActionAttribute : Attribute, IActionAttribute
    {
        public void BeforeRequest(RouteData context)
        {
            throw new NotImplementedException();
        }
    }
}
