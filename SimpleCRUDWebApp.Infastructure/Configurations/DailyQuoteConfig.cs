using DailyQuoteManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyQuoteManager.Infrastructure.Configurations
{
    public class DailyQuoteConfig : IEntityTypeConfiguration<DailyQuote>
    {
        #region Public Methods
        
        public void Configure(EntityTypeBuilder<DailyQuote> builder)
        {
            // Primary Key
            builder.HasKey(dq => dq.DailyQuoteId);

            // Relationships
            builder.HasOne(dq => dq.User)
                   .WithMany(u => u.DailyQuotes)
                   .HasForeignKey(dq => dq.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dq => dq.Quote)
                   .WithMany(q => q.DailyQuotes)
                   .HasForeignKey(dq => dq.QuoteId)
                   .OnDelete(DeleteBehavior.Restrict);

            // one daily quote per user per date
            builder.HasIndex(dq => new { dq.UserId, dq.DateShown }).IsUnique();

            // Configure DateShown as Date only (if using SQL Server)
            builder.Property(dq => dq.DateShown).HasColumnType("date");
        }

        #endregion

    }
}
