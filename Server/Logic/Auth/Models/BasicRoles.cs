namespace Server.Logic.Auth.Models;

public class BasicRoles
{
    public static readonly Role AdminRole = new Role("admin");
    public static readonly Role SimpleUserRole = new Role("user");
}