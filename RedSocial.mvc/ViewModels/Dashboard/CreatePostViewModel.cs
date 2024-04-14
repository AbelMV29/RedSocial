using RedSocial.mvc.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RedSocial.mvc.ViewModels.Dashboard
{
    public class CreatePostViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public IFormFile? Image { get; set; }
    }
}
