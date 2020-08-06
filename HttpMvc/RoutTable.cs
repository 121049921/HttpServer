using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpMvc
{
    public class RoutTable
    {
       
        public static RouteConlection Routes { get; set; }
        static RoutTable()
        {
            Routes = new RouteConlection();
        }

    }
}
