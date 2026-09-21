using Microsoft.EntityFrameworkCore;

public class SemesterService : IDataService<Semesters>
{
    private readonly AppDbContext _context;
    public SemesterService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Semesters?> GetByIdAsync(int semestersId)
    {
        return await _context.Semesters.FirstOrDefaultAsync(s => s.SemestersId == semestersId);
    }

    public async Task<IEnumerable<Semesters>?> GetAllAsync()
    {
        return await _context.Semesters.ToListAsync();
    }

    public async Task<Semesters> CreateAsync(Semesters semester)
    {
        _context.Add(semester);
        await _context.SaveChangesAsync();
        return semester;
    }

    public async Task<Semesters?> UpdateAsync(Semesters semester)
    {
        var semesterToUpdate = await _context.Semesters.FindAsync(semester.SemestersId);
        if (semesterToUpdate is null) return null;

        semesterToUpdate.Term = semester.Term;
        semesterToUpdate.Year = semester.Year;

        await _context.SaveChangesAsync();
        return semesterToUpdate;
    }

    public async Task<bool> DeleteAsync(int semestersId)
    {
        var semesterToDelete = await _context.Semesters.FindAsync(semestersId);
        if (semesterToDelete is null) return false;

        _context.Remove(semesterToDelete);
        await _context.SaveChangesAsync();
        return true;
    }
}
