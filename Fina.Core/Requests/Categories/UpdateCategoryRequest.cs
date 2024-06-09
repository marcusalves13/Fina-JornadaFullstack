using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Categories;

public class UpdateCategoryRequest:Request
{
    [Required(ErrorMessage = "ID Inválido")]
    public long Id { get; set; }

    [Required(ErrorMessage = "Titulo Inválido")]
    [MaxLength(80, ErrorMessage = "Titulo deve conter no máximo 80 caracteres.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Descrição Inválida")]
    public string Description { get; set; } = string.Empty;
}

