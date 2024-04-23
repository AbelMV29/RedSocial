using RedSocial.mvc.Data;
using RedSocial.mvc.DataModels;
using RedSocial.mvc.Interfaces;

namespace RedSocial.mvc.Repository
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(RedSocialContext context) : base(context)
        {
        }

        public async Task<bool> Add(Comment entity)
        {
            await _context.AddAsync(entity);
            return await Save();
        }
    }
}
