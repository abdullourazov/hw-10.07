using Domain.ApiResponse;
using Domain.Entities;
using Domain.Filters;

namespace Infrastructure.Interfaces;

public interface ICourseRepository
{
    Task<PagedResponse<List<Course>>> GetAllAsync(CourseFilter filter);
    Task<Course?> GetAsync(int id);
    Task<int> CreateAsync(Course course);
    Task<int> UpdateAsync(Course course);
    Task<int> DeleteAsync(Course course);
}
