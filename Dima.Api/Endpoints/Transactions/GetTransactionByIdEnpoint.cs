using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionByIdEnpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Transactions : Buscar por id")
            .WithSummary("Buscar por id")
            .WithDescription("Buscar por id")
            .WithOrder(4)
            .Produces<Response<Transaction?>>();
    
    private static async Task<IResult> HandleAsync(
        [FromQuery]long id,
        ITransactionHandler handler)
    {
        var reque = new GetTransactionByIdRequest()
        {
            Id = id,
            UserId = "lucas@123"
        };
        
        var result = await handler.GetByIdAsync(reque);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.NotFound(result);   
        
    }
}