using Microsoft.EntityFrameworkCore;

public class AssignmentService : IDataService<Assignments>
{
    private readonly AppDbContext _context;
    public AssignmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Assignments?> GetByIdAsync(int assignmentsId)
    {
        return await _context.Assignments.FirstOrDefaultAsync(a => a.AssignmentsId == assignmentsId);
    }

    public async Task<IEnumerable<Assignments>?> GetAllAsync()
    {
        return await _context.Assignments.Where(a => a.IsDeleted == false).ToListAsync();
    }

    public async Task<IEnumerable<Assignments>?> GetBySectionAsync(int sectionsId)
    {
        return await _context.Assignments.Where(a => a.SectionsId == sectionsId && a.IsDeleted == false).ToListAsync();
    }

    public async Task<Assignments> CreateAsync(Assignments assignment)
    {
        _context.Add(assignment);
        await _context.SaveChangesAsync();
        return assignment;
    }

    public async Task<Assignments?> UpdateAsync(Assignments assignment)
    {
        var assignmentToUpdate = await _context.Assignments.FindAsync(assignment.AssignmentsId);
        if (assignmentToUpdate is null) return null;

        assignmentToUpdate.AssignName = assignment.AssignName;
        assignmentToUpdate.DueDate = assignment.DueDate;
        assignmentToUpdate.LockDate = assignment.LockDate;
        assignmentToUpdate.AvailableDate = assignment.AvailableDate;
        assignmentToUpdate.Url = assignment.Url;
        assignmentToUpdate.MaxPoints = assignment.MaxPoints;

        await _context.SaveChangesAsync();
        return assignmentToUpdate;
    }

    public async Task<bool> DeleteAsync(int assignmentsId)
    {
        var assignmentToDelete = await _context.Assignments.Where(a => a.IsDeleted == false && a.AssignmentsId == assignmentsId).SingleOrDefaultAsync();
        if (assignmentToDelete is null) return false;

        assignmentToDelete.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
