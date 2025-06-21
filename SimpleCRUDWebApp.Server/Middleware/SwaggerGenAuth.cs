using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace DailyQuoteManager.Api.Middleware
{
    public static class SwaggerGenAuth
    {

        #region Public Methods 

        public static IServiceCollection AddSwaggerAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {

                var securityDefinition = new OpenApiSecurityScheme
               {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter Bearer {Access Token} to authenticate"
               };

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityDefinition);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        }
                    },
                    new string[] {}

                }
            });


            });

            return services;

            
        }

        #endregion Public Methods
    }
}
