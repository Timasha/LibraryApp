using Server.Logic.Books.Contracts.BookStorage.Responses.Base.Base;
using Server.Logic.Books.Models;

namespace Server.Logic.Books.Contracts.BookStorage.Responses;

public class BookStorageGetAllBooksResponse : BaseBookStorageResponse
{
    public Book[] Books { get; set; }

    public BookStorageGetAllBooksResponse(BookStorageResponsesStatusCode statusCode, Book[] books) : base(statusCode)
    {
        Books = books;
    }
}