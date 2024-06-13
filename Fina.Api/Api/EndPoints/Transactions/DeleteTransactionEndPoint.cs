using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Api.EndPoints.Transactions;

public class DeleteTransactionEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Transaction:Delete")
            .WithDescription("Excluir transação")
            .WithOrder(3)
            .Produces<Response<Transaction?>>();
    }
    private static async Task<IResult> HandleAsync(ITransactionHandler handler,long id)
    {
        var request = new DeleteTransactionRequest()
        {
            UserId = ApiConfiguration.UserId,
            Id = id
        };
        var response = await handler.DeleteAsync(request);
        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}
