using Microsoft.AspNetCore.Mvc;

namespace IbayApi.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
