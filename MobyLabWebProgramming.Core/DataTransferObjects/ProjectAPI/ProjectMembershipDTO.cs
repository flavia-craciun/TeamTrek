namespace MobyLabWebProgramming.Core.DataTransferObjects.ProjectAPI;

public class ProjectMembershipDTO
{
	public Guid Id { get; set; }
	public Guid ProjectId { get; set; }
	public Guid UserId { get; set; }
}