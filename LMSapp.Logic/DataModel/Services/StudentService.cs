using Microsoft.EntityFrameworkCore;

public class StudentService : IDataService<Students>
{
    private readonly AppDbContext _context;
    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Students?> GetByIdAsync(int studentsId)
    {
        return await _context.Students.FirstOrDefaultAsync(s => s.StudentsId == studentsId);
    }

    public async Task<IEnumerable<Students>?> GetAllAsync()
    {
        return await _context.Students.Where(s => s.IsDeleted == false).ToListAsync();
    }

    public async Task<Students?> GetByAppUserIdAsync(int appUserId)
    {
        return await _context.Students.FirstOrDefaultAsync(s => s.AppUserId == appUserId);
    }

    public async Task<Students> CreateAsync(Students student)
    {
        _context.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Students?> UpdateAsync(Students student)
    {
        var studentToUpdate = await _context.Students.FindAsync(student.StudentsId);
        if (studentToUpdate is null) return null;

        studentToUpdate.Major = student.Major;
        studentToUpdate.StudentNumber = student.StudentNumber;

        await _context.SaveChangesAsync();
        return studentToUpdate;
    }

    public async Task<bool> DeleteAsync(int studentsId)
    {
        var studentToDelete = await _context.Students.Where(s => s.IsDeleted == false && s.StudentsId == studentsId).SingleOrDefaultAsync();
        if (studentToDelete is null) return false;

        studentToDelete.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
