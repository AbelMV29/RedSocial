using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RedSocial.mvc.DataModels
{
    public class ProfileUser
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfileUserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; } = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQH23wl-q67cob4TWDwiHMie9RaSfX5A7Vm3tvs39u2KQ&s";
        public string? Description {  get; set; }
        public DateTime Created {  get; set; }
        public bool IsComplete { get; set; } = false;
        public string IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public virtual IdentityUser IdentityUser { get; set; }
        public virtual IEnumerable<Post>? Posts { get; set; }    
        public virtual IEnumerable<CommentPostProfile>? CommentPostProfiles { get; set; }
        public virtual IEnumerable<ResponsePostProfile>? ResponsePostProfiles { get; set; }
        //public virtual IEnumerable<Request>? Requests { get; set; }

    }
}
