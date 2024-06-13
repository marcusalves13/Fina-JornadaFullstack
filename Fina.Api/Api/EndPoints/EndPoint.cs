using Fina.Api.Api.EndPoints.Categories;
using Fina.Api.Api.EndPoints.Transactions;
using Fina.Api.Common.Api;

namespace Fina.Api.Api.EndPoints;

public static class EndPoint
{
    public static void MapEndPoints(this WebApplication app) 
    {
        var endPoints = app.MapGroup("");
        endPoints.MapGroup("/")
            .WithTags("Health Check")
            .MapGet("/", () => new { message = "Ok" });

        endPoints.MapGroup("v1/categories")
            .WithTags("Categories")
            .MapEndpoint<CreateCategoryEndPoint>()
            .MapEndpoint<UpdateCategoryEndPoint>()
            .MapEndpoint<DeleteCategoryEndPoint>()
            .MapEndpoint<GetCategoryByIdEndPoint>()
            .MapEndpoint<GetAllCategoryEndPoint>();

        endPoints.MapGroup("v1/transactions")
            .WithTags("Transactions")
            .MapEndpoint<CreateTransactionEndPoint>()
            .MapEndpoint<UpdateTransactionEndPoint>()
            .MapEndpoint<DeleteTransactionEndPoint>()
            .MapEndpoint<GetTransactionByIdEndPoint>()
            .MapEndpoint<GetTransactionByPeriodEndPoint>();

    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndPoint
    {
        TEndpoint.Map(app);
        return app;
    }
}
