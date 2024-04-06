using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class ProjectTask : BaseEntity
{
    public string TaskName { get; set; } = default!;
    public string? Description { get; set; }
    public TaskStatusEnum Status { get; set; } = default!;
    public Guid ProjectId { get; set; } = default!;
    public Guid AssignedToUserId { get; set; } = default!;
    
    // Navigation property
    public Project Project { get; set; } = default!;
    public User AssignedToUser { get; set; } = default!;
}