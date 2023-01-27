using BLL.Models;
using Dal;
using Dal.Entities;
using IbayApi.Models;
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
        private BLL.Data.ProductsBLL _BLLProduct;

        public CartsController(DatabaseContext context)
        {

            _BLLCart = new BLL.Data.CartsBLL(context);
            _BLLUser = new BLL.Data.Users(context);
            _BLLProduct = new BLL.Data.ProductsBLL(context);
        }

        //[HttpPost]
        //public IActionResult Add([FromQuery] int IdProduit)
        //{
        //    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        //    if (userId == null)
        //        return StatusCode(500, "Error when reading id in token information !");

        //  CartsModel  newCart = _BLLCart.Insert(userId, IdProduit);


        //    return Created("Add", newCart) ;

        //}

        [HttpPost("/add")]
        public IActionResult Add(CartsInputCreate cart)
        {
            CartsModel cartModel = new CartsModel
            {
                UserId = cart.UserId,
                Status = "In Progress",
                CartItems = new List<CartsItemsEntities> {
                    new CartsItemsEntities
                    {
                         ProductId = cart.CartItems.ProductId,
                    Quantity = cart.CartItems.Quantity,
                    Status = "In Progress"
                    }
                   
                }
        };

            var result = _BLLCart.AddToCart(cartModel);

            if (result)
            {
                return Ok("Le produit a bien été ajouté au panier");

            }
            return BadRequest("Un problème est survenu lors de l'ajout au panier");

        }

        [HttpGet("/Get")]
        public IActionResult GetCart()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (userId == null)
                return StatusCode(500, "Error when reading id in token information !");
            var cart = _BLLCart.GetCarts(userId);

            if (cart != null) 
                return Ok(cart);

            return NotFound("Votre panier est vide :( ");
        }

        [HttpPut("/updateProduct")]
        public IActionResult Update(CartsModel cart)
        {
            var result = _BLLCart.Update(cart);
            if (result)
                return Ok("Le panier à bien été mit à jour");

            return BadRequest("Problème lors de la mise à jour du panier !");
        }

        [HttpDelete("/RemoveFromCart")]
        public IActionResult Delete(int productId)
        {

           
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (userId == null)
                return StatusCode(500, "Error when reading id in token information !");

            var result = _BLLCart.RemoveFromCart(productId,userId);


            if (result)
                return Ok("Le produit à bien été retiré du panier");
            
            return BadRequest("Erreur lors de la suppression du panier, veuillez vérifier que le produit est bien dans le panier !");
        }



    }
}
