namespace DailyQuoteManager.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync();
    }
}
