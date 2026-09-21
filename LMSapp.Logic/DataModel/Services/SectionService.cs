using Microsoft.EntityFrameworkCore;

public class SectionService : IDataService<Sections>
{
    private readonly AppDbContext _context;
    public SectionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Sections?> GetByIdAsync(int sectionsId)
    {
        return await _context.Sections.FirstOrDefaultAsync(s => s.SectionsId == sectionsId);
    }

    public async Task<IEnumerable<Sections>?> GetAllAsync()
    {
        return await _context.Sections.Where(s => s.IsDeleted == false).ToListAsync();
    }

    public async Task<IEnumerable<Sections>?> GetByCourseAsync(int coursesId)
    {
        return await _context.Sections.Where(s => s.CoursesId == coursesId && s.IsDeleted == false).ToListAsync();
    }

    public async Task<IEnumerable<Sections>?> GetByInstructorAsync(int instructorId)
    {
        return await _context.Sections.Where(s => s.InstructorId == instructorId && s.IsDeleted == false).ToListAsync();
    }

    public async Task<IEnumerable<Sections>?> GetBySemesterAsync(int semestersId)
    {
        return await _context.Sections.Where(s => s.SemestersId == semestersId && s.IsDeleted == false).ToListAsync();
    }

    public async Task<Sections> CreateAsync(Sections section)
    {
        _context.Add(section);
        await _context.SaveChangesAsync();
        return section;
    }

    public async Task<Sections?> UpdateAsync(Sections section)
    {
        var sectionToUpdate = await _context.Sections.FindAsync(section.SectionsId);
        if (sectionToUpdate is null) return null;

        sectionToUpdate.SectionNumber = section.SectionNumber;
        sectionToUpdate.InstructorId = section.InstructorId;
        sectionToUpdate.ClassTime = section.ClassTime;
        sectionToUpdate.Room = section.Room;

        await _context.SaveChangesAsync();
        return sectionToUpdate;
    }

    public async Task<bool> DeleteAsync(int sectionsId)
    {
        var sectionToDelete = await _context.Sections.Where(s => s.IsDeleted == false && s.SectionsId == sectionsId).SingleOrDefaultAsync();
        if (sectionToDelete is null) return false;

        sectionToDelete.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
