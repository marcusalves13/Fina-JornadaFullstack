using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;

namespace Fina.Core.Handler;

public interface ICategoryHandler
{
    Task<Response<Category>?> CreateAsync(CreateCategoryRequest request);
    Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request);
    Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request);
    Task<Response<Category>?> GetAsync(GetCategoryByIdRequest request);
    Task<Response<List<Category>>?> GetAllAsync(GetAllCategoryRequest request);
}
