using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Entities;

namespace BLL.Models
{
    public class OrdersModel : OrdersEntities
    {
        public int userId { get; set; }
    }
}
