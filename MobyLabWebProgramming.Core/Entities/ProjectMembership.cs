namespace MobyLabWebProgramming.Core.Entities;

public class ProjectMembership : BaseEntity
{
    public Guid ProjectId { get; set; } = default!;
    public Guid UserId { get; set; } = default!;
    
    // Navigation property
    public User User { get; set; } = default!;
    public Project Project { get; set; } = default!;
}