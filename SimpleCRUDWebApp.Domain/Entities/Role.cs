

using System.ComponentModel.DataAnnotations;

namespace SimpleCRUDWebApp.Domain.Entities
{
    public class Role
    {
        #region Properties

        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<UserRole> UserRoles { get; set; }
        #endregion Properties 
    }
}
