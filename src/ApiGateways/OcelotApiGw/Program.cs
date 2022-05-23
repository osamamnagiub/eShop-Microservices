using Ocelot.Middleware;
using Ocelot.DependencyInjection;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);

builder.Logging
    .AddConsole()
    .AddDebug();

builder.Services.AddOcelot().AddCacheManager(c => c.WithDictionaryHandle());

var app = builder.Build();
await app.UseOcelot();

app.Run();
