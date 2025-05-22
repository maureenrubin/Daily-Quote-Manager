using Microsoft.EntityFrameworkCore;
using SimpleCRUDWebApp.Domain.Entities;

namespace SimpleCRUDWebApp.Domain.Commons.Interfaces
{
    public interface IApplicationDbContext
    {
        #region Properties

        DbSet<User> User { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        #endregion Properties

    }
}
