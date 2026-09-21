using Microsoft.EntityFrameworkCore;

public class CourseService : IDataService<Courses>
{
    private readonly AppDbContext _context;
    public CourseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Courses?> GetByIdAsync(int coursesId)
    {
        return await _context.Courses.FirstOrDefaultAsync(c => c.CoursesId == coursesId);
    }

    public async Task<IEnumerable<Courses>?> GetAllAsync()
    {
        return await _context.Courses.Where(c => c.IsDeleted == false).ToListAsync();
    }

    public async Task<Courses> CreateAsync(Courses course)
    {
        _context.Add(course);
        await _context.SaveChangesAsync();
        return course;
    }

    public async Task<Courses?> UpdateAsync(Courses course)
    {
        var courseToUpdate = await _context.Courses.FindAsync(course.CoursesId);
        if (courseToUpdate is null) return null;

        courseToUpdate.CourseCode = course.CourseCode;
        courseToUpdate.CourseTitle = course.CourseTitle;
        courseToUpdate.Credits = course.Credits;
        courseToUpdate.SyllabusExp = course.SyllabusExp;

        await _context.SaveChangesAsync();
        return courseToUpdate;
    }

    public async Task<bool> DeleteAsync(int coursesId)
    {
        var courseToDelete = await _context.Courses.Where(c => c.IsDeleted == false && c.CoursesId == coursesId).SingleOrDefaultAsync();
        if (courseToDelete is null) return false;

        courseToDelete.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
