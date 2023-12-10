namespace Server.Logic.Auth.Responses.Base;

public abstract class BaseAuthLogicResponse
{
    protected BaseAuthLogicResponse(AuthLogicResponsesStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public AuthLogicResponsesStatusCode StatusCode { get; set; } 
}