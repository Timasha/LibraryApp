using Server.Logic.Books.Contracts.BookStorage.Responses.Base.Base;

namespace Server.Logic.Books.Contracts.BookStorage.Responses;

public class BookStorageAddBookResponse : BaseBookStorageResponse
{

    public BookStorageAddBookResponse(BookStorageResponsesStatusCode statusCode) : base(statusCode)
    {
    }
}