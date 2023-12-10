using Server.Logic.Auth.Contracts.UserStorage.Responses.Base;
using Server.Logic.Auth.Models;

namespace Server.Logic.Auth.Contracts.UserStorage.Responses;

public class UserStorageGetUserByLoginResponse : BaseUserStorageResponse
{
    public User User { get; set; }
    public UserStorageGetUserByLoginResponse(UserStorageStatusCode statusCode, User user) : base(statusCode)
    {
        User = user;
    }
}