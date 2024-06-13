using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Api.EndPoints.Transactions;

public class GetTransactionByIdEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Transaction:GetById")
            .WithDescription("Retorna uma transação por ID")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();
    }
    private static async Task<IResult> HandleAsync(ITransactionHandler handler, long id)
    {
        var request = new GetTransactionByIdRequest()
        {
            Id = id,
            UserId = ApiConfiguration.UserId
        };
        var response = await handler.GetByIdAsync(request);
        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}
