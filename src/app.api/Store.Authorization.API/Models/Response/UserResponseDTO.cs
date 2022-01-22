using Store.Authorization.API.Models.User;

namespace Store.Authorization.API.Models.Response
{
    public class UserResponseLogin
    {
        public string AccesToken { get; set; }
        public double ExpirationIn { get; set; }
        public UserToken UserToken { get; set; }
    }
}
