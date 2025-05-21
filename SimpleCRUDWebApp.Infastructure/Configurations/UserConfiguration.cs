

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleCRUDWebApp.Domain.Entities;

namespace SimpleCRUDWebApp.Infastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        #region Public Methods

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
        }

        #endregion Public Methods
    }
}
