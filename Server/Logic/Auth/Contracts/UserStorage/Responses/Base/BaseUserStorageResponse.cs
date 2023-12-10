namespace Server.Logic.Auth.Contracts.UserStorage.Responses.Base;

public abstract class BaseUserStorageResponse
{
    protected BaseUserStorageResponse(UserStorageStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public UserStorageStatusCode StatusCode { get; set; }
}