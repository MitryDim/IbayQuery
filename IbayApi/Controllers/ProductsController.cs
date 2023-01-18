using Dal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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


        // GET: Products
        [HttpGet]
        public ActionResult<List<Products>> GetAll()
        {

            return Ok(_context.Product);

        }

        // GET: Product by ID
        [HttpGet("{id}")]
        public ActionResult<List<Products>> GetProductById(int id)
        {

            var product = _context.Product
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .OrderBy(c => c.id);

            if (classproductroom == null)
            {
                return NotFound();
            }
            return Ok(product);

        }

        // POST: Product
        [HttpPost]
        public IActionResult PostProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            // insertion de notre objet
            return Created($"Product/{product.id}", product);
        }

        // PUT: Product/5
        [HttpPut("{id}")]
        public IActionResult PutProduct(int id, Product product)
        {
            if (id != product.id)
            {
                return BadRequest();
            }
            // modification de notre objet
            return NoContent();
        }

        // DELETE: Product/5
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int? id)
        {
            var classroomExist = _context.Product.Where(c => c.id == id).FirstOrDefault();

            if (classroomExist == null)
                return NotFound();

            try
            {
                _context.Classroom.Remove(classroomExist);
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
