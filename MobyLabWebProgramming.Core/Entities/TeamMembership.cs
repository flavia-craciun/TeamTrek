namespace MobyLabWebProgramming.Core.Entities;

public class TeamMembership : BaseEntity
{
    public Guid TeamId { get; set; }
    public Guid UserId { get; set; }
    
    // Navigation property
    public Team Team { get; set; } = default!;
    public User User { get; set; } = default!;

}