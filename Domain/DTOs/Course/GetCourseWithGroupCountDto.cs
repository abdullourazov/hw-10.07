namespace Domain.DTOs.Course;

public class GetCourseWithGroupCountDto
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public int GroupCount { get; set; }
}
