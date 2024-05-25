namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ProjectMembersDTO
{
	public Guid ProjectId { get; set; }
	public ICollection<Guid> UserIds { get; set; }
}