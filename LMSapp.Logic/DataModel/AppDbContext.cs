using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Courses> Courses { get; set; }
    public DbSet<AppUser> Users { get; set; }
    public DbSet<Enrollments> Enrollments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Students> Students { get; set; }
    public DbSet<Assignments> Assignments { get; set; }
    public DbSet<Submissions> Submissions { get; set; }
    public DbSet<Todos> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Instructor>()
            .HasOne(i => i.User)
            .WithMany()
            .HasForeignKey(i => i.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Instructor>()
            .HasIndex(i => i.AppUserId)
            .IsUnique();

        modelBuilder.Entity<Students>()
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Students>()
            .HasIndex(s => s.AppUserId)
            .IsUnique();

        modelBuilder.Entity<Courses>()
            .HasOne(c => c.Instructor)
            .WithMany(i => i.courses)
            .HasForeignKey(c => c.InstructorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollments>()
            .HasOne(e => e.Student)
            .WithMany(s => s.courses)
            .HasForeignKey(e => e.StudentsId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollments>()
            .HasOne(e => e.Course)
            .WithMany(c => c.students)
            .HasForeignKey(e => e.CoursesId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Enrollments>()
            .HasIndex(e => new { e.StudentsId, e.CoursesId })
            .IsUnique();

        modelBuilder.Entity<Assignments>()
            .HasOne(a => a.courses)
            .WithMany(c => c.assignments)
            .HasForeignKey(a => a.CoursesId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Submissions>()
            .HasOne(s => s.Assignment)
            .WithMany(a => a.submissions)
            .HasForeignKey(s => s.AssignmentsId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Submissions>()
            .HasOne(s => s.Student)
            .WithMany(st => st.submissions)
            .HasForeignKey(s => s.StudentsId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Submissions>()
            .HasIndex(s => new { s.AssignmentsId, s.StudentsId })
            .IsUnique();

        modelBuilder.Entity<Todos>()
            .HasOne(t => t.User)
            .WithMany(u => u.todos)
            .HasForeignKey(t => t.AppUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}