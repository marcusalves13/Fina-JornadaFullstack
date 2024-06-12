﻿using Fina.Api.Common.Api;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Api.Api.EndPoints.Categories;

public class CreateCategoryEndPoint : IEndPoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Cria uma nova categoria")
            .WithOrder(1)
            .Produces<Response<Category?>>();
    }
    private static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request) 
    {
        request.UserId = ApiConfiguration.UserId;
        var response = await handler.CreateAsync(request);
        return response.IsSuccess ? TypedResults.Created($"vi/categories/{response.Data?.Id}") : TypedResults.BadRequest(response);
    }
}