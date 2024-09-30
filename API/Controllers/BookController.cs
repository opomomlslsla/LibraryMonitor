using API.Hubs;
using API.WorkerServices;
using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(BookService service, IHubContext<BooksInfoHub> context) : ControllerBase
    {
        private readonly IHubContext<BooksInfoHub> _hubContext = context;
        private readonly BookService _service = service;

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _service.GetAsync(id));
        }

        [HttpGet("GetByName/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            return Ok(await _service.GetByNameAsync(name));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> Add(BookDTO bookDTO)
        {
            await _service.AddAsync(bookDTO);
            await _hubContext.Clients.All.SendAsync("onBookAdd", bookDTO);
            return Ok();
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(BookDTO bookDTO)
        {
            await _service.UpdateAsync(bookDTO);
            await _hubContext.Clients.All.SendAsync("onBookUpdate", bookDTO);
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            await _hubContext.Clients.All.SendAsync("onBookDelete");
            return Ok();
        }
    }
}
