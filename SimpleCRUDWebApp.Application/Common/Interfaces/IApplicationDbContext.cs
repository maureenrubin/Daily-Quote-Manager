using Microsoft.EntityFrameworkCore;
using SimpleCRUDWebApp.Domain.Entities;

namespace SimpleCRUDWebApp.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
            #region Properties

            DbSet<User> User { get; }

            #endregion Properties

    }

}

