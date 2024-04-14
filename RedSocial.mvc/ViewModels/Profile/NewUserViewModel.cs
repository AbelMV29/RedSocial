using System.ComponentModel.DataAnnotations;

namespace RedSocial.mvc.ViewModels.Profile
{
    public class NewUserViewModel
    {
        [Required(ErrorMessage ="La descripción es necesaria")]
        public string Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}
