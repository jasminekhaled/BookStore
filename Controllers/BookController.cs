using bookstore.models.Repositories;
using BookStoreTestCase.Models;
using BookStoreTestCase.viewmodel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookStore.Controllers
{
    public class bookController : Controller
    {
        private IbookstoreRepository<Book> bookRepository;
        private readonly IbookstoreRepository<author> authorRepository;
        private readonly IHostingEnvironment hosting;

        // GET: bookController
        public bookController(IbookstoreRepository<Book> bookRepository, IbookstoreRepository<author> authorRepository, IHostingEnvironment hosting)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: bookController/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // GET: bookController/Create
        public ActionResult Create()
        {
            var model = new bookauthorviewmodel
            {
                authors = Fillselectlist()
            };

            return View(model);

        }

        // POST: bookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(bookauthorviewmodel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string filename = uploadFile(model.File) ?? string.Empty;
                    

                    if (model.authorId == -1)
                    {
                        ViewBag.Message = "please select an author name!";
                        return View(getallauthors());
                    }

                    var author = authorRepository.Find(model.authorId);
                    Book book = new Book
                    {
                        Id = model.bookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author,
                        ImageUrl = filename
                    };

                    bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("", "YYOU HAVE TO FILL ALL THE REQUIRED FIELDS!");
            return View(getallauthors());
        }

        // GET: bookController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = bookRepository.Find(id);

            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;

            var viewmodel = new bookauthorviewmodel
            {
                bookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                authorId = book.Author.Id,
                authors = authorRepository.List().ToList(),
                ImageUrl = book.ImageUrl
            };
            return View(viewmodel);
        }

        // POST: bookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(bookauthorviewmodel viewmodel)
        {
            try
            {
                string filename = uploadFile(viewmodel.File, viewmodel.ImageUrl);
               
                
                var author = authorRepository.Find(viewmodel.authorId);
                Book book = new Book
                {
                    Id = viewmodel.bookId,
                    Title = viewmodel.Title,
                    Description = viewmodel.Description,
                    Author = author,
                    ImageUrl = filename
                };
                bookRepository.Update(viewmodel.bookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: bookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.Find(id);
            return View(book);
        }

        // POST: bookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult confirmDelete(int id)
        {
            try
            {
                bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        List<author> Fillselectlist()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new author { Id = -1, FullName = "please select an author" });
            return authors;
        }
        bookauthorviewmodel getallauthors()
        {
            var vmodel = new bookauthorviewmodel
            {
                authors = Fillselectlist()
            };
            return vmodel;
        }
        string uploadFile (IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string FullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(FullPath, FileMode.Create));

                return file.FileName;
            }
            return null;
        }


        string uploadFile(IFormFile file, string ImageUrl)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "uploads");
                string newPath = Path.Combine(uploads, file.FileName);

                //delete the old file
                
                string oldPath = Path.Combine(uploads, ImageUrl);
                if (oldPath != newPath)
                {
                    System.IO.File.Delete(oldPath);
                    //save the new file
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }
                return file.FileName;
            }
            return ImageUrl;
        }
    }
}
