using Fina.Api.Data;
using Fina.Core.Common;
using Fina.Core.Enums;
using Fina.Core.Handler;
using Fina.Core.Models;
using Fina.Core.Requests.Transactions;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;

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
        try
        {
            if (request is { Type: ETransactionType.WithDraw, Amount: >= 0 })
                request.Amount *= -1;

            var transaction = new Transaction()
            {
                CategoryId = request.CategoryId,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                Type = request.Type, 
                Amount = request.Amount,
                Title = request.Title,
                UserId = request.UserId,
            };

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return new Response<Transaction>(null, message: "Transação criada com sucesso.");
        }
        catch (Exception ex)
        {
            return new Response<Transaction>(null, 500, "Erro ao criar transação");
        }
    }

    public async Task<Response<Transaction>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = _context.Transactions.FirstOrDefault(x => x.Id == request.Id && x.UserId == request.UserId);
            if (transaction != null)
                return new Response<Transaction>(null, 204, "Transação não encontrada.");

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return new Response<Transaction>(null, 200, "Transação excluida com sucesso.");
        }
        catch (Exception ex)
        {
            return new Response<Transaction>(null, 500, "Erro ao excluir transação");
        }
    }

    public async Task<Response<Transaction>?> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = _context.Transactions.AsNoTracking().FirstOrDefault(x => x.Id == request.Id && x.UserId == request.UserId);
            if (transaction == null)
                return new Response<Transaction>(null, 204, "Transação não encontrada.");
            return new Response<Transaction>(transaction);
        }
        catch (Exception ex)
        {
            return new Response<Transaction>(null, 500, "Erro ao retornar transação.");
        }
    }

    public async Task<Response<List<Transaction>>?> GetPeriodAsync(GetTransactionByPeriodRequest request)
    {
        try 
        {
            request.StartDate ??= DateTime.Now.GetFirstDay();
            request.EndDate ??= DateTime.Now.GetLastDay();

            var transaction = await _context
                                   .Transactions
                                   .AsNoTracking()
                                   .Where(x => 
                                          x.PaidOrReceivedAt >= request.StartDate &&
                                          x.PaidOrReceivedAt <= request.EndDate &&
                                          x.UserId == request.UserId)
                                   .OrderBy(x => x.PaidOrReceivedAt)
                                   .ToListAsync();

            var count = transaction.Count();

            transaction = transaction.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToList();
            return new Response<List<Transaction>>(transaction, countPageTotals: count, pageSize: request.PageSize, pageNumber: request.PageNumber);
        }
        catch(Exception ex) 
        {
            return new Response<List<Transaction>>(null, 500, message: "Erro ao retornar transações");
        }
    }

    public async Task<Response<Transaction>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = _context.Transactions.FirstOrDefault(x => x.Id == request.Id && x.UserId == request.UserId);
            if (transaction == null)
                return new Response<Transaction>(null, 204, "Categoria não encontrada.");

            if (request is { Type: ETransactionType.WithDraw, Amount: >= 0 })
                request.Amount *= -1;

            transaction.CategoryId = request.CategoryId;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
            transaction.Type = request.Type;
            transaction.Amount = request.Amount;
            transaction.Title = request.Title;
            transaction.UserId = request.UserId;

            await _context.SaveChangesAsync();
            return new Response<Transaction>(transaction);
        }
        catch (Exception ex)
        {
            return new Response<Transaction>(null, 500, "Erro ao atualizar categoria");
        }
    }
}
