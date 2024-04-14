using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedSocial.mvc.Models
{
    public class Response
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResponseId { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
        public int? ResponseParentId { get; set; }
        [ForeignKey("ResponseParentId")]
        public virtual Response? ResponseParent { get; set; }    
        public virtual IEnumerable<Response>? ResponseChilds { get; set; }
    }
}
