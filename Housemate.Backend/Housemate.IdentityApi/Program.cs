using Housemate.Application.Context;
using Housemate.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMvc();

builder.Services.AddDatabase<ApplicationDbContext>(builder.Configuration["ApplicationStore:ConnectionString"]!);
builder.Services.AddDatabase<IdentityDbContext>(builder.Configuration["IdentityStore:ConnectionString"]!);

var app = builder.Build();

app.MapControllers();

app.Run();