using Store.WebApp.MVC.Models.ViewModels;
using System.Collections.Generic;

namespace Store.WebApp.MVC.Models.DTO.User.Token
{
    public class UserTokenJwt
    {
        public string AccessToken { get; set; }
        public double ExpirationIn { get; set; }
        public UserToken UserToken { get; set; }
        public ResponseResult ResponseResult { get; set; }
    }
    public class UserToken
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }

    public class UserClaim
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }

}
