using SimpleCRUDWebApp.Domain.Abstractions;

namespace SimpleCRUDWebApp.Domain.Entities
{
    public class User : BaseEntity
    {
        #region Properties

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; }

        #endregion Properties
    }
}
