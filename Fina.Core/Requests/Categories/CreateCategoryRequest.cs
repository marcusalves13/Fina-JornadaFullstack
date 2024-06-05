using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Categories;

public class CreateCategoryRequest:Request
{
    [Required(ErrorMessage = "Titulo Inválido")]
    [MaxLength(80,ErrorMessage = "Titulo deve conter no máximo 80 caracteres.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Descrição Inválida")]
    public string Description { get; set; } = string.Empty;
}
