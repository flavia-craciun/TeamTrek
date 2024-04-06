using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class TeamMembershipSpec : Specification<TeamMembership>
{
	public TeamMembershipSpec(Guid teamId)
	{
		Query.Where(tm => tm.TeamId == teamId);
	}
}