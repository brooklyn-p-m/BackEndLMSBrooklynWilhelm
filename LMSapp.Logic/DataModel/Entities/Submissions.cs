public class Submissions
{
    public required int SubmissionsId { get; set; }

    public required int AssignmentsId { get; set; }
    public required Assignments Assignment { get; set; }

    public required int EnrollmentsId { get; set; }
    public required Enrollments Enrollment { get; set; }

    public decimal? Grade { get; set; }
    public DateTime? GradingDate { get; set; }
    public DateTime? SubmissionDate { get; set; }
    public string? FileUrl { get; set; }
    public string? GradeFeedback { get; set; }
    public bool IsDeleted { get; set; }
}
