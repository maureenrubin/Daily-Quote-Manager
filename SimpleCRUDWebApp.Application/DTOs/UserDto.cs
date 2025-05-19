using SimpleCRUDWebApp.Domain
namespace SimpleCRUDWebApp.Application.DTOs
{
    public class UserDto 
    {
        #region Properties
        
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Gender { get; set; } = string.Empty;

        #endregion Properties
    }
}
