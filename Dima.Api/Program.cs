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

app.MapPost("v1/categories", async (CreateCategoryRequest request, ICategoryHandler handler) => await handler.CreatedAsync(request))
    .WithName("categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category?>>();

app.MapPut("v1/categories/{id}", async (long id, UpdateCategoryRequest request, ICategoryHandler handler) =>
    {
        request.Id = id;
        await handler.UpdateAsync(request);
    })
    .WithName("categories: Update")
    .WithSummary("Atualizar uma categoria")
    .Produces<Response<Category?>>();


app.MapDelete("v1/categories/{id}", async (long id, ICategoryHandler handler) =>
    {
        var request = new DeleteCategoryRequest
        {
            Id = id
        };
        await handler.DeleteAsync(request);
    })
    .WithName("categories: Delete")
    .WithSummary("Deletar uma categoria")
    .Produces<Response<Category>>();


app.Run();
