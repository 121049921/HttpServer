using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApiMethod
{

    public class User : EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }

        public PayType PayType { get; set; }
    }
}
