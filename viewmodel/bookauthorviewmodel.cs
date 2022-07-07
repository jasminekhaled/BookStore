
using BookStoreTestCase.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStoreTestCase.viewmodel
{
    public class bookauthorviewmodel
    {
        public int bookId { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        [StringLength(120, MinimumLength = 5)]
        public string Description { get; set; }
        public int authorId { get; set; }

        public List<author> authors;

        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }
    }
}
