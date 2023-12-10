using Server.Logic.Auth.Responses.Base;

namespace Server.Logic.Auth.Responses;

public class AuthLogicRegisterSimpleUserResponse : BaseAuthLogicResponse
{
    public AuthLogicRegisterSimpleUserResponse(AuthLogicResponsesStatusCode statusCode) : base(statusCode)
    {
    }
}