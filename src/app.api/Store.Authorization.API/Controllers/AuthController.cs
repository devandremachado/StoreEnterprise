using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Authorization.API.Models;
using System.Threading.Tasks;

namespace Store.Authorization.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        public readonly SignInManager<IdentityUser> _signInManager;
        public readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UserRegistrationDTO userDTO)
        {
            if (ModelState.IsValid == false) return BadRequest();

            var user = new IdentityUser
            {
                UserName = userDTO.Email,
                Email = userDTO.Email,
                EmailConfirmed = true 
            };

            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded == false)
            {
                return BadRequest();
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok();
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userDTO)
        {
            if (ModelState.IsValid == false) return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, false, true);

            if (result.Succeeded == false)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
