namespace MobyLabWebProgramming.Core.DataTransferObjects.ProjectAPI;

public class ProjectUpdateDTO
{
	public Guid ProjectId { get; set; }
	public string? ProjectName { get; set; }
	public string? Description { get; set; }
}