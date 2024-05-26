using System.Linq.Expressions;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;
public sealed class FeedbackProjectionSpec : BaseSpec<FeedbackProjectionSpec, Feedback, FeedbackDTO>
{
    protected override Expression<Func<Feedback, FeedbackDTO>> Spec => e => new FeedbackDTO
    {
        FeedbackId = e.Id,
        Rating = e.Rating,
        FrequentedSection = e.FrequentedSection,
        Suggestion = e.Suggestion,
        ResponseWanted = e.ResponseWanted,
        GivenBy = new UserDTO
        {
            Id = e.User.Id,
            Name = e.User.Name,
            Email = e.User.Email
        }
    };

    public FeedbackProjectionSpec(Guid userId)
    {
        Query.Where( a =>  a.UserId == userId);
    }
}