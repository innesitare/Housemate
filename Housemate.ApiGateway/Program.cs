using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Polly;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Configuration
    .AddJsonFile("ocelot.json", false, true)
    .AddEnvironmentVariables();

builder.Services
    .AddOcelot(builder.Configuration)
    .AddPolly();

var app = builder.Build();

await app.UseOcelot();
app.Run();