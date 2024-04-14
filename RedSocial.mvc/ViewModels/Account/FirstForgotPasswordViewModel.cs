using System.ComponentModel.DataAnnotations;

namespace RedSocial.mvc.ViewModels.Account
{
    public class FirstForgotPasswordViewModel
    {
        [Required(ErrorMessage = "El email es requerido"), EmailAddress]
        public string Email { get; set; }
    }
}
