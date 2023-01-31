using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbayQuery
{
    public class Carts
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public List<CartsItems> CartItems { get; set; }
        public virtual Users User { get; set; }

    }
}
