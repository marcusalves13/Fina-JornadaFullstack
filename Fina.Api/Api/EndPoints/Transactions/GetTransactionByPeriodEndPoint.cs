using Fina.Api.Common.Api;
using Fina.Core;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Api.EndPoints.Transactions;

public class GetTransactionByPeriodEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/", HandleAsync)
            .WithName("Transaction:GetByPeriod")
            .WithSummary("Retorna transações por periodo")
            .WithOrder(5)
            .Produces<List<Response<Transaction?>>>();
    }
    private static async Task<IResult> HandleAsync
        (ITransactionHandler handler,
         [FromQuery] DateTime? startDate = null,
         [FromQuery] DateTime? endDate = null,
         [FromQuery] int pagenumber = Configuration.DefaultPageNumber,
         [FromQuery] int pagesize = Configuration.DefaultPageSize
        ) 
    {
        var request = new GetTransactionByPeriodRequest()
        {
            StartDate = startDate,
            EndDate = endDate,
            PageNumber = pagenumber,
            PageSize = pagesize,
            UserId = ApiConfiguration.UserId
        };
        var response = await handler.GetPeriodAsync(request);
        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}
