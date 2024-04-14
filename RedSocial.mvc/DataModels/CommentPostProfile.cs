using System.ComponentModel.DataAnnotations.Schema;

namespace RedSocial.mvc.Models
{
    public class CommentPostProfile
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int ProfileUserId { get; set; }
        public DateTime Created {  get; set; }  
        [ForeignKey("CommentId")]
        public virtual Comment Comment { get; set; }
        [ForeignKey("PostId")]
        public virtual Post Post { get; set; }
        [ForeignKey("ProfileUserId")]
        public virtual ProfileUser ProfileUser { get; set; }
    }
}