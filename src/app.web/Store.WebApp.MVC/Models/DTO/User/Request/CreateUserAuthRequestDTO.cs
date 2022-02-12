using Store.WebApp.MVC.Extensions.AnnotationAttributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Store.WebApp.MVC.Models.DTO.User.Request
{
    public class CreateUserAuthRequestDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Nome Completo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("CPF")]
        [CPF]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [DisplayName("Confirme sua senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmationPassword { get; set; }
    }
}
