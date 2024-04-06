namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class QuestionDTO
{
    public Guid QuestionId { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public UserDTO AskingUser { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
