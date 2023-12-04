using Server.Logic.Books.Contracts.BookStorage.Responses;
using Server.Logic.Books.Models;

namespace Server.Logic.Books.Contracts.BookStorage;

public interface IBookStorage
{
    public BookStorageAddBookResponse CreateBook(Book book);
    public BookStorageGetAllBooksResponse GetAllBooks();
    public BookStorageDeleteBookResponse DeleteBook(UInt64 id);
    public BookStorageUpdateBookResponse UpdateBook(UInt64 id, Book newBook);
    public BookStorageGetBookResponse GetBook(UInt64 id);
}