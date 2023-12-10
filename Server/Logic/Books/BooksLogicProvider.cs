using Server.Logic.Books.Contracts.BookStorage;
using Server.Logic.Books.Contracts.BookStorage.Responses;
using Server.Logic.Books.Contracts.BookStorage.Responses.Base;
using Server.Logic.Books.Contracts.BookStorage.Responses.Base.Base;
using Server.Logic.Books.Models;
using Server.Logic.Books.Responses;
using Server.Logic.Books.Responses.Base;
using ILogger = Server.Global.Logger.ILogger;
using LogLevel = Server.Global.Logger.LogLevel;

namespace Server.Logic.Books;

public class BooksLogicProvider
{
    private IBookStorage _bookStorage;
    private ILogger _logger;

    public BooksLogicProvider(IBookStorage bookStorage, ILogger logger)
    {
        _bookStorage = bookStorage;
        _logger = logger;
    }

    public BooksLogicAddBookResponse AddBook(Book book)
    {
        BookStorageAddBookResponse response = _bookStorage.CreateBook(book);
        
        
        _logger.Log(LogLevel.Info,$"Add book: create book status: {BookStorageStatusFabric.Status(response.StatusCode)}");
        switch (response.StatusCode)
        {
            case BookStorageResponsesStatusCode.Ok:
            {
                break;
            }
            case BookStorageResponsesStatusCode.BookAlreadyExists:
            {
                return new BooksLogicAddBookResponse(BooksLogicResponsesStatusCode.BookAlreadyExists);
            }
            
        }

        return new BooksLogicAddBookResponse(BooksLogicResponsesStatusCode.Ok);
    }

    public BooksLogicAddExistedBookResponse AddExistedBook(UInt64 bookId ,int num)
    {
        BookStorageGetBookResponse getBookResponse = _bookStorage.GetBook(bookId);
        
        _logger.Log(LogLevel.Info,$"Add existed book: get book status {BookStorageStatusFabric.Status(getBookResponse.StatusCode)}");

        switch (getBookResponse.StatusCode)
        {
            case BookStorageResponsesStatusCode.BookNotExists:
            {
                return new BooksLogicAddExistedBookResponse(BooksLogicResponsesStatusCode.BookNotExists);
            }
            case BookStorageResponsesStatusCode.Ok:
            {
                break;
            }
        }

        Book newBook = getBookResponse.Book;
        newBook.AllNum =  newBook.AllNum + (ulong)(num);
        
        BookStorageUpdateBookResponse updateBookResponse = _bookStorage.UpdateBook(bookId, newBook);
        
        _logger.Log(LogLevel.Info,$"Add existed book: update book status {BookStorageStatusFabric.Status(updateBookResponse.StatusCode)}");
        
        switch (updateBookResponse.StatusCode)
        {
            case BookStorageResponsesStatusCode.BookNotExists:
            {
                return new BooksLogicAddExistedBookResponse(BooksLogicResponsesStatusCode.BookNotExists);
            }
            case BookStorageResponsesStatusCode.Ok:
            {
                break;
            }
        }

        return new BooksLogicAddExistedBookResponse(BooksLogicResponsesStatusCode.Ok);
    }

    public BooksLogicGetAllBooksResponse GetAllBooks()
    {
        BookStorageGetAllBooksResponse response = _bookStorage.GetAllBooks();
        
        
        _logger.Log(LogLevel.Info,$"Get all books: get all books status: {BookStorageStatusFabric.Status(response.StatusCode)}");

        switch (response.StatusCode)
        {
            case BookStorageResponsesStatusCode.Ok:
            {
                break;
            }
            case BookStorageResponsesStatusCode.EmptyStorage:
            {
                return new BooksLogicGetAllBooksResponse(BooksLogicResponsesStatusCode.EmptyStorage, new Book[]{ });
            }
            
        }
        
        return new BooksLogicGetAllBooksResponse(BooksLogicResponsesStatusCode.Ok,response.Books);
    }

    public BooksLogicReserveBookResponse ReserveBook(UInt64 bookId, string login)
    {
        BookStorageGetBookResponse getBookResponse = _bookStorage.GetBook(bookId);
        
        _logger.Log(LogLevel.Info,$"Reserve book: get book status {BookStorageStatusFabric.Status(getBookResponse.StatusCode)}");

        switch (getBookResponse.StatusCode)
        {
            case BookStorageResponsesStatusCode.BookNotExists:
            {
                return new BooksLogicReserveBookResponse(BooksLogicResponsesStatusCode.BookNotExists);
            }
            case BookStorageResponsesStatusCode.Ok:
            {
                break;
            }
        }

        if (getBookResponse.Book.AllNum < (getBookResponse.Book.BookedNum + 1))
        {
            return new BooksLogicReserveBookResponse(BooksLogicResponsesStatusCode.AllBooksReserved);
        }
        Book newBook = getBookResponse.Book;
        newBook.BookedNum += 1;
        newBook.BookedByLogins = getBookResponse.Book.BookedByLogins.Append(login).ToArray();

        BookStorageUpdateBookResponse updateBookResponse = _bookStorage.UpdateBook(bookId, newBook);
        
        _logger.Log(LogLevel.Info,$"Reserve book: update book status {BookStorageStatusFabric.Status(updateBookResponse.StatusCode)}");
        
        switch (updateBookResponse.StatusCode)
        {
            case BookStorageResponsesStatusCode.BookNotExists:
            {
                return new BooksLogicReserveBookResponse(BooksLogicResponsesStatusCode.BookNotExists);
            }
            case BookStorageResponsesStatusCode.Ok:
            {
                break;
            }
        }
        
        return new BooksLogicReserveBookResponse(BooksLogicResponsesStatusCode.Ok);
    }
    
}