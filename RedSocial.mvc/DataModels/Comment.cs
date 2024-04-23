using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSocial.mvc.DataModels
{
    public class Comment
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public string Body { get; set; }
        public string? ImageUrl { get; set; }
        public virtual IEnumerable<ResponsePostProfile>? ResponsePostProfiles { get; set; }
    }
}