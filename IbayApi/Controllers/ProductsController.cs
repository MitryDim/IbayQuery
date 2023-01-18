using Dal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IbayApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _config;

        DatabaseContext _context;

        public ProductsController(DatabaseContext context)
        {

            _context = context;

        }

        // Vérification => If product exist
        private bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }


        // GET: Products
        [HttpGet]
        public ActionResult<List<Products>> GetAll()
        {

            return Ok(_context.Products);

        }

        // GET: Product by ID
        [HttpGet("{id}")]
        public ActionResult<Products> GetProductById(int id)
        {

            var product = _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }

        // POST: Product
        [HttpPost]
        public IActionResult PostProduct([FromForm] Products product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            //get the file extension
            var fileExtension = Path.GetExtension(product.Image.FileName);
            //generate a new file name
            var newFileName = Guid.NewGuid() + fileExtension;
            //get the file path
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Public", "Images\\Products", newFileName);

            //open the file stream
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                //copy the file to the server
                product.Image.CopyTo(fileStream);
            }
            //save the other product's information
            product.ImageURL = newFileName;
            _context.Products.Add(product);
            _context.SaveChanges();

            // insertion de notre objet
            return Created($"Products/{product.Id}", product);
        }

        // PUT: Product/5
        [HttpPut("{id}")]
        public IActionResult PutProduct([FromQuery] int id, [FromBody] Products product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: Product/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int? id)
        {
            var product = _context.Products.Where(p => p.Id == id).FirstOrDefault();

            if (product == null)
                return NotFound();

            try
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
    }

}
