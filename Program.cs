// Program.cs

using FYP_MusicGame_Backend.Data;
using Microsoft.EntityFrameworkCore;
using FYP_MusicGame_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// get connection string from configuration file
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// configuring database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Register repositories
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBugReportRepository, BugReportRepository>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBugReportService, BugReportService>();

// Add controllers services
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();

app.Run();