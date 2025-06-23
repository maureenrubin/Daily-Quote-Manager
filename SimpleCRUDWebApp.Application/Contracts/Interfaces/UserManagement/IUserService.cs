using DailyQuoteManager.Domain.Entities;

namespace DailyQuoteManager.Application.Contracts.Interfaces.UserManagement
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(Guid appUserId);

        Task<ApplicationUser> GetUserByEmailAsync(string email);


    }
}
