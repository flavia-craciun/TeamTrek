using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class ProjectTaskDTO
{
    public Guid TaskId { get; set; }
    public string TaskName { get; set; } = default!;
    public string? Description { get; set; }
    public TaskStatusEnum Status { get; set; } = default!;
    public UserDTO AssignedToUser{ get; set; } = default!;
    public ProjectDTO Project{ get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
