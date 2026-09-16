using Microsoft.EntityFrameworkCore;

public class AssignmentService : IDataService<Assignments>
{
    private readonly AppDbContext _context;
    public AssignmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Assignments?> GetByIdAsync(int assignmentId)
    {
        return await _context.Assignments.FirstOrDefaultAsync(a => a.AssignmentsId == assignmentId);
    }

    public async Task<IEnumerable<Assignments>?> GetAllAsync()
    {
        return await _context.Assignments.ToListAsync();
    }

    public async Task<IEnumerable<Assignments>?> GetByCourseAsync(int courseId)
    {
        return await _context.Assignments.Where(a => a.CoursesId == courseId).ToListAsync();
    }

    public async Task<Assignments> CreateAsync(Assignments assignment)
    {
        assignment.CreatedAt = DateTime.Now;
        _context.Add(assignment);
        await _context.SaveChangesAsync();
        return assignment;
    }

    public async Task<Assignments?> UpdateAsync(Assignments assignment)
    {
        var assignmentToUpdate = await _context.Assignments.FindAsync(assignment.AssignmentsId);
        if (assignmentToUpdate is null) return null;

        assignmentToUpdate.Title = assignment.Title;
        assignmentToUpdate.Description = assignment.Description;
        assignmentToUpdate.DueDate = assignment.DueDate;
        assignmentToUpdate.MaxPoints = assignment.MaxPoints;

        await _context.SaveChangesAsync();
        return assignmentToUpdate;
    }

    public async Task<bool> DeleteAsync(int assignmentId)
    {
        var assignmentToDelete = await _context.Assignments.Where(a => a.IsDeleted == false && a.AssignmentsId == assignmentId).SingleOrDefaultAsync();
        if (assignmentToDelete is null) return false;

        assignmentToDelete.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}