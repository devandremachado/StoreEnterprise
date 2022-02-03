using System.ComponentModel.DataAnnotations;

namespace Store.WebApp.MVC.Models.DTO.User.Request
{
    public class CreateUserAuthRequestDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmationPassword { get; set; }
    }
}
