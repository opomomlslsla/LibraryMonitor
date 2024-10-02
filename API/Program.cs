
using API.Hubs;
using API.WorkerServices;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Iinfrastructure.Data;
using Iinfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API
{
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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContextFactory<LibraryContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            });

            builder.Services.AddSignalR();
            builder.Services.AddScoped<IRepository<Book>, BookRepository>();
            builder.Services.AddScoped<BookService>();
            builder.Services.AddSingleton<LibraryDataUpdater>();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Some API v1", Version = "v1" });
                // some other configs
                options.AddSignalRSwaggerGen();
            });

            //builder.Services.AddSwaggerGen();

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

            app.MapHub<BooksInfoHub>("/BooksInfoHub");

            app.UseCors("any");

            app.Run();
        }
    }
}
