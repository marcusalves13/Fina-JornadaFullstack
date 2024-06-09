using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Fina.Core.Responses;

public class Response<TData> where TData : class
{
    private int _code = 200;

    [JsonIgnore]
    public bool IsSuccess => _code is >= 200 and <= 299;

    //[JsonConstructor]
    public Response()
    {
        _code = Configuration.DefaultStatusCode;
    }
    public Response(TData? data, int code = Configuration.DefaultStatusCode, string? message = null, int countPageTotals = 0, int pageNumber = 0, int pageSize = 0)
    {
        Data = data;
        _code = code;
        Message = message;
        CurrentPage = pageNumber;
        PageSize = pageSize;
        TotalCount = countPageTotals;

    }
    public int CurrentPage { get; set; }
    public int TotalPages => (int)Math.Ceiling(CurrentPage / (double)PageSize);
    public int PageSize { get; set; } = Configuration.DefaultPageSize;
    public int TotalCount { get; set; }
    public TData Data { get; set; }
    public string Message { get; set; }
}
