using Server.Logic.Books.Contracts.BookStorage.Responses.Base.Base;

namespace Server.Logic.Books.Contracts.BookStorage.Responses;

public class BookStorageDeleteBookResponse : BaseBookStorageResponse 
{
    public BookStorageDeleteBookResponse(BookStorageResponsesStatusCode statusCode) : base(statusCode)
    {
    }
}