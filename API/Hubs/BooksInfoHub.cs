using Application.DTO;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using System.Runtime.CompilerServices;

namespace API.Hubs
{
    [SignalRHub]
    public class BooksInfoHub : Hub
    {
        public async Task GetTask()
        {
            await Clients.All.SendAsync("onRecieveStatistics");
        }


    }
}
