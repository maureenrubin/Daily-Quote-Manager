using DailyQuoteManager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DailyQuoteManager.Persistence.Configurations.UserManagement
{
    public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshTokens>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<RefreshTokens> builder)
        {
            builder.HasKey(r => r.RefreshTokenId);

           
        }

        #endregion Public Methods
    }
}
