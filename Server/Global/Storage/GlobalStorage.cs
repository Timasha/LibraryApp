using Server.Logic.Auth.Contracts.UserStorage;
using Server.Logic.Auth.Contracts.UserStorage.Responses;
using Server.Logic.Auth.Contracts.UserStorage.Responses.Base;
using Server.Logic.Auth.Models;
using Server.Logic.Books.Contracts.BookStorage;
using Server.Logic.Books.Contracts.BookStorage.Responses;
using Server.Logic.Books.Contracts.BookStorage.Responses.Base.Base;
using Server.Logic.Books.Models;

namespace Server.Global.Storage;

public class GlobalStorage : IUserStorage, IBookStorage
{
    private Dictionary<string,User> _users = new Dictionary<string, User>();
    private Dictionary<UInt64, Book> _books = new Dictionary<UInt64, Book>();
    private UInt64 _lastBooksId = 0;

    public UserStorageCreateUserResponse CreateUser(User user)
    {
        
        if (_users.ContainsKey(user.Login))
        {
            return new UserStorageCreateUserResponse(UserStorageStatusCode.UserAlreadyExists);
        }
        _users.Add(user.Login,user);
        return new UserStorageCreateUserResponse(UserStorageStatusCode.Ok);
    }

    public UserStorageGetUserByLoginResponse GetUserByLogin(string login)
    {
        if (!_users.ContainsKey(login))
        {
            return new UserStorageGetUserByLoginResponse(UserStorageStatusCode.UserNotExists,new User("",""));
        }

        return new UserStorageGetUserByLoginResponse(UserStorageStatusCode.Ok,_users[login]);
    }

    public BookStorageAddBookResponse CreateBook(Book book)
    {
        foreach (var bookKeyValue in _books)
        {
            if (bookKeyValue.Value.Name == book.Name)
            {
                return new BookStorageAddBookResponse(BookStorageResponsesStatusCode.BookAlreadyExists);
            }
        }
        if (_books.ContainsKey(book.Id))
        {
            return new BookStorageAddBookResponse(BookStorageResponsesStatusCode.BookAlreadyExists);
        }

        _lastBooksId += 1;
        book.Id = _lastBooksId;
        _books.Add(_lastBooksId,book);
        return new BookStorageAddBookResponse(BookStorageResponsesStatusCode.Ok);
    }

    public BookStorageGetAllBooksResponse GetAllBooks()
    {
        var arr = _books.Values.ToArray();
        if (arr.Length == 0)
        {
            return new BookStorageGetAllBooksResponse(BookStorageResponsesStatusCode.EmptyStorage, new Book[] { });
        }

        return new BookStorageGetAllBooksResponse(BookStorageResponsesStatusCode.Ok, arr);
    }

    public BookStorageDeleteBookResponse DeleteBook(ulong id)
    {
        if (!_books.ContainsKey(id))
        {
            return new BookStorageDeleteBookResponse(BookStorageResponsesStatusCode.BookNotExists);
        }

        _books.Remove(id);
        return new BookStorageDeleteBookResponse(BookStorageResponsesStatusCode.Ok);
    }

    public BookStorageUpdateBookResponse UpdateBook(ulong id, Book newBook)
    {
        if (!_books.ContainsKey(id))
        {
            return new BookStorageUpdateBookResponse(BookStorageResponsesStatusCode.BookNotExists);
        }

        _books[id] = newBook;
        return new BookStorageUpdateBookResponse(BookStorageResponsesStatusCode.Ok);
    }

    public BookStorageGetBookResponse GetBook(ulong id)
    {
        if (!_books.ContainsKey(id))
        {
            return new BookStorageGetBookResponse(BookStorageResponsesStatusCode.BookNotExists, new Book("","",0));
        }

        return new BookStorageGetBookResponse(BookStorageResponsesStatusCode.Ok, _books[id]);
    }
}