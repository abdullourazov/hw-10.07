namespace Domain.DTOs.Group;

public class CreateGroupDto
{
    public string Name { get; set; }
    public int RequiredStudents { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime EndedAt { get; set; }
    public int CourseId { get; set; }
}
