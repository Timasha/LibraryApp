using Server.Logic.Books.Contracts.BookStorage.Responses.Base.Base;

namespace Server.Logic.Books.Contracts.BookStorage.Responses;

public class BookStorageUpdateBookResponse : BaseBookStorageResponse
{
    public BookStorageUpdateBookResponse(BookStorageResponsesStatusCode statusCode) : base(statusCode)
    {
    }
}