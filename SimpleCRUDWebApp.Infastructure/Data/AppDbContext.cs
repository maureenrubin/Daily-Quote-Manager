using DailyQuoteManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DailyQuoteManager.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        #region Fields



        #endregion Fields

        
        
        #region Public Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options ) : base (options)
        {

        }
        #endregion Public Constructors


        #region Properties

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Quotes> Quotes { get; set; }

        #endregion Properties

    }
}
