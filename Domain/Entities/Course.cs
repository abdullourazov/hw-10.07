namespace Domain.Entities;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }  
    
    public List<Group> Groups { get; set; }
}
