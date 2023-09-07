using case_study_library.Data;
using case_study_library.Models;
using Microsoft.AspNetCore.Mvc;
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
            var list = _dbContext.Books.OrderBy(b => b.BookName).Where(b => b.IsDeleted == false && b.IsAvaliable == true).ToList();
            return View(list);
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