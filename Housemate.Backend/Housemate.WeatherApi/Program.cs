using Housemate.Application.Clients.Abstractions;
using Housemate.Application.Context;
using Housemate.Application.Extensions;
using Housemate.Application.Models.Identity;
using Housemate.Application.Services.Abstractions;
using Housemate.Application.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddAzureKeyVault();

builder.AddJwtBearer();

builder.Services.AddControllers();

builder.Services.AddDatabase<IdentityDbContext>(builder.Configuration["IdentityStore:ConnectionString"]!);

builder.Services.AddApplicationService<IWeatherHttpClient>();
builder.Services.AddApplicationService<IWeatherService>();
    
builder.Services.AddApplicationService<IAuthService>();
builder.Services.AddApplicationService<ITokenWriter<ApplicationUser>>();

builder.Services.AddIdentityConfiguration();

builder.Services.AddOptions<OpenWeatherApiSettings>()
    .Bind(builder.Configuration.GetSection(OpenWeatherApiSettings.EnvironmentKey))
    .ValidateOnStart();

builder.Services.AddOptions<JwtSettings>()
    .Bind(builder.Configuration.GetSection(JwtSettings.EnvironmentKey))
    .ValidateOnStart();

var app = builder.Build();

app.MapControllers();
app.Run();