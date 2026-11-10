using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Categories;

public class UpdateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPut("/{id}", HandleAsync)
            .WithName("Categories : update categorias")
            .WithSummary("atualizar uma categoria")
            .WithDescription("atualizar uma nova categoria")
            .WithOrder(3)
            .Produces<Response<Category?>>();
    
    private static async Task<IResult> HandleAsync(
        [FromQuery]long id,
        ICategoryHandler handler, UpdateCategoryRequest request)
    {
        request.Id = id;
        request.UserId = "lucas@123";
        
        var result = await handler.UpdateAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.NotFound(result);   
        
    }
}

