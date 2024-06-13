namespace Fina.Core;

public static class Configuration
{
    public const int DefaultPageSize  = 25;
    public const int DefaultPageNumber = 1;
    public const int DefaultStatusCode = 200;
    public static string BackEndUrl { get; set; } = string.Empty;
    public static string FontEndUrl { get; set; } = string.Empty;
}
