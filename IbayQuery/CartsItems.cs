using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbayQuery
{
    public class CartsItems
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public String Status { get; set; }
        public virtual Carts Cart { get; set; }
        public virtual Products Product { get; set; }


    }
}
