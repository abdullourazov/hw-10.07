using Domain.DTOs.Course;
using Domain.Entities;

namespace Infrastructure.Mapper;

public static class CourseMappers
{
    public static Course ToEntity(CreateCourseDto courseDto)
    {
        return new Course
        {
            Title = courseDto.Title,
            Description = courseDto.Description,
            Price = courseDto.Price,
        };
    }

    public static void ToEntity(this Course course, UpdateCourseDto courseDto)
    {
        course.Title = courseDto.Title;
        course.Description = courseDto.Description;
        course.Price = courseDto.Price;
    }
}
