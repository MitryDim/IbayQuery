using BLL.Models;
using Dal;
using Dal.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IbayApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private BLL.Data.CartsBLL _BLLCart;
        private BLL.Data.Users _BLLUser;
        private BLL.Data.Products _BLLProduct;

        public CartsController(DatabaseContext context)
        {

            _BLLCart = new BLL.Data.CartsBLL(context);
            _BLLUser = new BLL.Data.Users(context);
            _BLLProduct = new BLL.Data.Products(context);
        }

        [HttpPost]
        public IActionResult Add([FromQuery] int IdProduit)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (userId == null)
                return StatusCode(500, "Error when reading id in token information !");

          
          
            return Ok(_BLLCart.Insert(userId, IdProduit));



        }
    }
}
