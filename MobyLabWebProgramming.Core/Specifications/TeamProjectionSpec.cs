using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.TeamAPI;
using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class TeamProjectionSpec : BaseSpec<TeamProjectionSpec, Team, TeamDTO>
{
    protected override Expression<Func<Team, TeamDTO>> Spec => e => new TeamDTO
    {
        TeamId = e.Id,
        TeamName = e.TeamName,
        TeamLeader = new UserDTO
        {
            Id = e.TeamLeader.Id,
            Name = e.TeamLeader.Name,
            Email = e.TeamLeader.Email,
            Role = e.TeamLeader.Role
        },
    };

    public TeamProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public TeamProjectionSpec(Guid id) : base(id)
    {
    }

    public TeamProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.TeamName, searchExpr) ||
                                                   EF.Functions.ILike(e.TeamLeader.Name, searchExpr));
    }
}