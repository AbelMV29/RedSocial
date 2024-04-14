namespace RedSocial.mvc.Interfaces
{
    public interface IDeletableRepository<T>
    {
        Task<bool> Delete(T entity);

    }
}
