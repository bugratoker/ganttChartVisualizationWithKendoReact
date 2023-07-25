using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using singular_project.Configurations;
using singular_project.Data;
using singular_project.Entities;
using singular_project.Repositories;
using singular_project.Repositories.Interfaces;
using singular_project.Services;
using singular_project.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperProfileConfig)); ;
builder.Services.AddScoped<ICSVRepository, CSVRepository>();
builder.Services.AddScoped<ICSVService, CSVService>();
builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"))
                );

builder.Services.AddCors(options =>{
    options.AddPolicy("CorsPolicy", builder =>
    builder.AllowAnyOrigin().
    AllowAnyHeader().
    AllowAnyMethod()
    );
});

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
app.UseCors("CorsPolicy");
app.Run();
