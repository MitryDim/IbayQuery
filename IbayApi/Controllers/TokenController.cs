using Microsoft.AspNetCore.Mvc;

namespace IbayApi.Controllers
{
    public class TokenController : Controller
    {
        public IConfiguration _configuration;
        private readonly DatabaseContext _context;

            public TokenController(IConfiguration config,DatabaseContext context)
        {
            _configuration = config;
            _context = context; 

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
