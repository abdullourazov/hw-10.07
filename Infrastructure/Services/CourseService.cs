using System.Net;
using AutoMapper;
using Domain.ApiResponse;
using Domain.DTOs.Course;
using Domain.Filters;
using Domain.Paginations;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourseService(DataContext context,
    ICourseRepository courseRepository,
    IMapper mapper) : ICourseService
{
    public async Task<PagedResponse<List<GetCourseDto>>> GetCoursesAsync(CourseFilter filter)
    {
        var validFilter = new ValidFilter(filter.PageNumber, filter.PageSize);
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

        var totalRecords = await query.CountAsync();

        var paged = await query
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();

        var mapped = mapper.Map<List<GetCourseDto>>(paged);

        return new PagedResponse<List<GetCourseDto>>(mapped, totalRecords, validFilter.PageNumber,
            validFilter.PageSize);
    }

    public async Task<Response<List<GetCourseDto>>> GetCoursesAsync()
    {
        var courses = await context.Courses
            .AsNoTracking()
            .Select(c => new GetCourseDto
            {
                Id = c.Id,
                Title = c.Title,
                Price = c.Price,
                Description = c.Description,
            }).ToListAsync();

        return new Response<List<GetCourseDto>>(courses);
    }

    public async Task<Response<GetCourseDto?>> GetCourseAsync(int id)
    {
        var course = await context.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
        {
            return null;
        }

        var mapped = mapper.Map<GetCourseDto>(course);
        return new Response<GetCourseDto?>(mapped);
    }

    public async Task<List<GetCourseWithGroupCountDto>> GetCourseWithGroupCountAsync()
    {
        var courses = await context.Courses
            .Include(course => course.Groups)
            .Select(course => new GetCourseWithGroupCountDto()
            {
                CourseId = course.Id,
                Title = course.Title,
                GroupCount = course.Groups.Count,
            }).ToListAsync();

        return courses;
    }



    public async Task<Response<string>> CreateCourseAsync(CreateCourseDto courseDto)
    {
        //var course = mapper.Map<Course>(courseDto);
        var course = CourseMappers.ToEntity(courseDto);
        var result = await courseRepository.CreateAsync(course);

        return result == 0
            ? Response<string>.Error(HttpStatusCode.InternalServerError, "Something went wrong")
            : Response<string>.Success(message: "Course created successfully");
    }

    public async Task<Response<string>> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto)
    {
        var course = await context.Courses.FindAsync(id);
        if (course == null)
        {
            return Response<string>.Error(HttpStatusCode.NotFound, "Course not found");
        }

        course.ToEntity(updateCourseDto);
        var result = await courseRepository.UpdateAsync(course);

        return result == 0
            ? Response<string>.Error(HttpStatusCode.InternalServerError, "Something went wrong")
            : Response<string>.Success(null, "Course updated successfully");
    }

    public async Task<Response<string>> DeleteCourseAsync(int id)
    {
        var course = await context.Courses.FindAsync(id);
        if (course == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Course not found");
        }

        context.Courses.Remove(course);
        var result = await context.SaveChangesAsync();

        return result == 0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong")
            : new Response<string>(null, "Course updated successfully");
    }

   

}
