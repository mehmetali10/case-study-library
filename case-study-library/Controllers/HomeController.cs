using case_study_library.Data;
using case_study_library.Dtos;
using case_study_library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace case_study_library.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LibraryDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, LibraryDbContext dbContext )
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var books = _dbContext.Books
                    .Where(b => b.IsDeleted == false)
                    .ToList();

                foreach (var book in books)
                {
                    var barrowHistory = _dbContext.BarrowHistories
                        .Where(h => h.IsDeleted == false)
                        .OrderByDescending(h => h.BarrowEndDate)
                        .FirstOrDefault(h => h.BookId == book.Id);

                    if (barrowHistory != null)
                    {
                        if (barrowHistory.BarrowEndDate <= DateTime.Now)
                        {
                            book.IsAvaliable = true;
                        }
                    }
                }

                _dbContext.SaveChanges(); 

                _logger.LogInformation("Updated the availability of books.");

                return View(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating book availability: {ex.Message}");
                return View("Error");
            }
        }


        public IActionResult CreateBook(CreateBookRequest _book)
        {
            try
            {
                var newBook = new Book
                {
                    BookName = _book.BookName,
                    Author = _book.Author,
                    AboutBook = _book.AboutBook,
                    ImageLink = _book.ImageLink,
                    PublicationYear = _book.PublicationYear,
                    Publisher = _book.Publisher,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    IsAvaliable = true
                };

                _dbContext.Books.Add(newBook);

                _dbContext.SaveChanges();

                _logger.LogInformation($"New book '{newBook.BookName}' created.");

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating book: {ex.Message}");
                return View("Error");
            }
        }

        public IActionResult GetBook(int id)
        {
            try
            {
                var book = _dbContext.Books.FirstOrDefault(b => b.Id == id);

                if (book == null)
                {
                    _logger.LogError("Book is null");
                    return View("Error");
                }

                _logger.LogInformation($"Get book called with id : '{id}'");

                return View(book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Hata oluştu: {ex.Message}");
                return View("Error");
            }
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}