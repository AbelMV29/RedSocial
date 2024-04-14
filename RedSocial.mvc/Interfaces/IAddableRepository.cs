namespace RedSocial.mvc.Interfaces
{
    public interface IAddableRepository<T>
    {
        Task<bool> Add(T entity);
    }
}
