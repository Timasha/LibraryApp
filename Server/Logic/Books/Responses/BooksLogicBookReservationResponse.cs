using Server.Logic.Books.Responses.Base;

namespace Server.Logic.Books.Responses;

public class BooksLogicBookReservationResponse : BaseBooksLogicResponse
{
    public BooksLogicBookReservationResponse(BooksLogicResponsesStatusCode statusCode) : base(statusCode)
    {
    }
}