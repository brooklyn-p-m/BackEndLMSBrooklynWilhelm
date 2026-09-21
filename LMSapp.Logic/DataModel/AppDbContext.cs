using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Students> Students { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Courses> Courses { get; set; }
    public DbSet<Semesters> Semesters { get; set; }
    public DbSet<Sections> Sections { get; set; }
    public DbSet<Enrollments> Enrollments { get; set; }
    public DbSet<Assignments> Assignments { get; set; }
    public DbSet<Submissions> Submissions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User -> Student (1 -> 0/1)
        modelBuilder.Entity<Students>()
            .HasOne(s => s.User)
            .WithOne(u => u.Student)
            .HasForeignKey<Students>(s => s.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Students>()
            .HasIndex(s => s.AppUserId)
            .IsUnique();

        // User -> Instructor (1 -> 0/1)
        modelBuilder.Entity<Instructor>()
            .HasOne(i => i.User)
            .WithOne(u => u.Instructor)
            .HasForeignKey<Instructor>(i => i.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Instructor>()
            .HasIndex(i => i.AppUserId)
            .IsUnique();

        // Course -> Section (1 -> many)
        modelBuilder.Entity<Sections>()
            .HasOne(sec => sec.Course)
            .WithMany(c => c.sections)
            .HasForeignKey(sec => sec.CoursesId)
            .OnDelete(DeleteBehavior.Restrict);

        // Semester -> Section (1 -> many)
        modelBuilder.Entity<Sections>()
            .HasOne(sec => sec.Semester)
            .WithMany(sem => sem.sections)
            .HasForeignKey(sec => sec.SemestersId)
            .OnDelete(DeleteBehavior.Restrict);

        // Instructor -> Section (1 -> many)
        modelBuilder.Entity<Sections>()
            .HasOne(sec => sec.Instructor)
            .WithMany(i => i.sections)
            .HasForeignKey(sec => sec.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Student -> Enrollment (1 -> many)
        modelBuilder.Entity<Enrollments>()
            .HasOne(e => e.Student)
            .WithMany(s => s.enrollments)
            .HasForeignKey(e => e.StudentsId)
            .OnDelete(DeleteBehavior.Restrict);

        // Section -> Enrollment (1 -> many)
        modelBuilder.Entity<Enrollments>()
            .HasOne(e => e.Section)
            .WithMany(sec => sec.enrollments)
            .HasForeignKey(e => e.SectionsId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollments>()
            .HasIndex(e => new { e.StudentsId, e.SectionsId })
            .IsUnique();

        // Section -> Assignment (1 -> many)
        modelBuilder.Entity<Assignments>()
            .HasOne(a => a.Section)
            .WithMany(sec => sec.assignments)
            .HasForeignKey(a => a.SectionsId)
            .OnDelete(DeleteBehavior.Restrict);

        // Enrollment -> Submission (1 -> many)
        modelBuilder.Entity<Submissions>()
            .HasOne(s => s.Enrollment)
            .WithMany(e => e.submissions)
            .HasForeignKey(s => s.EnrollmentsId)
            .OnDelete(DeleteBehavior.Restrict);

        // Assignment -> Submission (1 -> many)
        modelBuilder.Entity<Submissions>()
            .HasOne(s => s.Assignment)
            .WithMany(a => a.submissions)
            .HasForeignKey(s => s.AssignmentsId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Submissions>()
            .HasIndex(s => new { s.AssignmentsId, s.EnrollmentsId })
            .IsUnique();

    }
}
