using RedSocial.mvc.DataModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RedSocial.mvc.ViewModels.Dashboard;

namespace RedSocial.mvc.ViewModels.Post
{
    public class PostDetailViewModel
    {
        public PostViewModel? PostViewModel { get; set; }
        [Required]
        public CreateCommentViewModel CreateCommentViewModel { get; set; } 
        public List<CommentViewModel>? CommentViewModels { get; set; }
    }

    public class CreateCommentViewModel
    {
        [Required]
        public string Body { get; set; }
        public IFormFile? Image { get; set; }
    }

    public class CommentViewModel
    {
        public string FullNameProfile { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string DeltaTime { get; set; }
        public string Body { get; set; }
        public string? ImageUrl { get; set; }
    }
}
