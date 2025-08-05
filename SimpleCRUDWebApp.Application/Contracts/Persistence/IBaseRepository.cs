namespace DailyQuoteManager.Application.Contracts.Persistence
{
    public interface IBaseRepository<T>
    {
        #region Public Methods

        Task<T> AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);

        Task<T?> DeleteByIdAsync(Guid id);

        Task<IEnumerable<T>> ListAllAsync();

        Task<T?> GetByIdAsync(Guid id);

        #endregion Public Methods
    }
}
