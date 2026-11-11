using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionByPeriodEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Transactions : Buscar todos")
            .WithSummary("Buscar transactions por periodo")
            .WithDescription("Buscar todos")
            .WithOrder(5)
            .Produces<PagedResponse<List<Transaction>?>>();
    
    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null,
        [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery] int pageSize = Configuration.DefaultPageSize
        )
    {
        var reque = new GetTransactionByPeriodRequest()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            StartDate = startDate,
            EndDate = endDate
        };
        
        var result = await handler.GetByPeriodoAsync(reque);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.NotFound(result);   
        
    }
    
}