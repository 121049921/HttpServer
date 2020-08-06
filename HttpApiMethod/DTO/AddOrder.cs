using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApiMethod
{
    public partial class AddOrder :EntityBase
    {

        public decimal TotalPrice { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string TableCode { get; set; }


        public List<Dish> Dishs { get; set; }

        public static implicit operator AddOrder(User user)
        {
            return new AddOrder()
            {

                Phone = user.Phone,
                CustomerName = user.Name,
            };

        }

      
    }
    public class Dish
    {

        public string Name { get; set; }
        public string Code { get; set; }
        public Unit Unit { get; set; }
        public Cook CookItems { get; set; }


    }
    public class Unit
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
    }
    public class Cook
    {
        public string Name { get; set; }
        public string Code { get; set; }

    }
}
