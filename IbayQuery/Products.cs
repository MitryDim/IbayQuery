using Microsoft.AspNetCore.Http;

namespace IbayQuery
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public Boolean Available { get; set; }
        public DateTime Added_Hour { get; set; }
        public int OwnedId { get; set; }


    }
}
