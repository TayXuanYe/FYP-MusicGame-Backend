// Program.cs

using FYP_MusicGame_Backend.Data;
using Microsoft.EntityFrameworkCore;
using FYP_MusicGame_Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FYP_MusicGame_Backend.Models;

var builder = WebApplication.CreateBuilder(args);

// get connection string from configuration file
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// configuring authentication services
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

// configuring JWT Bearer authentication
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                builder.Configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException("Jwt:SecretKey is not configured.")
            )
        )
    };
});
// configuring database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Register repositories
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBugReportRepository, BugReportRepository>();
builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
builder.Services.AddScoped<IChartRepository, ChartRepository>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBugReportService, BugReportService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();

// Add controllers services
builder.Services.AddControllers();

// If the --seed argument is provided, execute the seeding command and exit
if (args.Length > 0 && args[0].Equals("--seed", StringComparison.OrdinalIgnoreCase))
{
    var host = builder.Build();

    EnsureDatabaseExists(host);

    ExecuteSeedingCommand(host);
    return;
}

var app = builder.Build();

app.UseAuthentication(); 
app.UseAuthorization();
app.MapControllers();

app.Run();

void ExecuteSeedingCommand(IHost host)
{
    Console.WriteLine("Executing Chart Data Seeding Command...");
    
    using (var scope = host.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        TempLoadChartData(scope);
    }
    Console.WriteLine("Seeding completed successfully. Application exiting.");
}

void TempLoadChartData(IServiceScope scope)
{
    Console.WriteLine("Loading chart data from text files...");
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // read the txt file in the chart folder
    var projectRootDirectory = Directory.GetCurrentDirectory(); 
    var chartsDirectory = Path.Combine(projectRootDirectory, "Charts"); 
    Console.WriteLine($"Charts directory: {chartsDirectory}");
    if (Directory.Exists(chartsDirectory))
    {
        var chartFiles = Directory.GetFiles(chartsDirectory, "*.txt");
        foreach (string filePath in Directory.EnumerateFiles(chartsDirectory, "data.txt", SearchOption.AllDirectories))
        {
            Console.WriteLine($"Processing file: {filePath}");
            var chartsData = ParseDataSection(filePath, "Data");
            int chartId = 0;
            int songId = 0;
            string title = "";
            string artist = "";
            string difficulty = "";
            float level = 0;
            string designer = "";

            foreach (string line in chartsData)
            {
                string[] parts = line.Split(':');
                string key = parts[0].Trim();
                string value = parts[1].Trim();
                switch (key)
                {
                    case "ChartId":
                        chartId = int.Parse(value);
                        break;
                    case "SongId":
                        songId = int.Parse(value);
                        break;
                    case "Title":
                        title = value;
                        break;
                    case "Artist":
                        artist = value;
                        break;
                    case "Difficulty":
                        difficulty = value;
                        break;
                    case "Level":
                        level = float.Parse(value);
                        break;
                    case "Designer":
                        designer = value;
                        break;
                    default:
                        break;
                }
            }

            // Check is the song exists?
            Song? song = dbContext.Songs.FirstOrDefault(s => s.Id == songId);
            if (song == null)
            {
                song = new Song
                {
                    Id = songId,
                    Title = title,
                    Artist = artist,
                    Bpm = 120, // default value
                    UploadedAt = DateTime.UtcNow
                };
                dbContext.Songs.Add(song);
                dbContext.SaveChanges();
            }

            // Check if the chart exists?
            Chart? chart = dbContext.Charts.FirstOrDefault(c => c.Id == chartId);
            if (chart == null)
            {
                chart = new Chart
                {
                    Id = chartId,
                    SongId = songId,
                    Artist = artist,
                    Difficulty = difficulty,
                    Level = level,
                    Designer = designer,
                    UploadedAt = DateTime.UtcNow,
                    Song = song
                };
                dbContext.Charts.Add(chart);
                dbContext.SaveChanges();
            }
        }
    }
}

List<string> ParseDataSection(string filePath, string sectionName)
{
    List<string> sectionLines = new List<string>();
    bool inTargetSection = false;

    foreach (string line in File.ReadLines(filePath))
    {
        string trimmedLine = line.Trim();

        if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
        {
            string currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2);

            inTargetSection = (currentSection == sectionName);

            // If we've just entered the target section, skip the section header line
            if (inTargetSection)
            {
                continue; // Skip the header line
            }
        }

        // If we're in the target section, add the line to the list
        if (inTargetSection && !string.IsNullOrWhiteSpace(trimmedLine))
        {
            sectionLines.Add(trimmedLine);
        }
    }

    return sectionLines;
}

void EnsureDatabaseExists(WebApplication application)
{
    using (var scope = application.Services.CreateScope())
    {
        try
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            bool created = dbContext.Database.EnsureCreated();
            if (created)
            {
                Console.WriteLine("Database file 'database.db' created successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Critical error: Failed to ensure database creation. Details: {ex.Message}");
        }
    }
}