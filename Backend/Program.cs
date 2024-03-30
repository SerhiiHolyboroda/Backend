using Abackend.Data;
using Abackend.Repositories.Implementation;
using Infrastructure.GenericImplementation;
using Abackend.Repositories.Interface;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Backend.Middleware;
using Microsoft.AspNetCore.Mvc;
using Backend.Errors;
using Backend.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddApplicationServices(builder.Configuration);
 
 

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();


using var scope =  app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<ApplicationDbContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try { 
 //  await context.Database.MigrateAsync();
  // await StoreDataSeed.SeedAsync(context);

}
catch( Exception ex)
{
    logger.LogError(ex, "An error occured during migration");
}


app.Run();
