
namespace SimpleCRUDWebApp.Domain.Abstractions
{
    public class BaseEntity
    {
        #region Properties

        public Guid Id { get; set; } = Guid.NewGuid();

        #endregion Properties
    }
}
