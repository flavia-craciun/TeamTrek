namespace MobyLabWebProgramming.Core.Entities;

public class Team : BaseEntity
{
    public string TeamName { get; set; } = default!;
    public Guid TeamLeaderId { get; set; } = default!;

    // Navigation property
    public User TeamLeader { get; set; } = default!;
    public ICollection<User> Members  { get; set; } = default!;

    
}