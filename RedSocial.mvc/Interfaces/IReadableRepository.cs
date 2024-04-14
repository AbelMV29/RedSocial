namespace RedSocial.mvc.Interfaces
{
    public interface IReadableRepository<T>
    {
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
