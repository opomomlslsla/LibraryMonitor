using Application.DTO;
using Application.Services;
using Mapster;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using System.Runtime.CompilerServices;

namespace API.Hubs
{
    [SignalRHub]
    public class BooksInfoHub(BookService service) : Hub
    {
        public async Task GetData()
        {
            var books = await service.GetAllAsync();
            var data = new LibraryData()
            {
                AvailableBooksCount = books.Count(x => x.IsAvailable),
                TotalBooksCount = books.Count(),
                Books = books.Adapt<List<BookDTO>>()
            };
            await Clients.All.SendAsync("onRecieveStatistics", data);
        }


    }
}
