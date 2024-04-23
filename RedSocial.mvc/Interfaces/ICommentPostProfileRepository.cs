using RedSocial.mvc.DataModels;

namespace RedSocial.mvc.Interfaces
{
    public interface ICommentPostProfileRepository : IReadableRepository<CommentPostProfile>,IAddableRepository<CommentPostProfile>
    {
        public Task<IEnumerable<CommentPostProfile>> GetByIdPost(int id);
    }
}
