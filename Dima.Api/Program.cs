using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
 
var cnnString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(cnnString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("v1/categories",
    async (ICategoryHandler handler)
        =>
    {
        var request = new GetAllCategoriesRequest
        {
            UserId = "andre@123"
        };

        return await handler.GetAllAsync(request);
    })
    .WithName("categories: Get")
    .WithSummary("Buscar todos os categorias")
    .Produces<PagedResponse<List<Category>?>>();

app.MapGet("v1/categories/{id}",
        async (long id,
               ICategoryHandler handler)
        => {
           var request = new GetCategoryByIdRequest
           {
             Id = id

           };
           return await handler.GetAsync(request);
        })
        .WithName("categories: GetId")
        .WithSummary("Buscar por id")
        .Produces<Response<Category?>>();


app.MapPost("v1/categories",
        async (CreateCategoryRequest request,
               ICategoryHandler handler)
        =>  await handler.CreatedAsync(request))
    .WithName("categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category?>>();

app.MapPut("v1/categories/{id}",
        async (long id,
               UpdateCategoryRequest request,
               ICategoryHandler handler)
        => {
            request.Id = id;
            return await handler.UpdateAsync(request);
        })
        .WithName("categories: Update")
        .WithSummary("Atualizar uma categoria")
        .Produces<Response<Category?>>();


app.MapDelete("v1/categories/{id}",
        async (long id,
              ICategoryHandler handler)
        => {
            var request = new DeleteCategoryRequest
            {
                Id = id
                
            };
            return await handler.DeleteAsync(request);
        })
    .WithName("categories: Delete")
    .WithSummary("Deletar uma categoria")
    .Produces<Response<Category>>();

app.Run();
