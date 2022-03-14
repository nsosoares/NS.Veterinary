namespace NS.Veterinary.Api.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}
