using API.Data;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Middlewares;
using API.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseNpgsql(ConnectionStringProvider.GetConnectionString(builder.Configuration, builder.Environment));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

// Errors
app.UseMiddleware<ExceptionMiddleware>();

// CORS
app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("https://localhost:4200"));

// Auth
app.UseAuthentication(); // do you have a valid token?
app.UseAuthorization(); // okay you have a valid token, now what are you allowed to do?

// Look for index.html, and wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

// Endpoints
app.MapControllers();
app.MapHub<PresenceHub>("hubs/presence");
app.MapHub<MessageHub>("hubs/message");
app.MapFallbackToController("Index", "FallBack");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    await context.Database.MigrateAsync();
    await Seed.ClearConnections(context);
    await Seed.SeedUsers(userManager, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}

app.Run();