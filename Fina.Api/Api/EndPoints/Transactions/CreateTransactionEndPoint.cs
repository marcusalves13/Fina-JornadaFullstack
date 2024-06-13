using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Builder;

namespace Fina.Api.Api.EndPoints.Transactions;

public class CreateTransactionEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Transactions:Create")
            .WithDescription("Cria uma nova transação")
            .WithOrder(1)
            .Produces<Response<Transaction?>>();
    }
    private static async Task<IResult> HandleAsync(ITransactionHandler handler, CreateTransactionRequest request) 
    {
        request.UserId = ApiConfiguration.UserId;
        var response = await handler.CreateAsync(request);
        return response.IsSuccess ? TypedResults.Created($"v1/transactions/{response.Data?.Id}") : TypedResults.BadRequest(response);
    }
}
