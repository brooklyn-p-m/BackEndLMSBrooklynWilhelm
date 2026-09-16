using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

public class CourseService : IDataService<Courses>
{
    private readonly AppDbContext _context;
    public CourseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Courses?> GetByIdAsync (int courseId)
    {
        return await _context.Courses.FirstOrDefaultAsync(c => c.CoursesId == courseId);
    }

    public async Task<IEnumerable<Courses>?> GetAllAsync ()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<IEnumerable<Courses>?> GetCoursesByInstructorAsync(int InstructorId)
    {
        return await _context.Courses.Where(c => c.InstructorId == InstructorId).ToListAsync();
    } 

    public async Task<Courses> CreateAsync (Courses course)
    {
        course.CreatedAt = DateTime.Now;
        _context.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task<Courses?> UpdateAsync (Courses course)
    {
        var courseToUpdate = await _context.Courses.FindAsync(course.CoursesId);
        if(courseToUpdate is null) return null;

        courseToUpdate.Title = course.Title;
        courseToUpdate.Description = course.Description;
        courseToUpdate.Syllabus = course.Syllabus;
        courseToUpdate.CourseCode = course.CourseCode;

        await _context.SaveChangesAsync();
        return courseToUpdate;
    }

    public async Task<bool> DeleteAsync(int courseId)
    {
        var courseToDelete = await _context.Courses.Where(c => c.IsDeleted == false && c.CoursesId == courseId).SingleAsync();
        if(courseToDelete is null) return false;

        courseToDelete.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

}