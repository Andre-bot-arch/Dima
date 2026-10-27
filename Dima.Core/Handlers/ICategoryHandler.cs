using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;

public interface ICategoryHandler
{
    Task<Response<Category>> CreatedAsync(CreateCategoryRequest category);
    Task<Response<Category>> UpdateAsync(UpdateCategoryRequest category);
    Task<Response<Category>> DeleteAsync (DeleteCategoryRequest category);
    Task<Response<Category>> GetAsync(GetCategoryByIdRequest request);
    Task<Response<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
}