using Server.Logic.Books.Responses.Base;

namespace Server.Logic.Books.Responses;

public class BooksLogicAddExistedBookResponse : BaseBooksLogicResponse
{
    public BooksLogicAddExistedBookResponse(BooksLogicResponsesStatusCode statusCode) : base(statusCode)
    {
    }
}