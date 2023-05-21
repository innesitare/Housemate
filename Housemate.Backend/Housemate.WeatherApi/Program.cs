using FluentValidation;
using FluentValidation.AspNetCore;
using Housemate.Application.Clients.Abstractions;
using Housemate.Application.Context;
using Housemate.Application.Extensions;
using Housemate.Application.Filters;
using Housemate.Application.Helpers;
using Housemate.Application.Models.Identity;
using Housemate.Application.Services.Abstractions;
using Housemate.Application.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.AddJwtBearer();

builder.Services.AddControllers();

builder.Services.AddDatabase<IdentityDbContext>(builder.Configuration["IdentityStore:ConnectionString"]!);

builder.Services.AddApplicationService<IWeatherHttpClient>();
builder.Services.AddApplicationService<IWeatherService>();

builder.Services.AddApplicationService<IAuthService>();
builder.Services.AddApplicationService<ITokenWriter<ApplicationUser>>();

builder.Services.AddIdentityConfiguration();

builder.Services.AddOptions<JwtSettings>()
    .Bind(builder.Configuration.GetSection("Jwt"))
    .ValidateOnStart();

builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<IApplicationMarker>()
    .AddFilter<ValidationFilter>();

var app = builder.Build();

app.MapControllers();
app.Run();