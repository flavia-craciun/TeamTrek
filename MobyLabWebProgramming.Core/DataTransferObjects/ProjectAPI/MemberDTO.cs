using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class MemberDTO
{
	public string Name { get; set; } = default!;
	public string Email { get; set; } = default!;
	public UserRoleEnum Role { get; set; } = default!;

}