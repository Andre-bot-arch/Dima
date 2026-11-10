using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Categories;

public class GetAllCategoriesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapGet("/", HandleAsync)
            .WithName("Categories : Get all")
            .WithSummary("receber as categorias")
            .WithDescription("receber as categorias")
            .WithOrder(2)
            .Produces<PagedResponse<List<Category>?>>();
    
    
    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler, 
        [FromQuery]int pageNumber = Configuration.DefaultPageNumber,
        [FromQuery]int pageSize = Configuration.DefaultPageSize)
    {
        var request = new GetAllCategoriesRequest
        {
            UserId = "andre@123",
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await handler.GetAllAsync(request);
        result.Message = "All Categories";
        
        
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest();



    }

}