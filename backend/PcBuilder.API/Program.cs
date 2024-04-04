using Microsoft.EntityFrameworkCore;
using PcBuilder.BL.Servises;
using PcBuilder.Core.Abstractions;
using PcBuilder.DAL.MySQL;
using PcBuilder.DAL.MySQL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString(nameof(BuilderDBContext));
builder.Services.AddDbContext<BuilderDBContext>(
    options =>
    {
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
    );

builder.Services.AddScoped<IComponentTypeServise, ComponentTypeServise>();
builder.Services.AddScoped<IComponentTypeRepository, ComponentTypeRepository>();
builder.Services.AddScoped<IComponentServise, ComponentServise>();
builder.Services.AddScoped<IComponentRepository, ComponentRepository>();
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

app.Run();
