using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.FeedbackAPI;
using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class FeedbackService : IFeedbackService
{
	private readonly IRepository<WebAppDatabaseContext> _repository;
	
	public FeedbackService(IRepository<WebAppDatabaseContext> repository)
	{
		_repository = repository;
	}
	
	public async Task<ServiceResponse<FeedbackDTO>> GetFeedback(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var feedback = await _repository.GetAsync(new FeedbackProjectionSpec(id), cancellationToken);

		if (feedback == null)
			return ServiceResponse<FeedbackDTO>.FromError(CommonErrors.FeedbackNotFound);
		
		var askingUser = await _repository.GetAsync(new UserSpec(feedback.GivenBy.Id), cancellationToken);
		var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);

		if (currentUser == null || askingUser == null)
			return ServiceResponse<FeedbackDTO>.FromError(CommonErrors.UserNotFound);

		return ServiceResponse<FeedbackDTO>.ForSuccess(feedback);
		// return askingUser.TeamId == currentUser.TeamId
		// 	? ServiceResponse<FeedbackDTO>.ForSuccess(feedback)
		// 	: ServiceResponse<FeedbackDTO>.FromError(CommonErrors.AccessNotAllowed);
	}

	public async Task<ServiceResponse> AddFeedback(FeedbackAddDTO feedback, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		await _repository.AddAsync(new Feedback
		{
			Rating = feedback.Rating,
			Suggestion = feedback.Suggestion ?? "",
			FrequentedSection = feedback.FrequentedSection,
			ResponseWanted = feedback.ResponseWanted,
			UserId = requestingUser.Id
		}, cancellationToken);
        
		return ServiceResponse.ForSuccess();
	}

	public async Task<ServiceResponse> UpdateFeedback(FeedbackUpdateDTO feedback, UserDTO requestingUser,
		CancellationToken cancellationToken = default)
	{
		var oldFeedback = await _repository.GetAsync(new FeedbackSpec(requestingUser.Id), cancellationToken);
		if (oldFeedback == null)
			return ServiceResponse.FromError(CommonErrors.FeedbackNotFound);

		if (requestingUser.Id != oldFeedback.UserId)
			return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);

		if (feedback.Suggestion != null)
			oldFeedback.Suggestion = feedback.Suggestion;
		
		if (feedback.FrequentedSection != null)
			oldFeedback.FrequentedSection = feedback.FrequentedSection;
		
		if (feedback.Rating != null)
			oldFeedback.Rating = feedback.Rating ?? oldFeedback.ResponseWanted;
		
		if (feedback.ResponseWanted != null)
			oldFeedback.ResponseWanted = feedback.ResponseWanted ?? oldFeedback.ResponseWanted;
        
		await _repository.UpdateAsync(oldFeedback, cancellationToken);
        
		return ServiceResponse.ForSuccess();
	}

	public async Task<ServiceResponse> DeleteFeedback(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var oldFeedback = await _repository.GetAsync(new FeedbackSpec(id), cancellationToken);
		if (oldFeedback == null)
			return ServiceResponse.FromError(CommonErrors.FeedbackNotFound);

		if (requestingUser.Id != oldFeedback.UserId)
			return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);

		await _repository.DeleteAsync<Feedback>(id, cancellationToken);

		return ServiceResponse.ForSuccess();
	}
}