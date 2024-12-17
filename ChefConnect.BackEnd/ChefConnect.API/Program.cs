using ChefConnect.API.Startup;
using ChefConnect.Infrastructure.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Ensure database is seeded
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        InitialDatabase.Seed(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error while seeding the database: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
