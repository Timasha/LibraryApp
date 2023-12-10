using Server.Logic.Books.Responses.Base;

namespace Server.Logic.Books.Responses;

public class BooksLogicReserveBookResponse : BaseBooksLogicResponse
{
    public BooksLogicReserveBookResponse(BooksLogicResponsesStatusCode statusCode) : base(statusCode)
    {
    }
}