using SimpleCRUDWebApp.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using SimpleCRUDWebApp.Domain.Entities;

namespace SimpleCRUDWebApp.Infastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        #region Public Constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        #endregion Public Constructor


        #region Properties

        public DbSet<User> User => Set<User>();

        #endregion Properties


        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        #endregion Protected Methods
    }
}
