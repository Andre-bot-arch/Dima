using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class CategoryHandler(AppDbContext context) : ICategoryHandler
{
    public async Task<Response<Category?>> CreatedAsync(CreateCategoryRequest request)
    {
        try
        {
            var category = new Category
            {
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description
            };

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 201,  "Category created");
        }
        catch (Exception e)
        {
            return new Response<Category?>(null, 500, e.Message);
        }
    }

    public async Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request)
    {
        try
        {
            var category = await context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

            if (category is null)
                return new Response<Category>(null, 404, "Category not found");

            category.Title = request.Title;
            category.Description = request.Description;

            context.Update(category);
            await context.SaveChangesAsync();
            return new Response<Category?>(category);
        }
        catch (Exception e)
        {
            return new Response<Category?>(null, 500, e.Message);
        }

    }

    public async Task<Response<Category?>> DeleteAsync(DeleteCategoryRequest request)
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
            if (category is null)
                return new Response<Category?>(null, 404, "Category not found");
            
            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return new Response<Category?>(category, 200, "Category deleted");
        }
        catch (Exception e)
        {
            return new Response<Category?>(null, 500, e.Message);
        }
    }

    public async Task<Response<Category?>> GetAsync(GetCategoryByIdRequest request)
    {
        try
        {
            var categoty = await context.Categories.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (categoty is null)
                return new Response<Category?>(null, 404, "Category not found");

            return new Response<Category?>(categoty, 200, "Category retrieved");
        }
        catch (Exception e)
        {
            return new Response<Category?>(null, 500, e.Message);
        }

    }

    public async Task<Response<List<Category>>> GetAllAsync(GetAllCategoriesRequest request)
    {
        try
        {
            var categories = await context.Categories.ToListAsync();
            return new Response<List<Category>>(categories);
        }
        catch (Exception e)
        {
            return new Response<List<Category>>(null, 500, e.Message);
        }
        
    }
}