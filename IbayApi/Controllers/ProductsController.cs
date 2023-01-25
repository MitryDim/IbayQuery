using Azure.Core;
using BLL.Models;
using Dal;
using Dal.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IbayApi.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private BLL.Data.ProductsBLL _BLL;
        private DatabaseContext _dbContext;

        public ProductsController(DatabaseContext context)
        {
            _BLL = new BLL.Data.ProductsBLL(context);
            _dbContext = context;

        }



        /// <summary>
        /// List all products available
        /// </summary>
        // GET: Products
        [HttpGet]
        [ProducesResponseType(typeof(ProductsEntities), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<List<ProductsEntities>> GetAll([FromQuery] string sortBy = "Name", int limit = 10, string query = "")
        {
            return Ok(_BLL.GetAll(sortBy, limit , query));
        }

        /// <summary>
        /// Find an availabled product by Id
        /// </summary>
        // GET: Product by ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductsEntities), 200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<ProductsEntities> GetProductById(int id)
        {

            return Ok(_BLL.GetProductById(id));

        }


        /// <summary>
        /// Add a product
        /// </summary>
        // POST: Product
        [HttpPost]
        [Authorize(Roles = "Seller")]
        [ProducesResponseType(typeof(ProductsModel), 201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult PostProduct([FromForm] ProductsInput product)
        {

            if (product == null)
            {
                return BadRequest();
            }

            // get the owner id
            var ownerId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var newProduct = new ProductsModel { Name = product.Name, Image = product.Image, Price = product.Price, Available = product.Available, Added_Hour = DateTime.UtcNow, OwnedId = int.Parse(ownerId) };


            var data = _BLL.Insert(newProduct);
            // insertion de notre objet
            return Created(data.Name, data);
        }

        /// <summary>
        /// Update a product by his Id
        /// </summary>
        //// PUT: Product/5
        [HttpPut]
        [Authorize(Roles = "Seller, Admin")]
        [ProducesResponseType(typeof(ProductsInput), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult PutProduct([FromQuery] int id, [FromForm] ProductsInput product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            // get the owner id
            var OwnedId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // check if the product is owned by the currently logged in user
            var currentProduct = _BLL.CheckProductOwner(id, int.Parse(OwnedId));

            if (currentProduct == false)
            {
                return Unauthorized();
            }

            var newProduct = new ProductsModel { Id = id, Name = product.Name, Image = product.Image, Price = product.Price, Available = product.Available };

            var data = _BLL.Update(newProduct);
            // update de notre objet
            return Ok(data);
        }

        /// <summary>
        /// Delete a product by his Id
        /// </summary>
        // DELETE: Product/5
        [HttpDelete]
        [Authorize(Roles = "Seller, Admin")]
        [ProducesResponseType(typeof(ProductsInput), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteProduct([FromQuery] int id)
        {
            // get the owner id
            var OwnedId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // check if the product is owned by the currently logged in user
            var currentProduct = _BLL.CheckProductOwner(id, int.Parse(OwnedId));

            if (currentProduct == false)
            {
                return Unauthorized();
            }

            var data = _BLL.Delete(id);
            // delete de notre objet
            return Ok(data);

        }
    }

}
