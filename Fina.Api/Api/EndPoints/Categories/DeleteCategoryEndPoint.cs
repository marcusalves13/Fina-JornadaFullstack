using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Api.EndPoints.Categories;

public class DeleteCategoryEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", HandleAsync)
            .WithName("Categories: Delete")
            .WithSummary("Deleta uma categoria")
            .WithOrder(3)
            .Produces<Response<Category?>>();
    }
    private static async Task<IResult> HandleAsync(ICategoryHandler handle, long id)
    {
        var request = new DeleteCategoryRequest()
        {
            Id = id,
            UserId = ApiConfiguration.UserId
        };
        var response = await handle.DeleteAsync(request);
        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}
