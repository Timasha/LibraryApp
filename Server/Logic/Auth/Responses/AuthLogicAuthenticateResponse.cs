using Server.Logic.Auth.Responses.Base;

namespace Server.Logic.Auth.Responses;

public class AuthLogicAuthenticateResponse : BaseAuthLogicResponse
{
    public string Login { get; set; }
    public string RoleName { get; set; }
    
    public AuthLogicAuthenticateResponse(AuthLogicResponsesStatusCode statusCode, string login, string roleName) : base(statusCode)
    {
        Login = login;
        RoleName = roleName;
    }
}