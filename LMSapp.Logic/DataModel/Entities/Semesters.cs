public class Semesters
{
    public required int SemestersId { get; set; }
    public required string Term { get; set; }
    public required int Year { get; set; }

    public ICollection<Sections>? sections { get; set; }
}
