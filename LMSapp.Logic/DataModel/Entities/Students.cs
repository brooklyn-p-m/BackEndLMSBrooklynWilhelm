public class Students
{
    public required int StudentsId { get; set; }

    public required int AppUserId { get; set; }
    public required AppUser User { get; set; }

    // maps to sql "student.student_id" - the school-issued student number,
    // kept separate from the internal StudentsId primary key
    public int? StudentNumber { get; set; }
    public string? Major { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Enrollments>? enrollments { get; set; }
}
