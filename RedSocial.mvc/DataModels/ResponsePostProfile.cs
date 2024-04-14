using System.ComponentModel.DataAnnotations.Schema;

namespace RedSocial.mvc.Models
{
    public class ResponsePostProfile
    {
        public int ResponseId { get; set; }
        public int ProfileUserId { get; set; }
        public int? CommentId { get; set; }
        [ForeignKey("CommentId")]
        public virtual Comment? Comment { get; set; }
        [ForeignKey("ResponseId")]
        public virtual Response Response { get; set; }
        [ForeignKey("ProfileUserId")]
        public virtual ProfileUser ProfileUser { get; set; }
        public DateTime Created { get; set; }
    }
}
