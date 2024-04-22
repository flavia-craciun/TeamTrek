using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class TeamMembershipSpec : Specification<TeamMembership>
{
	public TeamMembershipSpec(Guid id)
	{
	}
	
	public TeamMembershipSpec(Guid teamId, Guid id) : this(id)
	{
		Query.Where(tm => tm.TeamId == teamId);
	}
}