namespace MobyLabWebProgramming.Core.DataTransferObjects.AnswerAPI;

public class AnswerAddDTO
{
	public Guid QuestionId { get; set; }
	public string? Description { get; set; }
}
