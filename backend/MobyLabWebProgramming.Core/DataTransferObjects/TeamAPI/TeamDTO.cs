using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;

namespace MobyLabWebProgramming.Core.DataTransferObjects.TeamAPI;

public class TeamDTO
{
    public Guid TeamId { get; set; }
    public string TeamName { get; set; } = default!;
    public UserDTO TeamLeader { get; set; } = default!;
}
