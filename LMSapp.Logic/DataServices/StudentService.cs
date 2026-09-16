using Microsoft.EntityFrameworkCore;

public class StudentService : IDataService<Students>
{
    private readonly AppDbContext _context;
    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Students?> GetByIdAsync(int studentId)
    {
        return await _context.Students.FirstOrDefaultAsync(s => s.StudentsId == studentId);
    }

    public async Task<IEnumerable<Students>?> GetAllAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Students?> GetByUserIdAsync(int appUserId)
    {
        return await _context.Students.FirstOrDefaultAsync(s => s.AppUserId == appUserId);
    }

    public async Task<Students> CreateAsync(Students student)
    {
        student.EnrollmentDate = DateTime.Now;
        _context.Add(student);
        await _context.SaveChangesAsync();
        return student;
    }

    public async Task<Students?> UpdateAsync(Students student)
    {
        var studentToUpdate = await _context.Students.FindAsync(student.StudentsId);
        if (studentToUpdate is null) return null;

        //no other revelant info to update

        await _context.SaveChangesAsync();
        return studentToUpdate;
    }

    public async Task<bool> DeleteAsync(int studentId)
    {
        var studentToDelete = await _context.Students.Where(s => s.IsDeleted == false && s.StudentsId == studentId).SingleOrDefaultAsync();
        if (studentToDelete is null) return false;

        studentToDelete.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}