using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;

public interface ICategoryHandler
{
    Task<Response<Category?>> CreatedAsync(CreateCategoryRequest request);
    Task<Response<Category?>> UpdateAsync(UpdateCategoryRequest request);
    Task<Response<Category?>> DeleteAsync (DeleteCategoryRequest request);
    Task<Response<Category?>> GetAsync(GetCategoryByIdRequest request);
    Task<Response<List<Category>>> GetAllAsync(GetAllCategoriesRequest request);
}