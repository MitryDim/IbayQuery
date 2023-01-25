using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public int CartId { get; set; }

        public DateTime Added_Hour { get; set; }

        public bool IsPayed { get; set; }

    }
}
