using Domain.Paginations;

namespace Domain.Filters;

public class CourseFilter : ValidFilter
{
    public string? Title { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }
}
