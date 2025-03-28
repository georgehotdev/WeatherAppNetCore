using WeatherAppNetCore.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddInfrastructureDependencies()
    .AddBackgroundServices()
    .AddExternalServices()
    .AddConfiguration()
    .AddApplicationServices();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.ApplyMigrations();
app.SeedData();

app.UseAuthorization();

app.MapControllers();

// FOR DEMO PURPOSES ONLY
app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyOrigin());

app.Run();