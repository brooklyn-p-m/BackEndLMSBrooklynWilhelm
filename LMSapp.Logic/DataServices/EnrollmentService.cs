using Microsoft.EntityFrameworkCore;

public class EnrollmentService : IDataService<Enrollments>
{
    private readonly AppDbContext _context;
    public EnrollmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Enrollments?> GetByIdAsync(int enrollmentId)
    {
        return await _context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentsId == enrollmentId);
    }

    public async Task<IEnumerable<Enrollments>?> GetAllAsync()
    {
        return await _context.Enrollments.ToListAsync();
    }

    public async Task<IEnumerable<Enrollments>?> GetByStudentAsync(int studentId)
    {
        return await _context.Enrollments.Where(e => e.StudentsId == studentId).ToListAsync();
    }

    public async Task<IEnumerable<Enrollments>?> GetByCourseAsync(int courseId)
    {
        return await _context.Enrollments.Where(e => e.CoursesId == courseId).ToListAsync();
    }

    public async Task<Enrollments> CreateAsync(Enrollments enrollment)
    {
        enrollment.EnrollmentDate = DateTime.Now;
        _context.Add(enrollment);
        await _context.SaveChangesAsync();
        return enrollment;
    }

    public async Task<Enrollments> EnrollStudentAsync(int studentId, int courseId)
    {
        var existing = await _context.Enrollments
            .FirstOrDefaultAsync(e => e.StudentsId == studentId && e.CoursesId == courseId);

        if (existing is not null) return existing;

        var student = await _context.Students.FindAsync(studentId);
        var course = await _context.Courses.FindAsync(courseId);


        var enrollment = new Enrollments
        {
            StudentsId = studentId,
            Student = student,
            CoursesId = courseId,
            Course = course,
            EnrollmentDate = DateTime.Now,
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

        await _context.SaveChangesAsync();
        return enrollmentToUpdate;
    }

    public async Task<bool> UpdateStatusAsync(int enrollmentId, EnrollmentStatus status)
    {
        var enrollmentToUpdate = await _context.Enrollments.FindAsync(enrollmentId);
        if (enrollmentToUpdate is null) return false;

        enrollmentToUpdate.Status = status;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int enrollmentId)
    {
        var enrollmentToDelete = await _context.Enrollments.Where(e => e.IsDeleted == false && e.EnrollmentsId == enrollmentId).SingleOrDefaultAsync();
        if (enrollmentToDelete is null) return false;

        enrollmentToDelete.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}