using Dima.Api.Data;
using Dima.Core.Common.Extensions;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class TransactionHandler(AppDbContext context) : ITransactionHandler
{
    public async Task<Response<Transaction?>> CreateAsync(CreateTransactionRequest request)
    {
        try
        {
            var transaction = new Transaction
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                CreatedAt = DateTime.Now,
                Amount = request.Amount,
                PaidOrReceivedAt = request.PaidOrReceivedAt,
                Title = request.Title,
                Type = request.Type
            };
            
            await context.Transactions.AddAsync(transaction);
            await context.SaveChangesAsync();
            
            return new Response<Transaction?>(transaction, 201, "Created");

        }
        catch (Exception e)
        {
            return new Response<Transaction?>(null, 500, e.Message);
        }
    }

    public async Task<Response<Transaction?>> UpdateAsync(UpdateTransactionRequest request)
    {
        try
        {
            var transaction = await context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == request.Id);
            
            if (transaction == null)
                 return  new Response<Transaction>(null, 404, "Not found");

            transaction.CategoryId = request.CategoryId;
            transaction.Amount = request.Amount;
            transaction.Title = request.Title;
            transaction.Type = request.Type;
            transaction.PaidOrReceivedAt = request.PaidOrReceivedAt;
            
            context.Transactions.Update(transaction);
            await context.SaveChangesAsync();
            
            return new Response<Transaction?>(transaction, 200, "Updated");

        }
        catch (Exception e)
        {
            return new Response<Transaction?>(null, 500, e.Message);
        }
    }

    public async Task<Response<Transaction?>> DeleteAsync(DeleteTransactionRequest request)
    {
        try
        {
            var transaction = await context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == request.Id);
            
            if (transaction == null)
                return  new Response<Transaction>(null, 404, "Not found");

            context.Transactions.Remove(transaction);
            await context.SaveChangesAsync();
            
            return new Response<Transaction?>(transaction, 200, "Deleted");

        }
        catch (Exception e)
        {
            return new Response<Transaction?>(null, 500, e.Message);
        }
    }

    public async Task<Response<Transaction?>> GetByIdAsync(GetTransactionByIdRequest request)
    {
        try
        {
            var transaction = await context.Transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == request.Id);
            
            if (transaction == null)
                return  new Response<Transaction>(null, 404, "Not found");
            
            return new Response<Transaction?>(transaction, 200, "Received");
        }
        catch (Exception e)
        {
            return  new Response<Transaction>(null, 500, e.Message);
        }
    }

    public async Task<PagedResponse<List<Transaction?>>> GetByPeriodoAsync(GetTransactionByPeriodRequest request)
    {
        try
        {
            request.StartDate ??= DateTime.Now.GetFirstDay();
            request.EndDate ??= DateTime.Now.GetLastDay();
            
        }
        catch (Exception e)
        {
            return  new PagedResponse<List<Transaction>>(null, 500, e.Message);
         
        }

        try
        {
            var query = context.Transactions
                .AsNoTracking()
                .Where(x => x.CreatedAt >= request.StartDate && x.CreatedAt <= request.EndDate)
                .OrderBy(x => x.CreatedAt);

            var transactions = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return new PagedResponse<List<Transaction>?>(
                transactions,
                count,
                request.PageNumber,
                request.PageSize);

        }
        catch (Exception e)
        {
            return  new PagedResponse<List<Transaction>>(null, 500, e.Message);
        }
        






    }
}