using Microsoft.AspNetCore.Mvc;

namespace Store.Cart.API.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
