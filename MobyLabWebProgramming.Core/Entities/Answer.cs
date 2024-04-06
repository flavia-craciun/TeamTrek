namespace MobyLabWebProgramming.Core.Entities;

public class Answer : BaseEntity
{
    public Guid QuestionId { get; set; } = default!;
    public Guid UserId { get; set; } = default!;
    public string? Description { get; set; }
    
    // Navigation property
    public User User { get; set; } = default!;
    public Question Question { get; set; } = default!;
}