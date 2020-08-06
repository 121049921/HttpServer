using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public interface IParameterAttribute
    {
        Task BeforeRequest(RouteData context, ParameterDescriptor parameter);
    }
}
