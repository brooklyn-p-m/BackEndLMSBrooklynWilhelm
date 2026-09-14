public class Todos
{
    public int TodosId { get; set; }
    public int AppUserId { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; set; }

    public AppUser? User { get; set; }
}