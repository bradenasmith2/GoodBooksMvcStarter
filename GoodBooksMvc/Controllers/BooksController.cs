using GoodBooksMvc.DataAccess;
using Microsoft.AspNetCore.Mvc;
using GoodBooksMvc.Models;

namespace GoodBooksMvc.Controllers
{
    public class BooksController : Controller
    {
        private readonly GoodBooksMvcContext _context;

        public BooksController(GoodBooksMvcContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var favoritedBooks = Request.Cookies["FavoritedBooks"];
            ViewData["favoritedBooks"] = favoritedBooks;

            return View(_context.Books.ToList());
        }

        [Route("/books/compare/{id:int}")]
        public IActionResult Favorite(int id)
        {
            var book = _context.Books.Find(id);
            var recievedCookie = Request.Cookies["FavoritedBooks"].Split(",").ToList();
            recievedCookie.Add(book.Title);

            string cookieString = "";
            foreach(var e in recievedCookie)
            {
                cookieString += e;
                cookieString += ",";
            }

            Response.Cookies.Append("FavoritedBooks", cookieString);

            return Redirect("/books");
        }

        [Route("/books/compare/remove/{id:int}")]
        public IActionResult RemoveFavorite(int id)
        {
            var book = _context.Books.Find(id);
            var recievedCookie = Request.Cookies["FavoritedBooks"].Split(",").ToList();
            recievedCookie.Remove(book.Title);

            string cookieString = "";
            foreach (var e in recievedCookie)
            {
                cookieString += e;
                cookieString += ",";
            }

            Response.Cookies.Append("FavoritedBooks", cookieString);

            return Redirect("/books");
        }
    }
}
