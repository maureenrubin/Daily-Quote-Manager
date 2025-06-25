using DailyQuoteManager.Api.Middleware;
using DailyQuoteManager.Infrastructure.Extensions;
using DailyQuoteManager.Application.DependencyInjections;
using DailyQuoteManager.Persistence.DependencyInjections;

namespace DailyQuoteManager.Api
{
    public static class Startup
    {
        #region Public Methods

        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddJwtAuthentication(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationService(builder.Configuration);

            builder.Services.AddAuthorization();
            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();


            var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
        
            // CORS Configuration
            builder.Services.AddCors(policy =>
            {
                policy.AddPolicy("AllowBlazorApp", policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerAuth();

            return builder.Build();
        }

        public static WebApplication ConfigurePipeLine(this WebApplication app)
        {
            app.UseCors("open");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }


            app.UseStaticFiles();
            app.UseCors("AllowBlazorApp");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();


            return app;
        }



        #endregion Public Methods

    }
}
