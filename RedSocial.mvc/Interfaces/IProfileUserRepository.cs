using RedSocial.mvc.Models;

namespace RedSocial.mvc.Interfaces
{
    public interface IProfileUserRepository : IRepository<ProfileUser>
    {
        public Task<ProfileUser> GetByIdentityUserId(string identityUserId);
    }
}
