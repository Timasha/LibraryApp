namespace Server.Logic.Books.Responses.Base;

public enum BooksLogicResponsesStatusCode
{
    Ok,
    EmptyStorage,
    BookAlreadyExists,
    BookNotExists,
    AllBooksReserved,
}