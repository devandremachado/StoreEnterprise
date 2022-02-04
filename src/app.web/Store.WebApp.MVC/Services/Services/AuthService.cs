using Microsoft.Extensions.Options;
using Store.WebApp.MVC.Extensions;
using Store.WebApp.MVC.Helpers;
using Store.WebApp.MVC.Models;
using Store.WebApp.MVC.Models.DTO.User.Request;
using Store.WebApp.MVC.Models.User.Token;
using Store.WebApp.MVC.Services.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Store.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient,
                          IOptions<AppSettings> appSettings)
        {

            httpClient.BaseAddress = new Uri(appSettings.Value.API_AuthorizationUrl);
            _httpClient = httpClient;
        }

        public async Task<UserTokenJwt> CreateUser(CreateUserAuthRequestDTO user)
        {
            var content = GetContent(user);

            var response = await _httpClient.PostAsync("/api/auth/create-user", content);

            if (!HandleResponseErrors(response))
            {
                return new UserTokenJwt
                {
                    ResponseResult = await DeserializeResponse<ResponseResult>(response)
                };
            }

            return await DeserializeResponse<UserTokenJwt>(response);
        }

        public async Task<UserTokenJwt> Login(UserLoginRequestDTO user)
        {
            var content = GetContent(user);

            var response = await _httpClient.PostAsync("/api/auth/login", content);

            if (!HandleResponseErrors(response))
            {
                return new UserTokenJwt
                {
                    ResponseResult = await DeserializeResponse<ResponseResult>(response)
                };
            }

            return await DeserializeResponse<UserTokenJwt>(response);
        }
    }
}
