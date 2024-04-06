using System.Globalization;
using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class TaskProjectionSpec : BaseSpec<TaskProjectionSpec, ProjectTask, ProjectTaskDTO>
{
    protected override Expression<Func<ProjectTask, ProjectTaskDTO>> Spec => e => new ProjectTaskDTO
    {
        TaskId = e.Id,
        TaskName = e.TaskName,
        Description = e.Description,
        Status = e.Status,
        AssignedToUser = new UserDTO
        {
            Id = e.AssignedToUser.Id,
            Name = e.AssignedToUser.Name,
            Email = e.AssignedToUser.Email,
            Role = e.AssignedToUser.Role
        },
        Project = new ProjectDTO
        {
            ProjectName = e.Project.ProjectName,
            Description = e.Project.Description
        },
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt,
    };

    public TaskProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public TaskProjectionSpec(Guid id) : base(id)
    {
    }

    public TaskProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.TaskName, searchExpr) ||
                          EF.Functions.ILike(e.Status, searchExpr) ||
                          EF.Functions.ILike(e.AssignedToUser.Name, searchExpr) ||
                          EF.Functions.ILike(e.UpdatedAt.ToString(CultureInfo.InvariantCulture), searchExpr));
    }
}