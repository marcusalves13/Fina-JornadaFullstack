using static System.Net.WebRequestMethods;

namespace Fina.Core;

public static class Configuration
{
    public const int DefaultPageSize  = 25;
    public const int DefaultPageNumber = 1;
    public const int DefaultStatusCode = 200;
    public static string BackEndUrl { get; set; } = "http://localhost:5126";
    public static string FrontEndUrl { get; set; } = "http://localhost:5188";
}
