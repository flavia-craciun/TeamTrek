namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TeamMembershipDTO
{
	public Guid Id { get; set; }
	public Guid TeamId { get; set; }
	public Guid UserId { get; set; }
}