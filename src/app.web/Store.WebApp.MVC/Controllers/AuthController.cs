using Microsoft.AspNetCore.Mvc;
using Store.WebApp.MVC.Models.User.Request;
using System.Threading.Tasks;

namespace Store.WebApp.MVC.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        [Route("nova-conta")]
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }

        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> CreateUser(UserRequestDTO userDTO)
        {
            if (ModelState.IsValid == false) 
                return View(userDTO);

            return View();
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            if (ModelState.IsValid == false)
                return View(loginDTO);

            return View();
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {

            return RedirectToAction("Index", "Home");
        }
    }
}
