using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Api.EndPoints.Categories;

public class GetCategoryByIdEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
            .WithName("Categories: GetById")
            .WithSummary("Retorna uma categoria por Id")
            .WithOrder(4)
            .Produces<Response<Category?>>();
    }
    private static async Task<IResult> HandleAsync(ICategoryHandler handler, int id) 
    {
        var request = new GetCategoryByIdRequest()
        {
            Id = id,
            UserId = ApiConfiguration.UserId
        };
        var response = await handler.GetByIdAsync(request);
        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}
