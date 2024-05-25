namespace MobyLabWebProgramming.Core.Entities;

public class Question : BaseEntity
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    
    // Navigation property
    public User User { get; set; } = default!;
    public ICollection<Answer> Answers  { get; set; } = default!;
}