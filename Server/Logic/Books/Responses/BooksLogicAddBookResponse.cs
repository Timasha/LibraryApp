using Server.Logic.Books.Responses.Base;

namespace Server.Logic.Books.Responses;

public class BooksLogicAddBookResponse : BaseBooksLogicResponse
{
    public BooksLogicAddBookResponse(BooksLogicResponsesStatusCode statusCode) : base(statusCode)
    {
    }
}