namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class TeamDTO
{
    public Guid TeamId { get; set; }
    public string TeamName { get; set; } = default!;
    public UserDTO TeamLeader { get; set; } = default!;
}
