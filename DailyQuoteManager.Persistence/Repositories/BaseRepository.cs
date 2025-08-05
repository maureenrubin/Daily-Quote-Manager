using DailyQuoteManager.Application.Contracts.Persistence;
using DailyQuoteManager.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DailyQuoteManager.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        #region Fields

        protected readonly AppDbContext dbContext;

        #endregion Fields

        public BaseRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region Public Methods

        public virtual async Task<T> AddAsync(T entity)
        {
            var result = await dbContext.Set<T>().AddAsync(entity);
            return result.Entity;
        }
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await dbContext.AddRangeAsync(entities);
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return entity;
        }

        public virtual async Task<T?> DeleteByIdAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            if (entity != null)
                dbContext.Set<T>().Remove(entity);

            return entity;
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync() =>
            await dbContext.Set<T>().ToListAsync();

        public virtual async Task<T?> GetByIdAsync(Guid id) =>
            await dbContext.Set<T>().FindAsync(id);

        #endregion Public Methods

    }
}
