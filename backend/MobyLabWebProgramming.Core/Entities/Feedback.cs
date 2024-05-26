namespace MobyLabWebProgramming.Core.Entities;

public class Feedback : BaseEntity
{
	public int Rating { get; set; } = default!;
	public string FrequentedSection { get; set; } = default!;
	public string Suggestion { get; set; } = default!;
	public int ResponseWanted { get; set; } = default!;
	public Guid UserId { get; set; }

	// Navigation property
	public User User { get; set; } = default!;
}