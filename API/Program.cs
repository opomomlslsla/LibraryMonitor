using API.Hubs;
using API.WorkerServices;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("any", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddSignalR();
builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddScoped<BookService>();
builder.Services.AddHostedService<LibraryDataProvider>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Some API v1", Version = "v1" });
    options.AddSignalRSwaggerGen();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    DataSeeder.SeedBooks(scope.ServiceProvider.GetService<LibraryContext>());
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<BooksInfoHub>("/BooksInfoHub");
app.UseCors("any");
app.Run();

