using Domain.ApiResponse;
using Domain.DTOs.Course;
using Domain.Filters;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/course")]

public class CourseController(ICourseService courseService) : ControllerBase
{
    [HttpGet]
    public async Task<PagedResponse<List<GetCourseDto>>> GetCoursesAsync([FromQuery] CourseFilter filter)
    {
        return await courseService.GetCoursesAsync(filter);
    }

    [HttpGet("with-group-count")]
    public async Task<List<GetCourseWithGroupCountDto>> GetCourseWithGroupCountsAsync()
    {
        return await courseService.GetCourseWithGroupCountAsync();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourseAsync(CreateCourseDto createCourseDto)
    {
        var response = await courseService.CreateCourseAsync(createCourseDto);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto)
    {
        return await courseService.UpdateCourseAsync(id, updateCourseDto);
    }
}
