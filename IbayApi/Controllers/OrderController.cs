using BLL.Data;
using BLL.Models;
using Dal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IbayApi.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        private BLL.Data.OrdersBLL _BLL;
        private DatabaseContext _dbContext;

        public OrdersController(DatabaseContext context)
        {
            _BLL = new BLL.Data.OrdersBLL(context);
            _CartsBLL = new BLL.Data.CartsBLL(context);
            _dbContext = context;

        }

        /// <summary>
        /// Add an order, containing products in the cart
        /// </summary>
        // POST: Orders
        [HttpPost]
        [Authorize(Roles = "User, Seller, Admin")]
        [ProducesResponseType(typeof(OrdersModel), 201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult PostOrders([FromForm] OrdersInput order)
        {

            if (order == null)
            {
                return BadRequest();
            }

            // get the cart id with the id choice
            var CartId = _CartsBLL.SearchById(order.CartId);

            var newProduct = new OrdersModel { NumOrders = Guid.NewGuid(), CartId = CartId, Added_Hour = DateTime.UtcNow };


            var data = _BLL.Insert(newProduct);
            // insertion de notre objet
            return Created(data.NumOrders.ToString(), data);
        }
    }

}
