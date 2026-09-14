public class Instructor
{
    public int InstructorId { get; set; }
    public int AppUserId { get; set; }
    public required AppUser User { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public bool IsDeleted { get; set; }
    public required ICollection<Courses> courses { get; set; }
}