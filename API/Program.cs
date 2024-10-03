using API.Hubs;
using API.WorkerServices;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Infrastructure.Data;
using Infrastructure.Repositories;
namespace API;

public class Program
{
    public static void Main(string[] args)
    {
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
        builder.Services.AddDbContextFactory<LibraryContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
        });

        builder.Services.AddSignalR();
        builder.Services.AddScoped<IRepository<Book>, BookRepository>();
        builder.Services.AddScoped<BookService>();
        builder.Services.AddHostedService<LibraryDataUpdater>();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Some API v1", Version = "v1" });
            options.AddSignalRSwaggerGen();
        });

        var app = builder.Build();

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
    }
}
