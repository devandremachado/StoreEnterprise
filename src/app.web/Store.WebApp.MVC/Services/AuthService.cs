using Store.WebApp.MVC.Models;
using Store.WebApp.MVC.Models.User.Request;
using Store.WebApp.MVC.Models.User.Token;
using Store.WebApp.MVC.Services.Interfaces;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserTokenJwt> CreateUser(UserRequestDTO user)
        {
            var content = new StringContent(
               JsonSerializer.Serialize(user),
               Encoding.UTF8,
               "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44325/api/auth/create-user", content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (!HandleResponseErrors(response))
            {
                return new UserTokenJwt
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<UserTokenJwt>(await response.Content.ReadAsStringAsync(), options);
        }

        public async Task<UserTokenJwt> Login(UserLoginDTO user)
        {
            var content = new StringContent(
               JsonSerializer.Serialize(user),
               Encoding.UTF8,
               "application/json");

            var response = await _httpClient.PostAsync("https://localhost:44325/api/auth/login", content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (!HandleResponseErrors(response))
            {
                return new UserTokenJwt
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), options)
                };
            }

            return JsonSerializer.Deserialize<UserTokenJwt>(await response.Content.ReadAsStringAsync(), options);
        }
    }
}
