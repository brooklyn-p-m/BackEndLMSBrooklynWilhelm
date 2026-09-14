using System.ComponentModel.DataAnnotations;

public class AppUser
{
    public int AppUserId { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public required Guid PasswordHash { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Todos>? todos { get; set; }
}