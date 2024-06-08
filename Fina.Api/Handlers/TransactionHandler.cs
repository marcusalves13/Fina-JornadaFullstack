using Fina.Api.Data;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;

namespace Fina.Api.Handlers;

public class TransactionHandler : ITransactionHandler
{
    private AppDbContext _context { get; set; }
    public TransactionHandler(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Response<Transaction>?> CreateAsync(CreateTransactionRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Transaction>?> GetAsync(GetTransactionByIdRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<List<Transaction>>?> GetPeriodAsync(GetTransactionByPeriodRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<Transaction>> UpdateAsync(UpdateTransactionRequest request)
    {
        throw new NotImplementedException();
    }
}
