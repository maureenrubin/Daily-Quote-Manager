
using Microsoft.EntityFrameworkCore;
using SimpleCRUDWebApp.Domain.Entities;

namespace SimpleCRUDWebApp.Domain.Interfaces
{
    public interface IAppDbContext
    {
        #region Properties
        
        DbSet<User> Users { get; }
        DbSet<UserRole> UserRoles { get; }
        DbSet<Role> Roles { get; }

        #endregion Properties

        #region Public Methods
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
 
        #endregion Public Methods 
    }
}
