namespace RedSocial.mvc.Interfaces
{
    public interface IRepository<T> : IReadableRepository<T>,IAddableRepository<T>,IDeletableRepository<T>,IUpdatableRepository<T>
    {
    }
}
