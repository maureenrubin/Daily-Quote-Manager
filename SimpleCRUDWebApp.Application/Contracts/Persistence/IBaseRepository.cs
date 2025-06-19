namespace DailyQuoteManager.Application.Contracts.Persistence
{
    public interface IBaseRepository<T>
    {
        #region Public Methods

        //retruns single record by id 
        Task<T?> GetByIdAsync(Guid id);

        //returns all records of what type of entity
        Task<IEnumerable<T>> ListAllAsync();

        //Add single entity to the database context 
        Task AddAsync(T entity);

        //Add multiple entities at once
        Task AddRangeAsync(IEnumerable<T> entities);
        
        //Only marks the data as changed, can be changed actually to the db using UnitofWork
        void Update(T entity);

        //removes an entity from context
        void Delete(T entity);

        //removes an entity by its id
        Task<T?> DeleteByIdAsync(Guid id);

        #endregion Public Methods


    }
}
