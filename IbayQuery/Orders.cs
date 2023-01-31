using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbayQuery
{
    public class Orders
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime Added_Hour { get; set; }
        public List<Payements> Payements { get; set; }
        public virtual Carts Cart { get; set; }
        public virtual Users User { get; set; }

    }
}
