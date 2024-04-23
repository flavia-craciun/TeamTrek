namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ProjectMembershipDTO
{
	public Guid Id { get; set; }
	public Guid ProjectId { get; set; }
	public Guid UserId { get; set; }
}