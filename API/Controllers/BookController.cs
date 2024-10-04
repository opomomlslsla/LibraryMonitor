using API.Filters;
using API.Hubs;
using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers;

[Route("api/Books")]
[ApiController]
public class BookController(BookService service, IHubContext<BooksInfoHub> context) : ControllerBase
{
    private readonly IHubContext<BooksInfoHub> _hubContext = context;
    private readonly BookService _service = service;

    [ApiExceptionFilter]
    [HttpGet("/{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.GetAsync(id));
    }

    [ApiExceptionFilter]
    [HttpGet("/bookname={name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        return Ok(await _service.GetByNameAsync(name));
    }


    [ApiExceptionFilter]
    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [ApiExceptionFilter]
    [HttpPost]
    public async Task<IActionResult> Add(BookDTO bookDTO)
    {
        await _service.AddAsync(bookDTO);
        return Ok();
    }


    [ApiExceptionFilter]
    [HttpPut("/{id}")]
    public async Task<IActionResult> Update(BookDTO bookDTO, Guid id)
    {
        await _service.UpdateAsync(bookDTO);
        return Ok();
    }

    [ApiExceptionFilter]
    [HttpDelete("/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }


}
