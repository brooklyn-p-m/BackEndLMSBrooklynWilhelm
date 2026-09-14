public class Courses
{
    public required int CoursesId { get; set; }
    public required string CourseCode { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public string? Syllabus { get; set; }
    public bool IsDeleted { get; set; }


    public required int InstructorId { get; set; }
    public required Instructor Instructor { get; set; }


    public required DateTime? CreatedAt { get; set; }

    public required ICollection<Enrollments> students { get; set; }
    public required ICollection<Assignments> assignments { get; set; }
}