using Domain.ApiResponse;
using Domain.DTOs.Course;
using Domain.Filters;

namespace Infrastructure.Interfaces;

public interface ICourseService
{
    Task<PagedResponse<List<GetCourseDto>>> GetCoursesAsync(CourseFilter filter);
    Task<Response<List<GetCourseDto>>> GetCoursesAsync();
    Task<Response<GetCourseDto?>> GetCourseAsync(int id);
    Task<List<GetCourseWithGroupCountDto>> GetCourseWithGroupCountAsync();
    Task<Response<string>> CreateCourseAsync(CreateCourseDto createCourseDto);
    Task<Response<string>> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto);
    Task<Response<string>> DeleteCourseAsync(int id);
}
