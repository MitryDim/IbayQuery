using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Entities
{
    public class OrdersEntities
    {
        [Key]
        public int Id { get; set; }

        public Guid NumOrders { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }

        public DateTime Added_Hour { get; set; }

        public virtual CartsEntities Cart { get; set; }
    }
}
