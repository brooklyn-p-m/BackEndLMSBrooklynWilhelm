using Microsoft.EntityFrameworkCore;

public class EnrollmentService : IDataService<Enrollments>
{
    private readonly AppDbContext _context;
    public EnrollmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Enrollments?> GetByIdAsync(int enrollmentsId)
    {
        return await _context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentsId == enrollmentsId);
    }

    public async Task<IEnumerable<Enrollments>?> GetAllAsync()
    {
        return await _context.Enrollments.Where(e => e.IsDeleted == false).ToListAsync();
    }

    public async Task<IEnumerable<Enrollments>?> GetByStudentAsync(int studentsId)
    {
        return await _context.Enrollments.Where(e => e.StudentsId == studentsId && e.IsDeleted == false).ToListAsync();
    }

    public async Task<IEnumerable<Enrollments>?> GetBySectionAsync(int sectionsId)
    {
        return await _context.Enrollments.Where(e => e.SectionsId == sectionsId && e.IsDeleted == false).ToListAsync();
    }

    public async Task<Enrollments> CreateAsync(Enrollments enrollment)
    {
        enrollment.EnrollDate = DateTime.UtcNow;
        _context.Add(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }

    public async Task<Enrollments> EnrollStudentAsync(int studentsId, int sectionsId)
    {
        var existing = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentsId == studentsId && e.SectionsId == sectionsId);

        if (existing is not null) return existing;

        var student = await _context.Students.FindAsync(studentsId);
        var section = await _context.Sections.FindAsync(sectionsId);

        var enrollment = new Enrollments
        {
            EnrollmentsId = 0,
            StudentsId = studentsId,
            Student = student!,
            SectionsId = sectionsId,
            Section = section!,
            EnrollDate = DateTime.UtcNow,
            Status = EnrollmentStatus.Active
        };

        _context.Add(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }

    public async Task<Enrollments?> UpdateAsync(Enrollments enrollment)
    {
        var enrollmentToUpdate = await _context.Enrollments.FindAsync(enrollment.EnrollmentsId);
        if (enrollmentToUpdate is null) return null;

        enrollmentToUpdate.Status = enrollment.Status;
        enrollmentToUpdate.FinishGrade = enrollment.FinishGrade;

        await _context.SaveChangesAsync();
        return enrollmentToUpdate;
    }

    public async Task<bool> UpdateStatusAsync(int enrollmentsId, EnrollmentStatus status)
    {
        var enrollmentToUpdate = await _context.Enrollments.FindAsync(enrollmentsId);
        if (enrollmentToUpdate is null) return false;

        enrollmentToUpdate.Status = status;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int enrollmentsId)
    {
        var enrollmentToDelete = await _context.Enrollments.Where(e => e.IsDeleted == false && e.EnrollmentsId == enrollmentsId).SingleOrDefaultAsync();
        if (enrollmentToDelete is null) return false;

        enrollmentToDelete.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
