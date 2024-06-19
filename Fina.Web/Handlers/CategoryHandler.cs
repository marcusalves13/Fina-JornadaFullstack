using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using System.Net.Http.Json;

namespace Fina.Web.Handlers;

public class CategoryHandler(IHttpClientFactory httpClientFactory) : ICategoryHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);
    public async Task<Response<Category>?> CreateAsync(CreateCategoryRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/categories", request);
        return await result.Content.ReadFromJsonAsync<Response<Category>?>()?? new Response<Category?>(null,400,"Falha ao criar categoria");
    }

    public async Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
    {
        var result = await _client.DeleteAsync($"v1/categories/{request.Id}");
        return await result.Content.ReadFromJsonAsync<Response<Category>?>() ?? new Response<Category?>(null, 400, "Falha ao excluir categoria");
    }

    public async Task<Response<List<Category>>?> GetAllAsync(GetAllCategoryRequest request)
    {
        var result = await _client.GetFromJsonAsync<Response<List<Category>>?>("v1/categories/");
        return result;
    }

    public async Task<Response<Category>?> GetByIdAsync(GetCategoryByIdRequest request)
    {
        var result = await _client.GetFromJsonAsync<Response<Category>?>($"v1/categories/{request.Id}");
        return result;
    }

    public async Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request)
    {
        var result = await _client.PutAsJsonAsync($"v1/categories/{request.Id}",request);
        return await result.Content.ReadFromJsonAsync<Response<Category>>()?? new Response<Category?>(null, 400, "Falha ao atualizar categoria"); ;
    }
}
