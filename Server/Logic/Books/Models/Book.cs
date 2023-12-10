namespace Server.Logic.Books.Models;

public class Book
{
    public Book(string name, string description, UInt64 allNum)
    {
        Name = name;
        Description = description;
        AllNum = allNum;
    }
    public Book(UInt64 id,string name, string description, UInt64 allNum)
    {
        Id = id;
        Name = name;
        Description = description;
        AllNum = allNum;
    }
    public UInt64 Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public UInt64 BookedNum { get; set; }
    public UInt64 AllNum { get; set; }

    public string[] BookedByLogins { get; set; } = { };
}