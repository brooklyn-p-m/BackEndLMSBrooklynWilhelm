public class Enrollments
{
    public required int EnrollmentsId { get; set; }

    public required int StudentsId { get; set; }
    public required Students Student { get; set; }

    public required int SectionsId { get; set; }
    public required Sections Section { get; set; }

    public DateTime EnrollDate { get; set; }
    public required EnrollmentStatus Status { get; set; }
    public string? FinishGrade { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Submissions>? submissions { get; set; }
}
