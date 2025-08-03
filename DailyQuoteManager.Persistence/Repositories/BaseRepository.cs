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

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> ListAllAsync()
        {
            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await dbContext.AddAsync(entity);
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await dbContext.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public async Task<T?> DeleteByIdAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                dbContext.Set<T>().Remove(entity);
            }

            return entity;
        }

        #endregion Public Methods

    }
}
