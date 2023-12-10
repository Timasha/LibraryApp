using Server.Logic.Auth.Responses.Base;

namespace Server.Logic.Auth.Responses;

public class AuthLogicRegisterAdminUserResponse : BaseAuthLogicResponse
{
    public AuthLogicRegisterAdminUserResponse(AuthLogicResponsesStatusCode statusCode) : base(statusCode)
    {
    }
}