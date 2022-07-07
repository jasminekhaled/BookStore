using System.ComponentModel.DataAnnotations;

namespace BookStoreTestCase.Models
{
    public class author
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
