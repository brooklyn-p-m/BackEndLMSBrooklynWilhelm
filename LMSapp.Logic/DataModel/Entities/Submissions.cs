public class Submissions
{
    public int SubmissionsId { get; set; }
    public int AssignmentsId { get; set; }
    public int StudentsId { get; set; }
    public DateTime? SubmittedAt { get; set; }
    public string? Submission { get; set; }
    public int? Grade { get; set; }
    public string? Feedback { get; set; }

    public required Assignments Assignment { get; set; }
    public required Students? Student { get; set; }


}