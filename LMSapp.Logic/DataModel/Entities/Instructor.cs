public class Instructor
{
    public required int InstructorId { get; set; }

    public required int AppUserId { get; set; }
    public required AppUser User { get; set; }

    public string? Department { get; set; }
    public string? Office { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Sections>? sections { get; set; }
}
