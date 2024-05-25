using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects.TaskAPI;

public class ProjectTaskUpdateDTO
{
	public Guid TaskId { get; set; }
	public string? TaskName { get; set; } = default!;
	public string? Description { get; set; }
	public TaskStatusEnum? Status { get; set; } = default!;
}