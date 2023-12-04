using Server.Logic.Auth.Contracts.UserStorage;
using Server.Logic.Auth.Contracts.UserStorage.Responses;
using Server.Logic.Auth.Contracts.UserStorage.Responses.Base;
using Server.Logic.Auth.Models;
using Server.Logic.Auth.Responses;
using Server.Logic.Auth.Responses.Base;
using ILogger = Server.Global.Logger.ILogger;
using LogLevel = Server.Global.Logger.LogLevel;

namespace Server.Logic.Auth;

public class AuthLogicProvider
{
    private IUserStorage _userStorage;
    private ILogger _logger;

    public AuthLogicProvider(IUserStorage userStorage, ILogger logger)
    {
        _userStorage = userStorage;
        _logger = logger;
    }

    public AuthLogicAuthenticateResponse Authenticate(string login, string password)
    {
        UserStorageGetUserByLoginResponse response = _userStorage.GetUserByLogin(login);
        _logger.Log(LogLevel.Info,$"Get user by login while authenticate status: {response.StatusCode.GetType()}");
        
        switch (response.StatusCode)
        {
            case UserStorageStatusCode.UserNotExists:
            {
                return new AuthLogicAuthenticateResponse(AuthLogicResponsesStatusCode.WrongLoginOrPassword,"","");
            }
            case UserStorageStatusCode.Ok:
            {
                break;
            }
        }
        // Here I compare not hashed password, because its example code
        // TODO: make hashed password compare here
        if (password != response.User.Password)
        {
            return new AuthLogicAuthenticateResponse(AuthLogicResponsesStatusCode.WrongLoginOrPassword, "", "");
        }
        
        return new AuthLogicAuthenticateResponse(AuthLogicResponsesStatusCode.Ok,response.User.Login,response.User.Role.Name);
    }

    public AuthLogicRegisterSimpleUserResponse RegisterSimpleUser(User user)
    {
        user.Role = BasicRoles.SimplyUserRole;
        UserStorageCreateUserResponse response = _userStorage.CreateUser(user);
        
        _logger.Log(LogLevel.Info,$"Create user while register simple user status: {response.StatusCode.GetType()}");
        
        switch (response.StatusCode)
        {
            case UserStorageStatusCode.Ok:
            {
                break;
            }
            case UserStorageStatusCode.UserAlreadyExists:
            {
                return new AuthLogicRegisterSimpleUserResponse(AuthLogicResponsesStatusCode.UserAlreadyExists);
            }
        }

        return new AuthLogicRegisterSimpleUserResponse(AuthLogicResponsesStatusCode.Ok);
    }

    public AuthLogicRegisterAdminUserResponse RegisterAdminUser(User user)
    {
        user.Role = BasicRoles.AdminRole;
        UserStorageCreateUserResponse response = _userStorage.CreateUser(user);
        
        _logger.Log(LogLevel.Info,$"Create user while register admin user status: {response.StatusCode.GetType()}");

        switch (response.StatusCode)
        {
            case UserStorageStatusCode.Ok:
            {
                break;
            }
            case UserStorageStatusCode.UserAlreadyExists:
            {
                return new AuthLogicRegisterAdminUserResponse(AuthLogicResponsesStatusCode.UserAlreadyExists);
            }
        }

        return new AuthLogicRegisterAdminUserResponse(AuthLogicResponsesStatusCode.Ok);
    }
}