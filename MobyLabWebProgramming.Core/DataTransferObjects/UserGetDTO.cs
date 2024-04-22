using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class UserGetDTO
{
	public string Name { get; set; } = default!;
	public UserRoleEnum Role { get; set; } = default!;
}