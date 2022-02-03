using Store.WebApp.MVC.Models.DTO.User.Request;
using Store.WebApp.MVC.Models.User.Token;
using System.Threading.Tasks;

namespace Store.WebApp.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserTokenJwt> Login(UserLoginRequestDTO user);
        Task<UserTokenJwt> CreateUser(CreateUserAuthRequestDTO user);
    }
}
