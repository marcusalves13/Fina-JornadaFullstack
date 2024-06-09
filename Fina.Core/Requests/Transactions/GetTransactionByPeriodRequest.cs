using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Transactions;

public class GetTransactionByPeriodRequest:Request
{
    [Required(ErrorMessage = "Data Inicial Inválida")]
    public DateTime? StartDate { get; set; }

    [Required(ErrorMessage = "Data Final Inválida")]
    public DateTime? EndDate { get; set; }
}
