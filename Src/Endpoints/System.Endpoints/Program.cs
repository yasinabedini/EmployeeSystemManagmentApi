using Blogger.APIs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Application;
using System.Endpoints;
using System.Endpoints.Abstractions;
using System.Infra.Contexts;
using System.Infra.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
builder.Services.AddApplication();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));

builder.Configuration.AddEnvironmentVariables();

builder.Services.ConfigureMapster();
builder.Services.ConfigureValidator();
builder.Services.ConfigureCors();

builder.Services.AddEndpoints();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapEndpoints();

app.Run();


