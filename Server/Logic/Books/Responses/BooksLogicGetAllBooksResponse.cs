using Server.Logic.Books.Models;
using Server.Logic.Books.Responses.Base;

namespace Server.Logic.Books.Responses;

public class BooksLogicGetAllBooksResponse : BaseBooksLogicResponse
{
    public Book[] Books { get; set; }

    public BooksLogicGetAllBooksResponse(BooksLogicResponsesStatusCode statusCode, Book[] books) : base(statusCode)
    {
        Books = books;
    }
}