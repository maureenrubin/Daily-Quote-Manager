using DailyQuoteManager.Api;


var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();


var app = builder
    .ConfigureServices()
    .ConfigurePipeLine();



app.Run();
