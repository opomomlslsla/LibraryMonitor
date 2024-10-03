using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories.Common;

namespace Infrastructure.Repositories;

public class BookRepository(LibraryContext context) : BaseRepository<Book>(context)
{

}
