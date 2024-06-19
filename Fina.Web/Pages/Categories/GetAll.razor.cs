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
    [Inject]
    public IDialogService Dialog { get; set; } = null!;
    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var request = new GetAllCategoryRequest();
            var result = await Handler.GetAllAsync(request);
            if (!result.IsSuccess)
            {
                throw new Exception();
            }
             Categories = result.Data ?? [];
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
    #endregion

    public async void OnDeleteButtonClickedAsync(long id, string title)
    {
        var result = await Dialog.ShowMessageBox("ATENÇÃO",
                                                 $"Deseja remover a categoria {title}?",
                                                 yesText: "Excluir",
                                                 cancelText: "Cancelar");
        if (result is true)
        {
            await OnDeleteAsync(id, title);
        }
        StateHasChanged();
    }

    public async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            var request = new DeleteCategoryRequest() 
            {
                Id = id
            };
            await Handler.DeleteAsync(request);
            Categories.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Categoria {title} removida.",Severity.Info);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

}
