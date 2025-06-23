using DailyQuoteManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyQuoteManager.Persistence.Configurations.QuoteConfiguration
{
    public class FavoriteQuoteConfig : IEntityTypeConfiguration<FavoriteQuote>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<FavoriteQuote> builder)
        {
            // Primary Key
            builder.HasKey(fq => fq.FavoriteQuoteId);


            // Each favorite quote belongs to one user
            builder.HasOne(fq => fq.User)
                   .WithMany(u => u.FavoriteQuotes)
                   .HasForeignKey(fq => fq.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Each favorite quote refers to one quote
            builder.HasOne(fq => fq.Quote)
                   .WithMany(q => q.FavoritedByUsers)
                   .HasForeignKey(fq => fq.QuoteId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Unique index to prevent a user from favoriting the same quote multiple times
            builder.HasIndex(fq => new { fq.UserId, fq.QuoteId }).IsUnique();
        }

        #endregion

    }
}
