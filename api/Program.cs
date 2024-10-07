using System.Text;
using api.Data;
using api.Handlers;
using api.Repositories.Interfaces;
using api.Routes;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

Console.OutputEncoding = Encoding.UTF8;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepo, UserHandler>();
builder.Services.AddScoped<IEventRepo, api.Handlers.EventHandler>();

builder.Services.AddSingleton(provider =>
{
    var optionsBuilder = new DbContextOptionsBuilder<ApiContext>();
    optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string not found"), sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
        sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    });

    return new ApiContext(optionsBuilder.Options);
});

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add Health Checks
builder.Services.AddHealthChecks();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Event Management System", Version = "v1" });
});

var app = builder.Build();
app.MapHealthChecks("/healthz");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using var scope = app.Services.CreateScope();
// User routes
app.MapUserRoutes(scope.ServiceProvider.GetRequiredService<IUserRepo>());
// Event routes
app.MapEventRoutes(scope.ServiceProvider.GetRequiredService<IEventRepo>());

SeedData.SeedDatabase(app);

app.Run();