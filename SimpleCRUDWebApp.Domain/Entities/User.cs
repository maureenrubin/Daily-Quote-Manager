using System.ComponentModel.DataAnnotations;

namespace SimpleCRUDWebApp.Domain.Entities
{
    public class User 
    {
        #region Properties
        
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        [Required]
        public ICollection<UserRole> UserRoles { get; set; }
        #endregion Properties
    }
}
