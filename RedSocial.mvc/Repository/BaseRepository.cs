using RedSocial.mvc.Data;

namespace RedSocial.mvc.Repository
{
    public class BaseRepository
    {
        protected readonly RedSocialContext _context;
        public BaseRepository(RedSocialContext context)
        {
            _context = context;
        }
        protected async Task<bool> Save()
        {
            var resultSave = await _context.SaveChangesAsync();
            return resultSave > 0 ? true : false;
        }
    }
}
