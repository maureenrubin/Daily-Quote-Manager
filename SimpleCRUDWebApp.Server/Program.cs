using DailyQuoteManager.Infrastructure.DependencyInjections;
using ApiExceptionMiddleware = DailyQuoteManager.Api.Middleware.ExceptionMiddleware;
using InfraExceptionMiddleware = DailyQuoteManager.Infrastructure.Middleware.ExceptionMiddleware;
using DailyQuoteManager.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

//Add OpenAPI and Swagger for documentation
builder.Services.AddOpenApi();
builder.Services.AddSwaggerAuth();

//Add JWT Authentication
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:SecretKey"]!)),

            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWTSettingds:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWTSettings:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = ClaimTypes.Role
        };
    });

builder.Services.AddServices(builder.Configuration);



var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();


// CORS Configuration
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowBlazorApp", policy =>
    {
        policy.WithOrigins("https://localhost:7053")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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

app.Run();
