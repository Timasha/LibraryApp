namespace Server.Logic.Books.Responses.Base;

public class BaseBooksLogicResponse
{
    public BooksLogicResponsesStatusCode StatusCode { get; set; }

    public BaseBooksLogicResponse(BooksLogicResponsesStatusCode statusCode)
    {
        StatusCode = statusCode;
    }
}