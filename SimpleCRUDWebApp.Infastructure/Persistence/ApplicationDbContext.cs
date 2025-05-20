
using Microsoft.EntityFrameworkCore;

namespace SimpleCRUDWebApp.Infastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {

        #region Public Constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        #endregion Public Constructor


        #region Properties

        // public DbSet<>

        #endregion Properties


        #region Methods

        #endregion Methods
    }
}
