using System.ComponentModel.DataAnnotations;

namespace IbayApi.Models
{
    public class CartsInputCreate
    {

        public virtual CartItemsCreate CartItems { get; set; }
        

    }

    public class CartItemsCreate
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
