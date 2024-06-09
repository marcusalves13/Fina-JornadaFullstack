﻿using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Transactions;

public class DeleteTransactionRequest:Request
{
    [Required(ErrorMessage = "Id Inválido")]
    public long Id { get; set; }
}
