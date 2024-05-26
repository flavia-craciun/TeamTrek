namespace MobyLabWebProgramming.Core.DataTransferObjects.FeedbackAPI;

public class FeedbackAddDTO
{
	public int Rating { get; set; } = default!;
	public string FrequentedSection { get; set; } = default!;
	public string? Suggestion { get; set; } = default!;
	public int ResponseWanted { get; set; } = default!;
}
