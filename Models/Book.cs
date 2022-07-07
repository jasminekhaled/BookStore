using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreTestCase.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Author_Id { get; set; }
        [ForeignKey(nameof(Author_Id))]
        public author Author { get; set; }
        public string ImageUrl { get; set; }

    }
}
