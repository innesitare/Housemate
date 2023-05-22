using FluentValidation;
using FluentValidation.AspNetCore;
using Housemate.Application.Context;
using Housemate.Application.Extensions;
using Housemate.Application.Filters;
using Housemate.Application.Helpers;
using Housemate.Application.Models.Identity;
using Housemate.Application.Repositories.Abstractions;
using Housemate.Application.Repositories.CachedRepositories;
using Housemate.Application.Services.Abstractions;
using Housemate.Application.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.AddJwtBearer();

builder.Services.AddControllers();
builder.Services.AddMvc();

builder.Services.AddDatabase<ApplicationDbContext>(builder.Configuration["ApplicationStore:ConnectionString"]!);
builder.Services.AddDatabase<IdentityDbContext>(builder.Configuration["IdentityStore:ConnectionString"]!);
builder.Services.AddRedisCache(builder.Configuration["Redis:ConnectionString"]!);

builder.Services.AddApplicationService(typeof(IRepository<>));
builder.Services.AddApplicationService(typeof(ICacheService<>));

builder.Services.AddApplicationService<IHousingTaskService>();

builder.Services.AddApplicationService<IAuthService>();
builder.Services.AddApplicationService<ITokenWriter<ApplicationUser>>();

builder.Services.AddIdentityConfiguration();

builder.Services.AddOptions<JwtSettings>()
    .Bind(builder.Configuration.GetSection("Jwt"))
    .ValidateOnStart();

builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssemblyContaining<IValidationMarker>(ServiceLifetime.Singleton)
    .AddFilter<ValidationFilter>();

builder.Services.Decorate<IHousingTaskRepository, CachedHousingTaskRepository>();

var app = builder.Build();

app.MapControllers();
app.Run();