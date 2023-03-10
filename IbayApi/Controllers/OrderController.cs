using BLL.Data;
using BLL.Models;
using Dal;
using IbayApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace IbayApi.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private BLL.Data.OrdersBLL _BLL;
        private CartsBLL _BLLCart;
        private DatabaseContext _dbContext;

        public OrdersController(DatabaseContext context)
        {
            _BLL = new BLL.Data.OrdersBLL(context);
            _dbContext = context;

        }

        /// <summary>
        /// Add an order, containing products in the cart
        /// </summary>
        // POST: Orders
        [HttpPost("Add")]
        [Authorize(Roles = "User, Seller, Admin")]
        [ProducesResponseType(typeof(OrdersModel), 201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult PostOrders([FromForm] OrderInput order)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            if (userId == null)
                return StatusCode(500, "Error retrieving token ! ");


            var newOrder = new OrdersModel
            {
                userId = userId,
                Status = order.status.ToLower().Trim(),
                TotalPrice = order.TotalPrice,
                Payements = new List<Dal.Entities.PayementsEntities>
                {
                    new Dal.Entities.PayementsEntities
                    {
                        Amount= order.TotalPrice,
                        Type =  order.paymentType.ToLower().Trim()
                    }
                }
            };

            var data = _BLL.Insert(newOrder);
            if (data)
                return Created(("ProceedOrder"), "The order has been made");

            return BadRequest();
        }
    }

}
