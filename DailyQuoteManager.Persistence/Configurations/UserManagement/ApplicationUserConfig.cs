using DailyQuoteManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyQuoteManager.Persistence.Configurations.UserManagement
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.AppUserId);

            //Refresh Token
            builder.HasMany(u => u.RefreshTokens)
                   .WithOne(r => r.ApplicationUser)
                   .HasForeignKey(r => r.AppUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            //Quotes
            builder.HasMany(u => u.AddedQuotes)
                   .WithOne(q => q.ApplicationUser)
                   .HasForeignKey(q => q.AddedByUserId)
                   .OnDelete(DeleteBehavior.SetNull);

            //FavoriteQuotes
            builder.HasMany(u => u.FavoriteQuotes)
                   .WithOne(f => f.ApplicationUser)
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            //DailyQuotes
            builder.HasMany(u => u.DailyQuotes)
                   .WithOne(d => d.ApplicationUser)
                   .HasForeignKey(d => d.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
       
        }

        #endregion Public Methods
    }
}
