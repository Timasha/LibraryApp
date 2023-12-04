using Server.Logic.Auth.Contracts.UserStorage.Responses;
using Server.Logic.Auth.Models;

namespace Server.Logic.Auth.Contracts.UserStorage;

public interface IUserStorage
{
    public UserStorageCreateUserResponse CreateUser(User user);
    public UserStorageGetUserByLoginResponse GetUserByLogin(string login);
}