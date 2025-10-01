using System.Collections.ObjectModel;


namespace Core.Cors;

public class CorsOptions
{
    public CorsOptions()
    {
        AllowedOrigins = [];
    }

    public Collection<string> AllowedOrigins { get; }
}
