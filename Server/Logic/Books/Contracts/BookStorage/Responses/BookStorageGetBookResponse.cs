using Server.Logic.Books.Contracts.BookStorage.Responses.Base.Base;
using Server.Logic.Books.Models;

namespace Server.Logic.Books.Contracts.BookStorage.Responses;

public class BookStorageGetBookResponse : BaseBookStorageResponse
{
    public Book Book { get; set; } 
    public BookStorageGetBookResponse(BookStorageResponsesStatusCode statusCode, Book book) : base(statusCode)
    {
        Book = book;
    }
}