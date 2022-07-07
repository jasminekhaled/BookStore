
    using bookstore.models.Repositories;
    using BookStoreTestCase.Models;
    using System.Collections.Generic;
    using System.Linq;
namespace BookStoreTestCase.Repositories
{
    

    
        public class authorRepository : IbookstoreRepository<author>
        {
            IList<author> authors;
            public authorRepository()
            {
            authors = new List<author>()
            {
                new author() { Id = 1, FullName = "ali" },
                new author() { Id = 2, FullName = "ahmed" },
                new author() { Id = 3, FullName = "yasser" },
            };
        }
            public void Add(author entity)
            {
                entity.Id = authors.Max(b => b.Id) + 1;
                authors.Add(entity);
            }

            public void Delete(int id)
            {
                var author = Find(id);
                authors.Remove(author);
            }

            public author Find(int id)
            {
                var author = authors.SingleOrDefault(b => b.Id == id);
                return author;
            }

            public IList<author> List()
            {
                return authors;
            }

            public void Update(int id, author newauthor)
            {
                var author = Find(id);
                author.FullName = newauthor.FullName;
            }
        }
    


}
