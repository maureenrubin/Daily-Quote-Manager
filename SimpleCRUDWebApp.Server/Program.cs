//using DailyQuoteManager.Application.Common.Interfaces;
using DailyQuoteManager.Infrastructure.Middleware;



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





var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();


// CORS Configuration
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowBlazorApp", policy =>
    {
        policy
        .WithOrigins("https://localhost:7053")
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

// Enable Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
