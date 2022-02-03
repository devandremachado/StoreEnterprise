using System.ComponentModel.DataAnnotations;

namespace Store.WebApp.MVC.Models.DTO.User.Request
{
    public class UserLoginRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
