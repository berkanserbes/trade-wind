using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TradeWind.Infrastructure.Database.DbContexts;
using TradeWind.Modules.Identity.Application;

var builder = WebApplication.CreateBuilder(args);

//ServiceRegistration.ConfigureApplicationServices(builder.Services);
//ServiceRegistration.ConfigureValidators(builder.Services);

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureValidators();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
	app.MapScalarApiReference();
}

//app.UseMiddleware<TradeWind.WebApi.Middlewares.AuthenticationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
