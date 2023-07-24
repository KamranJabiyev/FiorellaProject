using System.Net;

namespace Fiorella.Persistence.Exceptions;

public class SignInFailerException : Exception, IBaseException
{
    public int StatusCode { get; set; }
    public string CustomMessage { get; set; }
    public SignInFailerException(string message):base(message)
    {
        StatusCode=(int)HttpStatusCode.BadRequest;
        CustomMessage=message;
    }
}
