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

public sealed class LibraryDataUpdater : BackgroundService
{
    private readonly IHubContext<BooksInfoHub> _hubContext ;
    private SqlTableDependency<Book> _sqlDependency;
    private readonly IServiceProvider _serviceProvider;

    public LibraryDataUpdater(IHubContext<BooksInfoHub> hub, IServiceProvider serviceProvider, IConfiguration configuration)
    {
        _hubContext = hub;
        _sqlDependency = new(configuration.GetConnectionString("DefaultConnectionString"), "Books");
        _sqlDependency.OnChanged += Changed;
        _sqlDependency.Start();
        _serviceProvider = serviceProvider;
    }

    private async void Changed(object sender, RecordChangedEventArgs<Book> e)
    {
        var service = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<BookService>();
        var bookDto = e.Entity.Adapt<BookDTO>();
        var data = new LibraryData()
        {
            AvailableBooksCount = await service.CountAvailableAsync(),
            TotalBooksCount = await service.CountTotalAsync(),
            Books = new List<BookDTO>() {bookDto}
        };
        await _hubContext.Clients.All.SendAsync("onRecieveStatistics", data);
    }

    public override void Dispose()
    {
        _sqlDependency.Dispose();
        base.Dispose();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}
