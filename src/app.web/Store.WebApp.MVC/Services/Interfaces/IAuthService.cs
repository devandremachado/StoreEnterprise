using Store.WebApp.MVC.Models.User.Request;
using Store.WebApp.MVC.Models.User.Token;
using System.Threading.Tasks;

namespace Store.WebApp.MVC.Services.Interfaces
{
    public interface IAuthService
    {
        Task<UserTokenJwt> Login(UserLoginDTO user);
        Task<UserTokenJwt> CreateUser(UserRequestDTO user);
    }
}
