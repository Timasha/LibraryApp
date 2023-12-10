using System.ComponentModel.DataAnnotations;

namespace Server.Logic.Auth.Models;

public class User
{
    public User(string login, string password, Role role)
    {
        Login = login;
        Password = password;
        Role = role;
    }

    public User(string login, string password)
    {
        Login = login;
        Password = password;
    }

    [Key]
    public string Login { get; set; }
    public string Password { get; set; }
    
    public Role Role { get; set; }
}