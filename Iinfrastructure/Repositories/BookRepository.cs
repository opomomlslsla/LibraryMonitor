using Domain.Entities;
using Iinfrastructure.Data;
using Iinfrastructure.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iinfrastructure.Repositories
{
    public class BookRepository(LibraryContext context) : BaseRepository<Book>(context)
    {
        
    }
}
