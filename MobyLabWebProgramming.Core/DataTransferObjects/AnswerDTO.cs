namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class AnswerDTO
{
    public Guid AnswerId { get; set; }
    public string? Description { get; set; }
    public QuestionDTO Question { get; set; } = default!;
    public UserDTO RespondingUser { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
