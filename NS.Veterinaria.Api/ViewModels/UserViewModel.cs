using NS.Veterinary.Api.DTOs;
using System.ComponentModel.DataAnnotations;

namespace NS.Veterinary.Api.ViewModels
{
    public class RegisterUserViewModel : LoginUserBaseViewModel
    {
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginUserViewModel : LoginUserBaseViewModel { }

    public abstract class LoginUserBaseViewModel : Dto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} esta em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
