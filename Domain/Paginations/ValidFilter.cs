namespace Domain.Paginations;

public class ValidFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }


    public ValidFilter()
    {

    }

    public ValidFilter(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize < 1 ? 1 : pageSize;
    }
}
