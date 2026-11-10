using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapDelete("/{id}", HandleAsync)
            .WithName("Categories : deletar uma categoria")
            .WithSummary("deletar categoria")
            .WithDescription("deletar categoria")
            .WithOrder(4)
            .Produces<Response<Category?>>();
    
    private static async Task<IResult> HandleAsync(
        [FromQuery]long id,
        ICategoryHandler handler)
    {
        var reque = new DeleteCategoryRequest
        {
            Id = id,
            UserId = "lucas@123"
        };
        
        var result = await handler.DeleteAsync(reque);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.NotFound(result);   
        
    }
}