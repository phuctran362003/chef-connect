﻿using ChefConnect.API.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication(); // Frist
app.UseAuthorization(); // Second


app.MigrateDatabases();

app.MapControllers();

app.Run();
