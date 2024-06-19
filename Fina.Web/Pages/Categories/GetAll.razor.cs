using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Categories;

public partial class GetAllCtagoriesPage:ComponentBase
{
    #region Properties
    public bool IsBusy { get; set; } = false;
    public List<Category> Categories { get; set; } = new();
    #endregion

    #region Services 
    [Inject]
    public ICategoryHandler Handler { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    #endregion

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var request = new GetAllCategoryRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess) 
            {
                Categories = result.Data ?? [];
            }
        } 
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally 
        {
            IsBusy = false;
        }

    }

}
