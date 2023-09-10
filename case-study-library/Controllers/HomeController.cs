﻿using case_study_library.Data;
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

        public HomeController(ILogger<HomeController> logger, LibraryDbContext dbContext)
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


        [HttpPost]
        public IActionResult CreateBook([FromBody] CreateBookRequest _book)

        {
            try
            {
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

                _dbContext.Books.Add(newBook);
                _dbContext.SaveChanges();

                _logger.LogInformation($"New book '{newBook.BookName}' created.");

                // Kitap başarıyla oluşturulduğunda bir başarı mesajı döndürün.
                return Json(new { success = true, message = "Kitap başarıyla oluşturuldu." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating book: {ex.Message}");
                // Hata durumunda bir hata mesajı döndürün.
                return Json(new { success = false, message = "Kitap oluşturulurken bir hata oluştu." });
            }
        }

        [HttpPost]
        public IActionResult CreateBarrow([FromBody] CreateBarrowRequest _barrow)

        {
            try
            {
                var newBarrow = new BarrowHistory
                {
                    BookId = _barrow.BookId,
                    PersonName = _barrow.PersonName,
                    PersonSurname = _barrow.PersonSurname,
                    BarrowEndDate = (DateTime)(_barrow.BarrowEndDate.HasValue ? _barrow.BarrowEndDate.Value.ToUniversalTime() : (DateTime?)null),
                    BarrowStartDate = DateTime.Now,
                    IsDeleted = false,
                };

                _dbContext.BarrowHistories.Add(newBarrow);
                var bookToUpdate = _dbContext.Books.FirstOrDefault(b => b.Id == _barrow.BookId);

                // Check if the book exists
                if (bookToUpdate != null)
                {
                    // Update the IsAvailable property to false
                    bookToUpdate.IsAvaliable = false;

                    // Save the changes to the database
                    _dbContext.SaveChanges();
                }

                _logger.LogInformation($"New barrow '{newBarrow.Id}' created.");

                // Kitap başarıyla oluşturulduğunda bir başarı mesajı döndürün.
                return Json(new { success = true, message = "Kitap Barrow oluşturuldu." });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating barrow: {ex.Message}");
                // Hata durumunda bir hata mesajı döndürün.
                return Json(new { success = false, message = "Kitap barrow oluşturulurken bir hata oluştu." });
            }
        }


        public IActionResult GetBook(int id)
        {
            try
            {
                var book = _dbContext.Books.Where(b => b.IsDeleted == false).FirstOrDefault(b => b.Id == id);

                if (book == null)
                {
                    _logger.LogError("Kitap bulunamadı");
                    return View("Error");
                }

                _logger.LogInformation($"ID ile kitap sorgulandı: '{id}'");

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