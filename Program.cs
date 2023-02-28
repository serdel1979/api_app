using api_app;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var StartUp = new StartUp(builder.Configuration);

StartUp.ConfigureServices(builder.Services);

var app = builder.Build();

StartUp.Configure(app, app.Environment);


app.Run();
