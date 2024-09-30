
using API.Hubs;
using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace API.WorkerServices
{
    public sealed class LibraryDataUpdater (IHubContext<BooksInfoHub> hub, IServiceProvider serviceProvider) : BackgroundService
    {
        private readonly IHubContext<BooksInfoHub> hubContext = hub;
        //private readonly BookService _service = service;
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var service = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<BookService>();
                var data = new LibraryData()
                {
                    AvailableBooksCount = await service.CountAvailableAsync(),
                    TotalBooksCount = await service.CountTotalAsync()
                };
                await hubContext.Clients.All.SendAsync("onRecieveStatistics", "sdsdsds", stoppingToken);
                await Task.Delay(5000);
            }
        }
    }
}
