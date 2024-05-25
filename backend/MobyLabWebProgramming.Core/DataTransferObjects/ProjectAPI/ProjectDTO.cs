using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;

namespace MobyLabWebProgramming.Core.DataTransferObjects.ProjectAPI;

public class ProjectDTO
{
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = default!;
    public string? Description { get; set; }
    public UserDTO CreatedByUser { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}
