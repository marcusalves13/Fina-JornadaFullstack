using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Categories;

public class GetCategoryByIdRequest:Request
{
    [Required(ErrorMessage = "ID Inválido")]
    public long Id { get; set; }
}
