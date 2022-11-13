using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Demo.Data;
using Demo.Controllers;
using Demo.Data;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DemoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoContext") ?? throw new InvalidOperationException("Connection string 'DemoContext' not found.")));
//builder.Services.AddDbContext<DemoContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoContext") ?? throw new InvalidOperationException("Connection string 'DemoContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsapp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapUserEndpoints();

app.Run();
