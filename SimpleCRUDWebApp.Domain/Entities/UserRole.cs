
namespace SimpleCRUDWebApp.Domain.Entities
{
    public class UserRole
    {
        #region Properties

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid RoleId { get; set; }
        public Role Role { get; set; } = null!;
        #endregion Properties
    }
}
