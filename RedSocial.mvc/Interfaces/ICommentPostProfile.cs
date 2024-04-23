using RedSocial.mvc.DataModels;

namespace RedSocial.mvc.Interfaces
{
    public interface ICommentPostProfile : IReadableRepository<CommentPostProfile>, IAddableRepository<CommentPostProfile>
    {
        public Task<IEnumerable<CommentPostProfile>> GetByIdPost(int postId);
    }
}
