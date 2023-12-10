namespace Server.Logic.Auth.Contracts.UserStorage.Responses.Base;

public class UserStorageStatusFabric
{
    public static string Status(UserStorageStatusCode code)
    {
        switch (code)
        {
            case UserStorageStatusCode.Ok:
            {
                return "ok";
            }
            case UserStorageStatusCode.UserAlreadyExists:
            {
                return "user already exists";
            }
            case UserStorageStatusCode.UserNotExists:
            {
                return "user not exists";
            }
        }

        return "";
    }
}