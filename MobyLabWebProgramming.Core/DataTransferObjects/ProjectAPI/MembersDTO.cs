namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class MembersDTO
{
	public Guid ProjectId { get; set; }
	public ICollection<Guid> UserIds { get; set; }
}