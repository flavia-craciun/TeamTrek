namespace MobyLabWebProgramming.Core.Entities;

public class TeamMembership : BaseEntity
{
    public Guid TeamId { get; set; } = default!;
    public Guid UserId { get; set; } = default!;
    
    // Navigation property
    public Team Team { get; set; } = default!;
    public User User { get; set; } = default!;

}