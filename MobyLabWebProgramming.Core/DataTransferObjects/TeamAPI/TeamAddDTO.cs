namespace MobyLabWebProgramming.Core.DataTransferObjects.TeamAPI;

public class TeamAddDTO
{
	public Guid TeamLeaderId { get; set; } = default!;
	public string TeamName { get; set; } = default!;
}