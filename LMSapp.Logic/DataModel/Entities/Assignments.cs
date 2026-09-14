public class Assignments
{
    public int AssignmentsId { get; set; }
    public int CoursesId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int MaxPoints { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public required Courses courses { get; set; }
    public ICollection<Submissions>? submissions { get; set; }

}