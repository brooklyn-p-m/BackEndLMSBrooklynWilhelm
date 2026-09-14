public class Enrollments
{
    public int EnrollmentsId { get; set; }
    public int StudentsId { get; set; }
    public int CoursesId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public required EnrollmentStatus Status { get; set; }
    public bool IsDeleted { get; set; }

    public required Students Student { get; set; }
    public required Courses Course { get; set; }
}

public enum EnrollmentStatus
{
    Active = 1, Dropped = 0, Completed = 2
}