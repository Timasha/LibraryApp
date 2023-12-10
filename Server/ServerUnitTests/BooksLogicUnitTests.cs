using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.ConsoleLogger;
using Server.Global.Storage;
using Server.Logic.Books;
using Server.Logic.Books.Models;
using Server.Logic.Books.Responses;
using Server.Logic.Books.Responses.Base;

namespace ServerUnitTests;

[TestClass]
public class BooksLogicUnitTests
{
    private BooksLogicProvider _logic; 
    public void Setup()
    {
        GlobalStorage storage = new GlobalStorage();
        storage.CreateBook(new Book("War and piece","somebookdescription1",10));
        storage.CreateBook(new Book("War and piece2","somebookdescription2",2));
        _logic = new BooksLogicProvider(storage,new ConsoleLogger());
    }
    public void SetupEmpty()
    {
        GlobalStorage storage = new GlobalStorage();
        _logic = new BooksLogicProvider(storage,new ConsoleLogger());
    }
    [TestMethod]
    public void Test_GetAllBooks()
    {
        Setup();

        BooksLogicGetAllBooksResponse expected = new BooksLogicGetAllBooksResponse(BooksLogicResponsesStatusCode.Ok,
        new[]
        {
            new Book(1,"War and piece", "somebookdescription1", 10),
            new Book(2,"War and piece2", "somebookdescription2", 2)
        });

        var resp = _logic.GetAllBooks();
        Assert.AreEqual(expected.StatusCode,resp.StatusCode);
        Assert.AreEqual(expected.Books.Length,resp.Books.Length);
        for (int i = 0; i < expected.Books.Length; i++)
        {
            Assert.AreEqual(expected.Books[i].Id,resp.Books[i].Id);
            Assert.AreEqual(expected.Books[i].Name,resp.Books[i].Name);
            Assert.AreEqual(expected.Books[i].Description,resp.Books[i].Description);
            Assert.AreEqual(expected.Books[i].BookedByLogins.Length,resp.Books[i].BookedByLogins.Length);
            for (int k = 0; k < expected.Books[i].BookedByLogins.Length; k++)
            {
                Assert.AreEqual(expected.Books[i].BookedByLogins[k],resp.Books[i].BookedByLogins[k]);
            }
            Assert.AreEqual(expected.Books[i].AllNum,resp.Books[i].AllNum);
            Assert.AreEqual(expected.Books[i].BookedNum,resp.Books[i].BookedNum);
            
        }
    }

    [TestMethod]
    public void Test_GetAllUsers_EmptyStorage()
    {
        SetupEmpty();
        
        BooksLogicGetAllBooksResponse expected = new BooksLogicGetAllBooksResponse(BooksLogicResponsesStatusCode.EmptyStorage,new Book[]{});

        var resp = _logic.GetAllBooks();
        Assert.AreEqual(expected.StatusCode,resp.StatusCode);
        Assert.AreEqual(expected.Books.Length,resp.Books.Length);
    }

    [TestMethod]
    public void Test_ReserveBook()
    {
        Setup();

        Tuple<ulong, string, BooksLogicReserveBookResponse>[] testTable = new[]
        {
            new Tuple<ulong, string, BooksLogicReserveBookResponse>(2, "someLogin",
                new BooksLogicReserveBookResponse(BooksLogicResponsesStatusCode.Ok)),
            new Tuple<ulong, string, BooksLogicReserveBookResponse>(2, "someLogin",
                new BooksLogicReserveBookResponse(BooksLogicResponsesStatusCode.Ok)),
            new Tuple<ulong, string, BooksLogicReserveBookResponse>(2, "someLogin",
                new BooksLogicReserveBookResponse(BooksLogicResponsesStatusCode.AllBooksReserved)),
            new Tuple<ulong, string, BooksLogicReserveBookResponse>(3, "someLogin",
                new BooksLogicReserveBookResponse(BooksLogicResponsesStatusCode.BookNotExists))
        };
        foreach (var ell in testTable)
        {
            var resp = _logic.ReserveBook(ell.Item1, ell.Item2);
            Assert.AreEqual(ell.Item3.StatusCode,resp.StatusCode);
        }
    }
}