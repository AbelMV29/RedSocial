using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedSocial.mvc.ViewModels.Dashboard
{
    public class PostDashboardViewModel
    {
        public List<PostViewModel>? Posts { get; set; }
        [Required]
        public CreatePostViewModel CreatePost { get; set; }
    }

    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }   
        public string Body { get; set; }
        public string? ImageUrl { get; set; }
        public string CreationTimeDelta { get; set; }
        public ProfileUserViewModel ProfileUserVM { get; set; }
    }

    public class ProfileUserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string ImageUrl { get; set; }
    }
    
    public class CreatePostViewModel
    {
        [Required,StringLength(50)]
        public string Title { get; set; }
        [Required,StringLength(200)]
        public string Body { get; set; }
        public IFormFile? Image { get; set; }
    }
}