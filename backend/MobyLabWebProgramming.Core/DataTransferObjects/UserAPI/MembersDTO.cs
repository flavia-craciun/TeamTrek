namespace MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;

public class MembersDTO
{
	public ICollection<string> Email { get; set; } = default!;
	public string TeamName { get; set; } = default!;
    
}