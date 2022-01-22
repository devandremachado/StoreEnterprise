using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Store.WebApp.MVC.Models.User.Request;
using Store.WebApp.MVC.Models.User.Token;
using Store.WebApp.MVC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.WebApp.MVC.Controllers
{
    public class AuthController : BaseController
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #region Views

        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        [Route("nova-conta")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Actions
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid == false)
                return View(loginDTO);

            var response = await _authService.Login(loginDTO);

            if (HasResponseError(response.ResponseResult))
                return View(loginDTO);

            await SignInUser(response);

            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Home");

            return LocalRedirect(returnUrl);
        }


        [HttpPost]
        [Route("nova-conta")]
        public async Task<IActionResult> CreateUser(UserRequestDTO userDTO)
        {
            if (ModelState.IsValid == false)
                return View(userDTO);

            var response = await _authService.CreateUser(userDTO);

            if (HasResponseError(response.ResponseResult))
                return View(userDTO);

            await SignInUser(response);
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Methods

        private async Task SignInUser(UserTokenJwt user)
        {
            var token = new JwtSecurityTokenHandler().ReadJwtToken(user.AccessToken);

            var claims = new List<Claim>();
            claims.Add(new Claim("JWT", user.AccessToken));
            claims.AddRange(token.Claims);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
        #endregion
    }
}
