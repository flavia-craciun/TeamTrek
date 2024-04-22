namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class QuestionUpdateDTO
{
	public Guid QuestionId { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
}