using System.Transactions;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions;

public class UpdateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories : update transaction")
            .WithSummary("atualizar uma transaction")
            .WithDescription("atualizar uma transaction")
            .WithOrder(2)
            .Produces<Response<Transaction?>>();
    
    private static async Task<IResult> HandleAsync(
        [FromQuery]long id,
        ITransactionHandler handler, UpdateTransactionRequest request)
    {
        request.Id = id;
        request.UserId = "lucas@123";
        
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.NotFound(result);   
        
    }
    
}