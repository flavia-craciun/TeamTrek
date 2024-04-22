using System.Linq.Expressions;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class
	TeamMembershipProjectionSpec : BaseSpec<TeamMembershipProjectionSpec, TeamMembership, TeamMembershipDTO>
{
	protected override Expression<Func<TeamMembership, TeamMembershipDTO>> Spec => e => new()
	{
		Id = e.Id,
		TeamId = e.TeamId,
		UserId = e.UserId,
		// Map other properties as needed
	};

	public TeamMembershipProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
	{
	}

	public TeamMembershipProjectionSpec(Guid id) : base(id)
	{
	}

	// Add additional constructors or methods as needed for filtering
}