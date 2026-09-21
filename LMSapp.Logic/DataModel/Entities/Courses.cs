public class Courses
{
    public required int CoursesId { get; set; }
    public required string CourseCode { get; set; }
    public required string CourseTitle { get; set; }
    public required int Credits { get; set; }
    public DateTime? SyllabusExp { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Sections>? sections { get; set; }
}
