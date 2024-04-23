using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RedSocial.mvc.ViewModels.Profile
{
    public class ProfileDetailViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; } = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQH23wl-q67cob4TWDwiHMie9RaSfX5A7Vm3tvs39u2KQ&s";
        public string? Description {  get; set; }
        public DateTime Created {  get; set; }
        //public virtual IEnumerable<Request>? Requests { get; set; }
    }
}