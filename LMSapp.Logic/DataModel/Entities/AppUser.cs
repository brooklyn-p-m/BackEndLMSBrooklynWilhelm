public class AppUser
{
    public required int AppUserId { get; set; }
    public required string Login { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public required string Password { get; set; }
    public bool IsDeleted { get; set; }

    // 1 -> 0/1: a user MAY have a student profile
    public Students? Student { get; set; }

    // 1 -> 0/1: a user MAY have an instructor profile
    public Instructor? Instructor { get; set; }

}
