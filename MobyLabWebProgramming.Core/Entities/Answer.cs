namespace MobyLabWebProgramming.Core.Entities;

public class Answer : BaseEntity
{
    public Guid QuestionId { get; set; }
    public Guid UserId { get; set; }
    public string? Description { get; set; }
    
    // Navigation property
    public User User { get; set; } = default!;
    public Question Question { get; set; } = default!;
}