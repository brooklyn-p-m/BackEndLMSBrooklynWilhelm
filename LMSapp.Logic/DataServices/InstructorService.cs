using Microsoft.EntityFrameworkCore;

public class InstructorService : IDataService<Instructor>
{
    private readonly AppDbContext _context;
    public InstructorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Instructor?> GetByIdAsync(int instructorId)
    {
        return await _context.Instructors.FirstOrDefaultAsync(i => i.InstructorId == instructorId);
    }

    public async Task<IEnumerable<Instructor>?> GetAllAsync()
    {
        return await _context.Instructors.ToListAsync();
    }

    public async Task<Instructor?> GetByUserIdAsync(int appUserId)
    {
        return await _context.Instructors.FirstOrDefaultAsync(i => i.AppUserId == appUserId);
    }

    public async Task<Instructor> CreateAsync(Instructor instructor)
    {
        instructor.EnrollmentDate = DateTime.Now;
        _context.Add(instructor);
        await _context.SaveChangesAsync();
        return instructor;
    }

    public async Task<Instructor?> UpdateAsync(Instructor instructor)
    {
        var instructorToUpdate = await _context.Instructors.FindAsync(instructor.InstructorId);
        if (instructorToUpdate is null) return null;

        //no other revelant info to update

        await _context.SaveChangesAsync();
        return instructorToUpdate;
    }

    public async Task<bool> DeleteAsync(int instructorId)
    {
        var instructorToDelete = await _context.Instructors.Where(i => i.IsDeleted == false && i.InstructorId == instructorId).SingleOrDefaultAsync();
        if (instructorToDelete is null) return false;

        instructorToDelete.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}