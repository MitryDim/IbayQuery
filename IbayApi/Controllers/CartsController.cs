using BLL.Models;
using Dal;
using Dal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
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
        /// Get product in cart of the user connected
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401), ProducesResponseType(403), ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetCart()
        {
            if (User.FindFirst(ClaimTypes.NameIdentifier) == null)
                return StatusCode(500, "Error retrieving token ! ");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            var cart = _BLLCart.GetCarts(userId);

            if (cart != null)
                return Ok(cart);

            return NotFound("your basket is empty :( ");
        }


        /// <summary>
        /// Add an product in cart
        /// </summary>
        /// <param name="productId">Id of product</param>
        /// <param name="quantity">Quantity to be added to the cart</param>
        /// <returns></returns>
        [HttpPost("Add"), Authorize]
        [ProducesResponseType(201)]
        [ProducesResponseType(401), ProducesResponseType(403), ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Add([FromForm] int productId, [FromForm] int quantity)
        {
            if (productId <= 0 || quantity <= 0)
                return BadRequest();

            if (User.FindFirst(ClaimTypes.NameIdentifier) == null)
                return StatusCode(500, "Error retrieving token ! ");

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
                return Created("Add", "The product has been added to cart");

            }
            return BadRequest("There was a problem adding to cart");
        }

        [HttpDelete("RemoveFromCart"), Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401), ProducesResponseType(403), ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int productId)
        {

            if (User.FindFirst(ClaimTypes.NameIdentifier) == null)
                return StatusCode(500, "Error retrieving token ! ");

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var result = _BLLCart.RemoveFromCart(productId, userId);

            if (result)
                return Ok("The product has been successfully removed from the basket");

            return BadRequest("Error when deleting the basket, please check that the product is in the basket !");
        }



    }
}
