using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Logic.Books;
using Server.Logic.Books.Models;
using Server.Logic.Books.Responses.Base;

namespace Server.Controllers;

public class BookManagingController : Controller
{
    private BooksLogicProvider _booksLogic;

    public BookManagingController(BooksLogicProvider booksLogic)
    {
        _booksLogic = booksLogic;
    }

    [Route("/createBook")]
    [Authorize(Roles = "admin")]
    [HttpPost]
    public IActionResult CreateBook([FromForm] string name, [FromForm] string description, [FromForm] UInt64 allNum)
    {
        var response = _booksLogic.AddBook(new Book(name, description, allNum));
        return Json(response);
    }

    [Route("/getAllBooks")]
    [Authorize(Roles="admin, user")]
    [HttpPost]
    public IActionResult GetAllBooks()
    {
        var response = _booksLogic.GetAllBooks();
        return Json(response);
    }

    [Route("/addExistedBook")]
    [Authorize(Roles="admin")]
    [HttpPost]
    public IActionResult AddExistedBook([FromQuery] UInt64 bookId, [FromForm] int num)
    {
        var response = _booksLogic.AddExistedBook(bookId, num);
        return Json(response);
    }
    
    [Route("/reserveBook")]
    [Authorize(Roles="admin, user")]
    [HttpPost]
    public IActionResult ReserveBook([FromQuery] UInt64 bookId)
    {
        string login = "";
        foreach (var claim in HttpContext.User.Claims)
        {
            if (claim.Type == ClaimsIdentity.DefaultNameClaimType)
            {
                break;
            }
        }

        var response = _booksLogic.ReserveBook(bookId, login);
        return Json(response);
    }
}