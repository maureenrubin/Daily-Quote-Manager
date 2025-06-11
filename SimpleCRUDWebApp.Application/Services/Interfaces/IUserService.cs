using DailyQuoteManager.Domain.Entities;

namespace DailyQuoteManager.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(Guid appUserId);

        Task<ApplicationUser> GetUserByEmailAsync(string email);


    }
}
