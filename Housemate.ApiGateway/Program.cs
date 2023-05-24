using Housemate.ApiGateway.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Kubernetes;
using Ocelot.Provider.Polly;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.AddJwtBearer();

builder.Configuration
    .AddJsonFile("ocelot.json", false, true)
    .AddAzureKeyVault();

builder.Services
    .AddOcelot(builder.Configuration)
    .AddPolly()
    .AddKubernetes();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();
app.Run();