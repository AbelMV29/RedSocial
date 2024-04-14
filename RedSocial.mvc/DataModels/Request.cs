using System.ComponentModel.DataAnnotations.Schema;

namespace RedSocial.mvc.Models
{
    public class Request
    {
        public int ProfileUserId { get; set; }
        public int FriendId { get; set; }
        public DateTime Created { get; set; }
        public State StateRequest { get; set; }
        public ProfileUser ProfileUser { get; set; }
        public ProfileUser FriendUser { get; set; }
    }

    public enum State
    {
        Rejected = 0,
        Pending = 1,
        Accepted =2
    }
}
