using RedSocial.mvc.Validations;
using System.ComponentModel.DataAnnotations;

namespace RedSocial.mvc.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El email es requerido"), EmailAddress(ErrorMessage = "El email no es adecuado")]
        public string Email { get; set; }
        [Required, DataType(DataType.Password, ErrorMessage = "La contraseña debe tener [8,15] caracteres (1 especial, 1 mayuscula y 1 minuscula)")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        public string? returnUrl { get; set; }
        [Display(Name ="Recordar contraseña")]
        public bool RememberMe {  get; set; }

    }
}
