public class Assignments
{
    public required int AssignmentsId { get; set; }

    public required int SectionsId { get; set; }
    public required Sections Section { get; set; }

    public required string AssignName { get; set; }
    public required DateTime DueDate { get; set; }
    public DateTime? LockDate { get; set; }
    public DateTime? AvailableDate { get; set; }
    public string? Url { get; set; }
    public required decimal MaxPoints { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Submissions>? submissions { get; set; }
}
