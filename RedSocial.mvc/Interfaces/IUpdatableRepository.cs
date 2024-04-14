namespace RedSocial.mvc.Interfaces
{
    public interface IUpdatableRepository<T>
    {
        Task<bool> Update(T entity);

    }
}
