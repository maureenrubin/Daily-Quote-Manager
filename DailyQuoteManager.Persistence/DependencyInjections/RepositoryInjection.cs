using DailyQuoteManager.Application.Contracts.Persistence;
using DailyQuoteManager.Domain.Interfaces;
using DailyQuoteManager.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DailyQuoteManager.Infrastructure.DependencyInjections
{
    public static class RepositoryInjection
    {
        #region Public Methods

        public static IServiceCollection AddPersistenceRepositories(this IServiceCollection services)
        {

            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQuotesRepository, QuotesRepository>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
           
            return services;
        }


        #endregion Public Methods


    }
}
