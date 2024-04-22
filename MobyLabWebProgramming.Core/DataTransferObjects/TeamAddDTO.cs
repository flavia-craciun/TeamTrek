namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TeamAddDTO
{
	public Guid TeamLeaderId { get; set; } = default!;
	public string TeamName { get; set; } = default!;
}