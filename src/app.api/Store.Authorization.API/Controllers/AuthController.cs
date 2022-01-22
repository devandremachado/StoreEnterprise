using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.Authorization.API.Extensions;
using Store.Authorization.API.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.Authorization.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        public readonly SignInManager<IdentityUser> _signInManager;
        public readonly UserManager<IdentityUser> _userManager;
        public readonly AppSettings _appSettings;

        public AuthController(SignInManager<IdentityUser> signInManager, 
                              UserManager<IdentityUser> userManager,
                              IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser(UserRegistrationDTO userDTO)
        {
            if (ModelState.IsValid == false) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = userDTO.Email,
                Email = userDTO.Email,
                EmailConfirmed = true 
            };

            var result = await _userManager.CreateAsync(user, userDTO.Password);

            if (result.Succeeded == false)
            {
                foreach (var error in result.Errors) 
                    AddError(error.Description);

                return CustomResponse();
            }

            return CustomResponse(await CreateJWT(userDTO.Email));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userDTO)
        {
            if (ModelState.IsValid == false) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, false, true);

            if (result.Succeeded == false)
            {
                if(result.IsLockedOut)
                {
                    AddError("User temporarily blocked for invalid attempts");
                    return CustomResponse();
                }

                AddError("Incorrect username or password");
                return CustomResponse();
            }

            return CustomResponse(await CreateJWT(userDTO.Email));
        }

        private async Task<UserResponseLogin> CreateJWT(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); //Token Id
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Nbf, DateTime.UtcNow.ToUnixEpochDate().ToString())); //Data expiração token
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64)); // Data emissao token

            foreach (var userRole in userRoles)
            {
                userClaims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(userClaims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new UserResponseLogin
            {
                AccesToken = encodedToken,
                ExpirationIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = userClaims.Select(x => new UserClaim { Type = x.Type, Value = x.Value })
                }
            };

            return response;
        }
    }
}
