using API.Filters;
using API.Hubs;
using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController(BookService service, IHubContext<BooksInfoHub> context) : ControllerBase
{
    private readonly IHubContext<BooksInfoHub> _hubContext = context;
    private readonly BookService _service = service;

    [ApiExceptionFilter]
    [HttpGet("Get/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.GetAsync(id));
    }

    [ApiExceptionFilter]
    [HttpGet("GetByName/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        return Ok(await _service.GetByNameAsync(name));
    }


    [ApiExceptionFilter]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [ApiExceptionFilter]
    [HttpPost("AddBook")]
    public async Task<IActionResult> Add(BookDTO bookDTO)
    {
        await _service.AddAsync(bookDTO);
        return Ok();
    }


    [ApiExceptionFilter]
    [HttpPut("Update")]
    public async Task<IActionResult> Update(BookDTO bookDTO)
    {
        await _service.UpdateAsync(bookDTO);
        return Ok();
    }

    [ApiExceptionFilter]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }


}
