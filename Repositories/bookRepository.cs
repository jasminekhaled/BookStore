
using bookstore.models.Repositories;
using BookStoreTestCase.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreTestCase.models.Repositories
{
    public class bookRepository : IbookstoreRepository<Book>
    {
        List<Book> books;
        public bookRepository()
        {
            books = new List<Book>()
            {
                new Book() { Id = 1, Title = "c#", Description = "C##" , Author = new author{ Id = 1 }, ImageUrl="5555.png"  },
                new Book() { Id = 2, Title = "c++", Description = "C+++", Author = new author{ Id = 3 }, ImageUrl="6666.png"},
                new Book() { Id = 3, Title = "css", Description = "Csss", Author = new author{ Id = 2 }, ImageUrl="8888.png" },
            };

        }
        public void Add(Book entity)
        {
            entity.Id = books.Max(b => b.Id) + 1;
            books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = Find(id);
            books.Remove(book);
        }

        public Book Find(int id)
        {
            var book = books.SingleOrDefault(b => b.Id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public void Update(int id, Book newbook)
        {
            var book = Find(id);
            book.Title = newbook.Title;
            book.Description = newbook.Description;
            book.Author = newbook.Author;
            book.ImageUrl = newbook.ImageUrl;
        }
    }

}

