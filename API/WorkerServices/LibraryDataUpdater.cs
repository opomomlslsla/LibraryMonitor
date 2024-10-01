using API.Hubs;
using Application.DTO;
using Application.Services;
using Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace API.WorkerServices;

public sealed class LibraryDataUpdater(IHubContext<BooksInfoHub> hub, IServiceProvider serviceProvider, IConfiguration configuration) : BackgroundService
{
    private readonly IHubContext<BooksInfoHub> hubContext = hub;
    private SqlTableDependency<Book> _sqlDependency = new(configuration.GetConnectionString("DefaultConnectionString"), "Books");


    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _sqlDependency.OnChanged += Changed;
        _sqlDependency.Start();
        
    }

    private async void Changed(object sender, RecordChangedEventArgs<Book> e)
    {
        var service = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<BookService>();
        var bookDto = e.Entity.Adapt<BookDTO>();
        var data = new LibraryData()
        {
            AvailableBooksCount = await service.CountAvailableAsync(),
            TotalBooksCount = await service.CountTotalAsync(),
            Books = new List<BookDTO>() {bookDto}
        };
        await hubContext.Clients.All.SendAsync("onRecieveStatistics", data);
    }

    public override void Dispose()
    {
        _sqlDependency.Dispose();
        base.Dispose();
    }

}
