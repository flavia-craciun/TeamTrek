using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TaskGetDTO
{
	public string TaskName { get; set; } = default!;
	public string? Description { get; set; }
	public TaskStatusEnum Status { get; set; } = default!;
}