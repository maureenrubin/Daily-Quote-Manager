using DailyQuoteManager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DailyQuoteManager.Infrastructure.Configurations
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.AppUserId);

            builder.HasMany(u => u.RefreshTokens)
                   .WithOne(r => r.ApplicationUser)
                   .HasForeignKey(r => r.AppUserId)
                   .OnDelete(DeleteBehavior.Cascade);

       
        }

        #endregion Public Methods
    }
}
