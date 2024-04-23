using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class ProjectProjectionSpec : BaseSpec<ProjectProjectionSpec, Project, ProjectDTO>
{
    protected override Expression<Func<Project, ProjectDTO>> Spec => e => new ProjectDTO
    {
        ProjectId = e.Id,
        ProjectName = e.ProjectName,
        Description = e.Description,
        CreatedByUser = new UserDTO
        {
            Id = e.CreatedByUser.Id,
            Name = e.CreatedByUser.Name,
            Email = e.CreatedByUser.Email,
            Role = e.CreatedByUser.Role
        },
        CreatedAt = e.CreatedAt,
    };

    public ProjectProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public ProjectProjectionSpec(Guid id) : base(id)
    {
    }

    public ProjectProjectionSpec(string? search, ICollection<Guid> projectIds)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => projectIds.Contains(e.Id) && (EF.Functions.ILike(e.ProjectName, searchExpr) ||
                                                    EF.Functions.ILike(e.Description, searchExpr) ||
                                                    EF.Functions.ILike(e.CreatedByUser.Name, searchExpr)));
    }
}