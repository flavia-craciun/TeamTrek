using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;

namespace MobyLabWebProgramming.Core.DataTransferObjects.AnswerAPI;

public class FeedbackGetDTO
{ public string? Description { get; set; }
	public UserGetDTO RespondingUser { get; set; } = default!;
	public DateTime UpdatedAt { get; set; }
}