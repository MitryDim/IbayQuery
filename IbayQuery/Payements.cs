using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace IbayQuery
{
    public class Payements
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual Orders Order { get; set; }


    }
}
