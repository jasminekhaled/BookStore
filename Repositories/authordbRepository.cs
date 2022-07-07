using bookstore.models.Repositories;
using BookStoreTestCase.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStoreTestCase.Repositories
{
    public class authordbRepository : IbookstoreRepository<author>
    {
        public DataBaseClass DB { get; }
        public authordbRepository(DataBaseClass DB)
        {
             this.DB = DB;
        }
        public IList<author> List()
        {
            var authors = DB.authors.ToList();
            return authors;
        }

        public void Add(author entity)
        {

            DB.authors.Add(entity);
            DB.SaveChanges();

        }

        public void Delete(int id)
        {
            var author = Find(id);
            DB.authors.Remove(author);
            DB.SaveChanges();
        }

        public author Find(int id)
        {
            var author = DB.authors.SingleOrDefault(b => b.Id == id);
            return author;
        }

        public void Update(int id, author newauthor)
        {
            DB.Update(newauthor);
            DB.SaveChanges();
        }
    }
    
}
