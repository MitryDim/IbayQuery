using BLL.Models;
using Dal;
using Dal.Entities;
using IbayApi.Models;
using Microsoft.AspNetCore.Authorization;
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


        /// <summary>
        /// Add an product in cart
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost("/add"), Authorize]
        [ProducesResponseType(typeof(ProductsInput), 201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Add([FromForm] int productId, [FromForm] int quantity)
        {
            if(productId == 0 || quantity == 0)           
                return BadRequest();
            

            if (User.FindFirst(ClaimTypes.NameIdentifier) == null)
                return StatusCode(500, "Error when reading id in token information !");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            CartsModel cartModel = new CartsModel
            {
                UserId = userId,
                Status = "In Progress",
                CartItems = new List<CartsItemsEntities> {
                    new CartsItemsEntities
                    {
                         ProductId = productId,
                    Quantity = quantity,
                    Status = "In Progress"
                    }
                   
                }
        };

            var result = _BLLCart.AddToCart(cartModel);

            if (result)
            {
                return Created("Add","Le produit a bien été ajouté au panier");

            }
            return BadRequest("Un problème est survenu lors de l'ajout au panier");

        }

        /// <summary>
        /// Get product in cart of the user connected
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Get"), Authorize]
        [ProducesResponseType(typeof(ProductsInput), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetCart()
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier) == null)
                return StatusCode(500, "Error when reading id in token information !");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            var cart = _BLLCart.GetCarts(userId);

            if (cart != null) 
                return Ok(cart);

            return NotFound("Votre panier est vide :( ");
        }

        /* /// <summary>
        /// Update cart
        /// </summary>
        /// <param name = "cart" ></ param >
        /// < returns ></ returns >
       [HttpPut("/updateProduct")]
        [Authorize]
        public IActionResult Update(CartsModel cart)
        {
            var result = _BLLCart.Update(cart);
            if (result)
                return Ok("Le panier à bien été mit à jour");

            return BadRequest("Problème lors de la mise à jour du panier !");
        }*/



        /// <summary>
        /// Remove product in cart
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("/RemoveFromCart"), Authorize]
        [ProducesResponseType(typeof(ProductsInput), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int productId)
        {

            if (User.FindFirst(ClaimTypes.NameIdentifier) == null)
                return StatusCode(500, "Error when reading id in token information !");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = _BLLCart.RemoveFromCart(productId,userId);


            if (result)
                return Ok("Le produit à bien été retiré du panier");
            
            return BadRequest("Erreur lors de la suppression du panier, veuillez vérifier que le produit est bien dans le panier !");
        }



    }
}
