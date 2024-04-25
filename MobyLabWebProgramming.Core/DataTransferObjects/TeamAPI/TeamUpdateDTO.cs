namespace MobyLabWebProgramming.Core.DataTransferObjects.TeamAPI;
public class TeamUpdateDTO
{
	public Guid TeamId { get; set; }
	public string TeamName { get; set; } = default!;
	
	public Guid TeamLeaderId { get; set; } = default!; // FIXME: remove after you implement authorization
}