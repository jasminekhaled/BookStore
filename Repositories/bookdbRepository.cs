using bookstore.models.Repositories;
using BookStoreTestCase.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreTestCase.Repositories
{
    public class bookdbRepository : IbookstoreRepository<Book>
    {
        public DataBaseClass DB { get; }
        public bookdbRepository(DataBaseClass DB)
        {
            this.DB = DB;
        }
        public IList<Book> List()
        {
            return DB.Books.Include(a => a.Author).ToList();
        }
      
        public void Add(Book entity)
        {
           
            DB.Books.Add(entity);
            DB.SaveChanges();

        }

        public void Delete(int id)
        {
            var Book = Find(id);
            DB.Books.Remove(Book);
            DB.SaveChanges();
        }

        public Book Find(int id)
        {
            var Book = DB.Books.Include(a => a.Author).SingleOrDefault(b => b.Id == id);
            return Book;
        }

        public void Update(int id, Book newBook)
        {
            DB.Update(newBook);
            DB.SaveChanges();
        }

        
    }

}
  
