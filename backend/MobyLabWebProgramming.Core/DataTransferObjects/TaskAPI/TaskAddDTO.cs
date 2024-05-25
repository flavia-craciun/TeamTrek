using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects.TaskAPI;

public class TaskAddDTO : BaseEntity
{
    public string TaskName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public TaskStatusEnum Status { get; set; } = default!;
    public Guid ProjectId { get; set; } = default!;
    public Guid AssignedToUserId { get; set; } = default!;
}