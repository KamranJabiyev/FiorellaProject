using System.Net;

namespace Fiorella.Persistence.Exceptions;


public sealed class DuplicatedException : Exception, IBaseException
{
    public int StatusCode { get; set; }

    public string CustomMessage { get; set; }
    public DuplicatedException(string message):base(message)
    {
        StatusCode = (int)HttpStatusCode.Conflict;
        CustomMessage = message;
    }
}
