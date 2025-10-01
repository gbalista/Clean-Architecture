using System.Net;

namespace Core.Exceptions;
public class ArcException : Exception
{
    public IEnumerable<string> ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }

    public ArcException(string message, IEnumerable<string> errors, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }

    public ArcException(string message) : base(message)
    {
        ErrorMessages = new List<string>();
    }
}
