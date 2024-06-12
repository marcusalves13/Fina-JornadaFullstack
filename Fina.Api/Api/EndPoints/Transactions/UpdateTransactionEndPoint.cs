using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Api.EndPoints.Transactions;

public class UpdateTransactionEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("/{id}",HandleAsync)
            .WithName("Transactions:Update")
            .WithDescription("Atualiza transação")
            .WithOrder(2)
            .Produces<Response<Transaction>?>();
    }
    private static async Task<IResult> HandleAsync(ITransactionHandler handler, UpdateTransactionRequest request ,long id)
    {
        request.Id = id;
        request.UserId = ApiConfiguration.UserId;
        var response = await handler.UpdateAsync(request);
        return response.IsSuccess? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}
