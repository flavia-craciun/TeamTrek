using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.QuestionAPI;
using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class QuestionProjectionSpec : BaseSpec<QuestionProjectionSpec, Question, QuestionDTO>
{
    protected override Expression<Func<Question, QuestionDTO>> Spec => e => new QuestionDTO
    {
        QuestionId = e.Id,
        Title = e.Title,
        Description = e.Description,
        AskingUser = new UserDTO
        {
            Id = e.User.Id,
            Name = e.User.Name,
            Email = e.User.Email,
            Role = e.User.Role
        },
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public QuestionProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public QuestionProjectionSpec(Guid id) : base(id)
    {
    }

    public QuestionProjectionSpec(string? search, Guid teamId)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => e.User.TeamId == teamId && EF.Functions.ILike(e.Title, searchExpr) ||
                         EF.Functions.ILike(e.User.Name, searchExpr));
    }
}