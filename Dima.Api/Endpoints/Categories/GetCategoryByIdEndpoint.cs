using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/{id}", HandleAsync)
            .WithName("Categories : Buscar por id")
            .WithSummary("Buscar por id")
            .WithDescription("Buscar por id")
            .WithOrder(5)
            .Produces<Response<Category?>>();
    
    private static async Task<IResult> HandleAsync(
        [FromQuery]long id,
        ICategoryHandler handler,
        ClaimsPrincipal user)
    {
        var reque = new GetCategoryByIdRequest()
        {
            Id = id,
            UserId = user.Identity?.Name ?? string.Empty
        };
        
        var result = await handler.GetAsync(reque);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.NotFound(result);   
        
    }
}