using RedSocial.mvc.Models;

namespace RedSocial.mvc.Interfaces
{
    public interface IPostRepository : IReadableRepository<Post>,IAddableRepository<Post>
    {
        public Task<IEnumerable<Post>> GetAllPostOrderByDateTime();
    }
}
