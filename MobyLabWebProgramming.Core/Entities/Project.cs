namespace MobyLabWebProgramming.Core.Entities;

public class Project : BaseEntity
{
    public string ProjectName { get; set; } = default!;
    public string? Description { get; set; }
    public Guid CreatedByUserId { get; set; } = default!;

    // Navigation property
    public ICollection<ProjectMembership> ProjectMemberships { get; set; } = default!;
    public ICollection<ProjectTask>? ProjectTasks { get; set; }
    public User CreatedByUser { get; set; } = default!;
}