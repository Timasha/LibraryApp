using Server.Logic.Books.Contracts.BookStorage.Responses.Base.Base;

namespace Server.Logic.Books.Contracts.BookStorage.Responses;

public abstract class BaseBookStorageResponse
{
    public BookStorageResponsesStatusCode StatusCode { get; set; }

    public BaseBookStorageResponse(BookStorageResponsesStatusCode statusCode)
    {
        StatusCode = statusCode;
    }
}