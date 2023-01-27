using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dal.Entities
{

    public class CartsItemsEntities
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public String Status { get; set; }

        [JsonIgnore]
        public virtual CartsEntities Cart { get; set; }

        [JsonIgnore]
        public virtual ProductsEntities Product { get; set; }
    }
}
