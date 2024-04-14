using System.ComponentModel.DataAnnotations;

namespace RedSocial.mvc.ViewModels.Account
{
    public class SecondForgotPasswordViewModel
    {
        [Required(ErrorMessage = "El email es requerido"), EmailAddress]
        public string Email { get; set; }
        [Required, RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$",
            ErrorMessage = "La contraseña debe tener [8,15] caracteres (1 especial, 1 mayuscula y 1 minuscula)")]
        [Display(Name = "Contraseña")]
        public string NewPassword { get; set; }
        [Required, RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$",
            ErrorMessage = "La contraseña debe tener [8,15] caracteres (1 especial, 1 mayuscula y 1 minuscula)")]
        [Display(Name = "Nueva contraseña")]
        public string NewPasswordConfirm { get; set; }
        public bool Changed = false;
    }
}
