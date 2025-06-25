using DailyQuoteManager.Persistence.DependencyInjections;
using ApiExceptionMiddleware = DailyQuoteManager.Api.Middleware.ExceptionMiddleware;
using DailyQuoteManager.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using DailyQuoteManager.Api;


var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();


var app = builder
    .ConfigureServices()
    .ConfigurePipeLine();



app.Run();
