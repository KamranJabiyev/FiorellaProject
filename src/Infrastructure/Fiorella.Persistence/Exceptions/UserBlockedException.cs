using System.Net;

namespace Fiorella.Persistence.Exceptions;

public class UserBlockedException : Exception, IBaseException
{
    public int StatusCode { get; set; }
    public string CustomMessage { get; set; }
    public UserBlockedException(string message):base(message)
    {
        StatusCode=(int)HttpStatusCode.Locked;
        CustomMessage = message;
    }
}
