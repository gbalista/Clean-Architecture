using System.Collections.ObjectModel;
using System.Net;

namespace Core.Exceptions;
public class NotFoundException : ArcException
{
    public NotFoundException(string message)
        : base(message, new Collection<string>(), HttpStatusCode.NotFound)
    {
    }
}
