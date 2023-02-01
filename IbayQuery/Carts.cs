using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbayQuery
{
    [JsonObject]
    public class Carts
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public List<CartsItems> CartItems { get; set; }

        public decimal TotalAmount { get; set; }

    }
}
