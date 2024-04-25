using MobyLabWebProgramming.Core.DataTransferObjects.QuestionAPI;
using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;

namespace MobyLabWebProgramming.Core.DataTransferObjects.AnswerAPI;

public class AnswerDTO
{
    public Guid AnswerId { get; set; }
    public string? Description { get; set; }
    public QuestionDTO Question { get; set; } = default!;
    public UserDTO RespondingUser { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
