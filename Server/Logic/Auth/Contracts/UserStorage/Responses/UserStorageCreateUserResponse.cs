using Server.Logic.Auth.Contracts.UserStorage.Responses.Base;

namespace Server.Logic.Auth.Contracts.UserStorage.Responses;

public class UserStorageCreateUserResponse : BaseUserStorageResponse
{
    public UserStorageCreateUserResponse(UserStorageStatusCode statusCode) : base(statusCode)
    {
    }
}