using Azure.Core;
using BLL.Models;
using Dal;
using Dal.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IbayApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private BLL.Data.Products _BLL;

        public ProductsController(DatabaseContext context)
        {
            _BLL = new BLL.Data.Products(context);

        }

        // GET: Products
        [HttpGet]
        public ActionResult<List<ProductsEntities>> GetAll()
        {

            return Ok(_BLL.GetAll());

        }

        // GET: Product by ID
        [HttpGet("{id}")]
        public ActionResult<ProductsEntities> GetProductById(int id)
        {

            return Ok(_BLL.GetProductById(id));

        }

        // POST: Product
        [HttpPost]
        public IActionResult PostProduct([FromForm] ProductsModel product)
        {

            if (product == null)
            {
                return BadRequest();
            }

            var data = _BLL.Insert(product);
            // insertion de notre objet
            return Created(data.Name, data);
        }

        //// PUT: Product/5
        //[HttpPut("{id}")]
        //public IActionResult PutProduct([FromQuery] int id, [FromBody] Products product)
        //{
        //    if (id != product.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(product).State = EntityState.Modified;

        //    try
        //    {
        //        _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// DELETE: Product/5
        //[HttpDelete("{id}")]
        //public IActionResult DeleteProduct(int? id)
        //{
        //    var product = _context.Products.Where(p => p.Id == id).FirstOrDefault();

        //    if (product == null)
        //        return NotFound();

        //    try
        //    {
        //        _context.Products.Remove(product);
        //        _context.SaveChanges();
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest();
        //    }

        //}
    }

}
