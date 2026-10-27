namespace Dima.Core.Requests.Categories;

public class UpdateCategoryRequest : Request
{
    public string Title { get; set; }
    public string Description { get; set; }
}