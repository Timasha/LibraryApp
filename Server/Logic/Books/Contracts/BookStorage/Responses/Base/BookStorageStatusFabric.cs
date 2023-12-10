using Server.Logic.Books.Contracts.BookStorage.Responses.Base.Base;

namespace Server.Logic.Books.Contracts.BookStorage.Responses.Base;

public class BookStorageStatusFabric
{
    public static string Status(BookStorageResponsesStatusCode code)
    {
        switch (code)
        {
            case BookStorageResponsesStatusCode.Ok:
            {
                return "ok";
            }
            case BookStorageResponsesStatusCode.EmptyStorage:
            {
                return "empty storage";
            }
            case BookStorageResponsesStatusCode.BookAlreadyExists:
            {
                return "book already exists";
            }
            case BookStorageResponsesStatusCode.BookNotExists:
            {
                return "book not exists";
            }
        }

        return "";
    }
}