using Dima.Api.Common.Api;
using Dima.Core.Models;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class CreatedCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .Produces<Response<Category?>>();
    
    private static async Task<> HandleAsync()
}