namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class AnswerGetDTO
{ public string? Description { get; set; }
	public UserGetDTO RespondingUser { get; set; } = default!;
	public DateTime UpdatedAt { get; set; }
}