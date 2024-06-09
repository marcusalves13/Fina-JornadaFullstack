namespace Fina.Core.Requests;

public abstract class Request
{
    public string UserId { get; set; } = string.Empty;
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
    public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
}
