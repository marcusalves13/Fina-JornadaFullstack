using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Fina.Api.Api.EndPoints.Categories;

public class GetAllCategoryEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/",HandleAsync)
            .WithName("Categories: GetAll")
            .WithSummary("Retorna todas categorias")
            .WithOrder(5)
            .Produces<Response<List<Category?>>>();
    }
    private static async Task<IResult> HandleAsync
        (ICategoryHandler handler, [FromQuery] int PageSize, [FromQuery] int PageNumber) 
    {
        var request = new GetAllCategoryRequest()
        {
            PageSize = PageSize,
            PageNumber = PageNumber
        };
        var response = await handler.GetAllAsync(request);
        return response.IsSuccess ? TypedResults.Ok(response) : TypedResults.BadRequest(response);
    }
}
