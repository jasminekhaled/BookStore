using Microsoft.EntityFrameworkCore;

namespace BookStoreTestCase.Models
{
    public class DataBaseClass:DbContext
    {
        public DataBaseClass(DbContextOptions<DataBaseClass> options) : base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<author> authors { get; set; }
    }
}