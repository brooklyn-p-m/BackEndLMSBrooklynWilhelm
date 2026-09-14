public class Students
{
    public int StudentsId { get; set; }
    public int AppUserId { get; set; }
    public required AppUser User { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public bool IsDeleted { get; set; }

    public required ICollection<Enrollments> courses { get; set; }
    public ICollection<Submissions>? submissions { get; set; }
}