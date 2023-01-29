using Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class CartsModel : CartsEntities
    {
        public decimal TotalAmount
        {
            get
            {
             
                return CartItems.Sum(x => x.Product != null ? x.Product.Price : 0 * x.Quantity);
            }
        }

    }
}
