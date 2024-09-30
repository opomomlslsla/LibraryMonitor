using Application.DTO;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Mapster;

namespace Application.Services;

public class BookService(IRepository<Book> repository)
{
    IRepository<Book> _repository = repository;

    public async Task<BookDTO> GetAsync(Guid id)
    {
        var book =  await _repository.GetEntityByIdAsync(id) ?? throw new NullEntityException($"can't find entity by given Id - {id}");
        return book.Adapt<BookDTO>();
    }

    public async Task<ICollection<BookDTO>> GetAllAsync()
    {
        var books = await _repository.GetAllEntitiesAsync();
        return books.Adapt<ICollection<BookDTO>>();
    }

    public async Task UpdateAsync(BookDTO bookDto)
    {
        var book = await _repository.GetEntityByIdAsync(bookDto.Id) ?? throw new NullEntityException($"can't find entity by given Id - {bookDto.Id}");
        bookDto.Adapt(book);
        _repository.UpdateEntity(book);

        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var book = await _repository.GetEntityByIdAsync(id) ?? throw new NullEntityException($"can't find entity by given Id - {id}");
        _repository.DeleteEntity(book);
        await _repository.SaveChangesAsync();
    }

    public async Task AddAsync(BookDTO bookDto)
    {
        var book = bookDto.Adapt<Book>();
        await _repository.AddEntityAsync(book);
        await _repository.SaveChangesAsync();
    }

    public async Task<ICollection<BookDTO>> GetByNameAsync(string name)
    {
        var books = await _repository.GetEntitiesByAsync(x => x.Name == name);
        return books.Adapt<ICollection<BookDTO>>();
    }

    public async Task<int> CountAvailableAsync()
    {
        return await _repository.CountAsync(x => x.IsAvailable);
    }

    public async Task<int> CountTotalAsync()
    {
        return await _repository.CountAsync(x => true);
    }

}
