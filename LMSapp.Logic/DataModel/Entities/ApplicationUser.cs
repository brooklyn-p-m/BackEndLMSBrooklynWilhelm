using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser<int>
{
    public int? AppUserId { get; set; }
}
