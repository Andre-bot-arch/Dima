namespace Dima.Core.Requests.Categories;

public class CreateCategoryRequest : Request
{
    public string Title { get; set; }
    public string Description { get; set; }
}