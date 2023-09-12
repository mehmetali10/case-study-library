using case_study_library.Data;
using case_study_library.Dtos;
using case_study_library.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;

namespace case_study_library.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly LibraryDbContext _dbContext;

        public BookController(ILogger<BookController> logger, LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                // Retrieve available books from the database
                var books = _dbContext.Books
                    .Where(b => b.IsDeleted == false)
                    .ToList();

                // Update book availability based on borrowing history
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

                // Save changes to the database
                _dbContext.SaveChanges();

                // Log the update operation
                _logger.LogInformation("Updated the availability of books.");

                return View(books);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating book availability: {ex.Message}");
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] CreateBookRequest _book)
        {
            try
            {
                // Create a new book record
                var newBook = new Book
                {
                    BookName = _book.BookName,
                    Author = _book.Author,
                    AboutBook = _book.AboutBook,
                    ImageLink = _book.ImageLink,
                    PublicationYear = _book.PublicationYear.HasValue ? _book.PublicationYear.Value.ToUniversalTime() : (DateTime?)null,
                    Publisher = _book.Publisher,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    IsAvaliable = true
                };

                // Add the new book to the database
                _dbContext.Books.Add(newBook);
                _dbContext.SaveChanges();

                // Log the book creation
                _logger.LogInformation($"New book '{newBook.BookName}' created.");

                return Json(new { success = true, message = "Book created successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating book: {ex.Message}");
                return Json(new { success = false, message = "An error occurred while creating the book." });
            }
        }

        [HttpPost]
        public IActionResult CreateBarrow([FromBody] CreateBarrowRequest _barrow)
        {
            try
            {
                // Create a new borrowing history record
                var newBarrow = new BarrowHistory
                {
                    BookId = _barrow.BookId,
                    PersonName = _barrow.PersonName,
                    PersonSurname = _barrow.PersonSurname,
                    BarrowEndDate = (DateTime)(_barrow.BarrowEndDate.HasValue ? _barrow.BarrowEndDate.Value.ToUniversalTime() : (DateTime?)null),
                    BarrowStartDate = DateTime.Now,
                    IsDeleted = false,
                };

                // Add the new borrowing history to the database
                _dbContext.BarrowHistories.Add(newBarrow);

                // Update the availability of the book
                var bookToUpdate = _dbContext.Books.FirstOrDefault(b => b.Id == _barrow.BookId);

                if (bookToUpdate != null)
                {
                    bookToUpdate.IsAvaliable = false;
                    _dbContext.SaveChanges();
                }

                // Log the borrowing history creation
                _logger.LogInformation($"New barrow '{newBarrow.Id}' created.");

                return Json(new { success = true, message = "Barrow created successfully." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating barrow: {ex.Message}");
                return Json(new { success = false, message = "An error occurred while creating the barrow." });
            }
        }

        public IActionResult GetBook(int id)
        {
            try
            {
                // Retrieve book information by ID
                var book = _dbContext.Books.Where(b => b.IsDeleted == false).FirstOrDefault(b => b.Id == id);

                if (book == null)
                {
                    _logger.LogError("Book not found");
                    return View("Error");
                }

                // Log the book query
                _logger.LogInformation($"Book queried by ID: '{id}'");

                // Check if the book is currently borrowed
                if (book.IsAvaliable == false)
                {
                    var barrowHistory = _dbContext.BarrowHistories
                        .Where(b => b.IsDeleted == false)
                        .OrderByDescending(h => h.BarrowEndDate)
                        .FirstOrDefault(h => h.BookId == id);

                    if (barrowHistory != null && barrowHistory.BarrowEndDate >= DateTime.Now)
                    {
                        ViewBag.BarrowHistory = barrowHistory;
                    }
                }

                return View(book);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                return View("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
