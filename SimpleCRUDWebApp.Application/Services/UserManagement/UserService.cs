using DailyQuoteManager.Application.Interfaces.UserManagement;
using DailyQuoteManager.Domain.Entities;
using DailyQuoteManager.Domain.Interfaces;

namespace DailyQuoteManager.Application.Services.UserManagement
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IUserRepository _userRepository;

        #endregion Fields

        #region Public Contructors 

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion Public Contructors

        
        
        #region Public Methods 

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }


        public async Task<ApplicationUser> GetUserByIdAsync(Guid appUserId)
        {
            return await _userRepository.GetUserByIdAsync(appUserId);
        }


        #endregion Public Methods

    }
}
