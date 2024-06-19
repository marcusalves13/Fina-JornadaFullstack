using Fina.Api.Data;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Fina.Api.Handlers;

public class CategoryHandler : ICategoryHandler
{
    private AppDbContext _context { get; set; }

    public CategoryHandler(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Response<Category>?> CreateAsync(CreateCategoryRequest request)
    {
        try 
        {
            await Task.Delay(5000);
            var category = new Category()
            {
                UserId = request.UserId,
                Description = request.Description,
                Title = request.Title
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return new Response<Category>(category,message:"Categoria criada com sucesso.");
        }catch (Exception ex)
        {
            return new Response<Category>(null, 500, "Erro ao criar categoria");
        }
    }

    public async Task<Response<Category>> DeleteAsync(DeleteCategoryRequest request)
    {
        try 
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if (category == null)
                return new Response<Category>(null, 204, "Categoria não encontrada.");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return new Response<Category>(null,200, "Categoria excluida com sucesso.");
        }
        catch (Exception ex) 
        {
            return new Response<Category>(null, 500, "Erro ao excluir categoria");
        }
    }

    public async Task<Response<List<Category>>?> GetAllAsync(GetAllCategoryRequest request)
    {
        try 
        {
            var categories = await _context
                                   .Categories
                                   .AsNoTracking()
                                   .Where(x => x.UserId == request.UserId)
                                   .OrderBy(x => x.Title)
                                   .ToListAsync();

            var count = categories.Count();

            categories = categories.Skip((request.PageNumber -1) * request.PageSize).Take(request.PageSize).ToList();
            return new Response<List<Category>>(categories, countPageTotals: count, pageSize: request.PageSize ,pageNumber: request.PageNumber);
        }
        catch (Exception ex) 
        {
            return new Response<List<Category>>(null, 500,message: "Erro ao retornar categorias");

        }
    }

    public async Task<Response<Category>?> GetByIdAsync(GetCategoryByIdRequest request)
    {
        try 
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if (category == null)
                return new Response<Category>(null, 204, "Categoria não encontrada.");
            return new Response<Category>(category);
        }
        catch(Exception ex) 
        {
            return new Response<Category>(null, 500, "Erro ao retornar categoria");
        }

    }

    public async Task<Response<Category>> UpdateAsync(UpdateCategoryRequest request)
    {
        try 
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if (category == null)
                return new Response<Category>(null, 204, "Categoria não encontrada.");
            category.Description = request.Description;
            category.Title = request.Title;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return new Response<Category>(category);
        }
        catch (Exception ex) 
        {
            return new Response<Category>(null, 500, "Erro ao atualizar categoria");
        }
    }
}
