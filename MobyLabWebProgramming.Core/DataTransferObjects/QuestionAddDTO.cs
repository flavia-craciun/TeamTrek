namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class QuestionAddDTO
{
	public string Title { get; set; } = default!;
	public string? Description { get; set; }
}