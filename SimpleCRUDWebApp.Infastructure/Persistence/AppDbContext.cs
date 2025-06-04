
using Microsoft.EntityFrameworkCore;
using SimpleCRUDWebApp.Domain.Entities;
using SimpleCRUDWebApp.Domain.Interfaces;

namespace SimpleCRUDWebApp.Infastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {

        #region Public Constructor

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        #endregion Public Constructor


        #region Properties

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        #endregion Properties


        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(e => e.User)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Role)
                .WithMany(e => e.UserRoles)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            });



            base.OnModelCreating(modelBuilder);
            
        }

        #endregion Protected Methods
    }
}
