using Microsoft.EntityFrameworkCore;
using RedSocial.mvc.Data;
using RedSocial.mvc.Interfaces;
using RedSocial.mvc.Models;

namespace RedSocial.mvc.Repository
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public PostRepository(RedSocialContext context) : base(context)
        {
        }

        public async Task<bool> Add(Post entity)
        {
            await _context.Post.AddAsync(entity);
            return await Save();
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            var postList = await _context.Post.Include(p=>p.ProfileUser).ToListAsync();
            return postList;
        }

        public async Task<IEnumerable<Post>> GetAllPostOrderByDateTime()
        {
            var postList = await _context.Post.Include(p => p.ProfileUser).OrderByDescending(p => p.Created).ToListAsync();
            return postList;
        }

        public Task<Post> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
