using DailyQuoteManager.Domain.Entities;

namespace DailyQuoteManager.Application.Contracts.Persistence
{
    public interface IUserRepository
    {

        #region Public Methods

        Task<ApplicationUser?> GetByEmailAsync(string email);

        Task<ApplicationUser?> GetUserByIdAsync(Guid appUserId);
        
       // Task<IEnumerable<ApplicationUser>> GetAllUserAsync();

        //Task<IEnumerable<string>> GetAllUserEmailsAsync();

        Task AddAsync(ApplicationUser user);

        Task UpdateAsync(ApplicationUser user);

        Task DeleteAsync(Guid AppUserId);



        #endregion Public Methods

    }
}
