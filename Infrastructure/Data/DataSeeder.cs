using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DataSeeder(LibraryContext context)
    {
        private readonly LibraryContext _libraryContext = context;

        public void Seed() 
        {
            SeedBooks();
        }

        public void SeedBooks() 
        {
            var date = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            if (!_libraryContext.Books.Any())
            {
                _libraryContext.Books.AddRange(new List<Book>()
                {
                new Book()
                {
                    Name = "Преступление и наказание",
                    PublicationDate = date,
                    IsAvailable = true,
                    UpdateTime = DateTime.Now
                },
                new Book()
                {
                    Name = "Война и мир",
                    PublicationDate = date,
                    IsAvailable = true,
                    UpdateTime = DateTime.Now
                },new Book()
                {
                    Name = "Цветы для Элджернона",
                    PublicationDate = date,
                    IsAvailable = true,
                    UpdateTime = DateTime.Now
                },new Book()
                {
                    Name = "Бойцовский клуб",
                    PublicationDate = date,
                    IsAvailable = true,
                    UpdateTime = DateTime.Now
                },new Book()
                {
                    Name = "Капитанская дочка",
                    PublicationDate = date,
                    IsAvailable = true,
                    UpdateTime = DateTime.Now
                },new Book()
                {
                    Name = "Маленький принц",
                    PublicationDate = date,
                    IsAvailable = true,
                    UpdateTime = DateTime.Now
                },
                new Book()
                {
                    Name = "Отцы и дети",
                    PublicationDate = date,
                    IsAvailable = true,
                    UpdateTime = DateTime.Now
                },new Book()
                {
                    Name = "Мертвые души 1 том",
                    PublicationDate = date,
                    IsAvailable = true,
                    UpdateTime = DateTime.Now
                },new Book()
                {
                    Name = "Как закалялась сталь",
                    PublicationDate = date,
                    IsAvailable = true,
                    UpdateTime = DateTime.Now
                },
                new Book()
                {
                    Name = "Когда плакал ницше",
                    PublicationDate = date,
                    IsAvailable = true,
                    UpdateTime = DateTime.Now
                },new Book()
                {
                    Name = "Трудно быть богом",
                    PublicationDate = date,
                    IsAvailable = true,
                    UpdateTime = DateTime.Now
                },new Book()
                {
                    Name = "Мы",
                    PublicationDate = date,
                    IsAvailable = true,
                    UpdateTime = DateTime.Now
                },
                });
            }
        }

    }
}
