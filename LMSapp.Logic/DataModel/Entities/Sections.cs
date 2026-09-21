public class Sections
{
    public required int SectionsId { get; set; }
    public required int SectionNumber { get; set; }

    public required int SemestersId { get; set; }
    public required Semesters Semester { get; set; }

    public required int CoursesId { get; set; }
    public required Courses Course { get; set; }

    public required int InstructorId { get; set; }
    public required Instructor Instructor { get; set; }

    public TimeSpan? ClassTime { get; set; }
    public string? Room { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Enrollments>? enrollments { get; set; }
    public ICollection<Assignments>? assignments { get; set; }
}
