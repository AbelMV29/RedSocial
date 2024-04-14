using RedSocial.mvc.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RedSocial.mvc.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="El Nombre es requerido"),Display(Name ="Nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El Apellido es requerido"), Display(Name = "Apellido")]
        public string LastName { get; set; }
        [Required(ErrorMessage ="El nombre de usuario es requerido"),Display(Name ="Nombre de usuario")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="El email es requerido"),EmailAddress(ErrorMessage ="El email no es adecuado")]
        public string Email { get; set; }
        [Required, RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$",
            ErrorMessage = "La contraseña debe tener [8,15] caracteres (1 especial, 1 mayuscula y 1 minuscula)")]
        [Display(Name ="Contraseña")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirmar contraseña")]
        [Compare(nameof(Password),ErrorMessage ="Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
        public string? returnUrl { get; set; }
    }
}
