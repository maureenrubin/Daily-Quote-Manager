using Microsoft.EntityFrameworkCore;
using DailyQuoteManager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DailyQuoteManager.Persistence.Configurations.QuoteConfiguration
{
    public class QuoteConfig : IEntityTypeConfiguration<Quotes>
    {
        #region Public Methods
        
        public void Configure(EntityTypeBuilder<Quotes> builder)
        {
            builder.HasKey(q => q.QuoteId);

            //Quote added by user
            builder.HasOne(q => q.ApplicationUser)
                   .WithMany(u => u.AddedQuotes)
                   .HasForeignKey(q => q.AddedByUserId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(q => q.Category);

        }

        #endregion

    }
}
