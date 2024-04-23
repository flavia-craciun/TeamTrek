using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ProjectMembershipSpec : Specification<ProjectMembership>
{
	public ProjectMembershipSpec(Guid? userId = null, Guid? projectId = null, Guid? id = null)
	{
		if (userId.HasValue)
		{
			Query.Where(pm => pm.UserId == userId);
		}

		if (projectId.HasValue)
		{
			Query.Where(pm => pm.ProjectId == projectId);
		}

		if (id.HasValue)
		{
			Query.Where(pm => pm.Id == id);
		}
	}
}