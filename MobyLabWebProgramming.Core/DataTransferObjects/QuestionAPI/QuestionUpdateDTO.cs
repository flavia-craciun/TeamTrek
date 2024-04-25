namespace MobyLabWebProgramming.Core.DataTransferObjects.QuestionAPI;

public class QuestionUpdateDTO
{
	public Guid QuestionId { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
}