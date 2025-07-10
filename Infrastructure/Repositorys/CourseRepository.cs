using Domain.ApiResponse;
using Domain.Entities;
using Domain.Filters;
using Domain.Paginations;
using Infrastructure.Data;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories;

public class CourseRepository(DataContext context) : ICourseRepository
{
    public async Task<PagedResponse<List<Course>>> GetAllAsync(CourseFilter filter)
    {
        var query = context.Courses.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Title))
        {
            query = query.Where(c => c.Title.ToLower().Trim().Contains(filter.Title.ToLower().Trim()));
        }

        if (filter.MinPrice != null)
        {
            query = query.Where(c => c.Price >= filter.MinPrice);
        }

        if (filter.MaxPrice != null)
        {
            query = query.Where(c => c.Price <= filter.MaxPrice);
        }


        var pagination = new Pagination<Course>(query);
        return await pagination.GetPagedResponseAsync(filter.PageNumber, filter.PageSize);
    }

    public async Task<Course?> GetAsync(int id)
    {
        var course = await context.Courses.FindAsync(id);
        return course;
    }

    public async Task<int> CreateAsync(Course course)
    {
        await context.Courses.AddAsync(course);
        return await context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Course course)
    {
        context.Courses.Update(course);
        return await context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Course course)
    {
        context.Courses.Remove(course);
        return await context.SaveChangesAsync();
    }
}
