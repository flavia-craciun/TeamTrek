using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.ProjectAPI;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ProjectMembershipProjectionSpec : BaseSpec<ProjectMembershipProjectionSpec, ProjectMembership, ProjectMembershipDTO>
{
	protected override Expression<Func<ProjectMembership, ProjectMembershipDTO>> Spec => e => new()
	{
		Id = e.Id,
		ProjectId = e.ProjectId,
		UserId = e.UserId,
		// Map other properties as needed
	};

	public ProjectMembershipProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
	{
	}

	public ProjectMembershipProjectionSpec(Guid id) : base(id)
	{
	}
	
	public ProjectMembershipProjectionSpec(Guid projectId, Guid userId)
	{
		Query.Where(tm => tm.ProjectId == projectId && tm.UserId == userId);
	}

	// Add additional constructors or methods as needed for filtering
}