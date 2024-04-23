using Microsoft.EntityFrameworkCore;
using RedSocial.mvc.Data;
using RedSocial.mvc.DataModels;
using RedSocial.mvc.Interfaces;

namespace RedSocial.mvc.Repository
{
    public class CommentPostProfileRepository : BaseRepository, ICommentPostProfileRepository
    {
        public CommentPostProfileRepository(RedSocialContext context) : base(context)
        {
        }

        public async Task<bool> Add(CommentPostProfile entity)
        {
            await _context.CommentPostProfile.AddAsync(entity);
            return await Save();
        }

        public Task<IEnumerable<CommentPostProfile>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CommentPostProfile> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CommentPostProfile>> GetByIdPost(int idPost)
            => await _context.CommentPostProfile
            .Include(cpf => cpf.Comment)
            .Include(cpf => cpf.ProfileUser)
            .Where(cpf => cpf.PostId == idPost)
            .ToListAsync();   
            }
}
