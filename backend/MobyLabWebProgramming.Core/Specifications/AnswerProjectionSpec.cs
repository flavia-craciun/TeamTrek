using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.AnswerAPI;
using MobyLabWebProgramming.Core.DataTransferObjects.QuestionAPI;
using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class AnswerProjectionSpec : BaseSpec<AnswerProjectionSpec, Answer, AnswerDTO>
{
    protected override Expression<Func<Answer, AnswerDTO>> Spec => e => new AnswerDTO
    {
        AnswerId = e.Id,
        Description = e.Description,
        Question = new QuestionDTO
        {
            QuestionId = e.Question.Id,
            Title = e.Question.Title,
            Description = e.Question.Description,
            AskingUser = new UserDTO
            {
                Id = e.Question.User.Id,
                Name = e.Question.User.Name,
                Email = e.Question.User.Email,
                Role = e.Question.User.Role
            },
            CreatedAt = e.Question.CreatedAt,
            UpdatedAt = e.Question.UpdatedAt
        },
        RespondingUser = new UserDTO
        {
            Id = e.User.Id,
            Name = e.User.Name,
            Email = e.User.Email,
            Role = e.User.Role
        },
        CreatedAt = e.CreatedAt,
        UpdatedAt = e.UpdatedAt
    };

    public AnswerProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public AnswerProjectionSpec(Guid questionId)
    {
        Query.Where( a =>  a.QuestionId == questionId);
    }


    public AnswerProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Description, searchExpr) ||
                                                   EF.Functions.ILike(e.User.Name, searchExpr) ||
                                                   EF.Functions.ILike(e.Question.Title, searchExpr));
    }
}