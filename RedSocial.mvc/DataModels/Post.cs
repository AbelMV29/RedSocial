using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RedSocial.mvc.DataModels
{
    public class Post
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostId { get; set; }
        public string Title { get; set; }   
        public string Body { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime Created {  get; set; }
        public int ProfileUserId { get; set; }
        [ForeignKey("ProfileUserId")]
        public virtual ProfileUser ProfileUser { get; set; }
        public virtual IEnumerable<PostCategory>? PostCategories { get; set; }
        public virtual IEnumerable<CommentPostProfile>? CommentPostProfiles { get; set; }

    }
}
