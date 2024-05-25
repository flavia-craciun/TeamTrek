using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.AnswerAPI;
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

public class AnswerService : IAnswerService
{
	private readonly IRepository<WebAppDatabaseContext> _repository;
	
	public AnswerService(IRepository<WebAppDatabaseContext> repository)
	{
		_repository = repository;
	}
	
	public async Task<ServiceResponse<AnswerDTO>> GetAnswer(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var answer = await _repository.GetAsync(new AnswerProjectionSpec(id), cancellationToken);

		if (answer == null)
			return ServiceResponse<AnswerDTO>.FromError(CommonErrors.QuestionNotFound);
		
		var askingUser = await _repository.GetAsync(new UserSpec(answer.RespondingUser.Id), cancellationToken);
		var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);

		if (currentUser == null || askingUser == null)
			return ServiceResponse<AnswerDTO>.FromError(CommonErrors.UserNotFound);

		return askingUser.TeamId == currentUser.TeamId
			? ServiceResponse<AnswerDTO>.ForSuccess(answer)
			: ServiceResponse<AnswerDTO>.FromError(CommonErrors.AccessNotAllowed);
	}

	public async Task<ServiceResponse<int>> GetAnswerCount(CancellationToken cancellationToken = default)
	{
		return ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Answer>(cancellationToken));
	}

	public async Task<ServiceResponse> AddAnswer(AnswerAddDTO answer, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var existingQuestion = await _repository.GetAsync(new QuestionSpec(answer.QuestionId), cancellationToken);
		if (existingQuestion == null)
			return ServiceResponse.FromError(CommonErrors.QuestionNotFound);
		
		var askingUser = await _repository.GetAsync(new UserSpec(existingQuestion.UserId), cancellationToken);
		var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);

		if (currentUser == null || askingUser == null)
			return ServiceResponse.FromError(CommonErrors.UserNotFound);

		if (askingUser.TeamId != currentUser.TeamId)
			return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);

		await _repository.AddAsync(new Answer
		{
			Description = answer.Description,
			QuestionId = answer.QuestionId,
			UserId = requestingUser.Id
		}, cancellationToken);
        
		return ServiceResponse.ForSuccess();
	}

	public async Task<ServiceResponse> UpdateAnswer(AnswerUpdateDTO answer, UserDTO requestingUser,
		CancellationToken cancellationToken = default)
	{
		var oldAnswer = await _repository.GetAsync(new AnswerSpec(answer.AnswerId), cancellationToken);
		if (oldAnswer == null)
			return ServiceResponse.FromError(CommonErrors.AnswerNotFound);

		if (requestingUser.Id != oldAnswer.UserId && requestingUser.Role != UserRoleEnum.Admin)
			return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);

		if (answer.Description != null)
			oldAnswer.Description = answer.Description;
        
		await _repository.UpdateAsync(oldAnswer, cancellationToken);
        
		return ServiceResponse.ForSuccess();
	}

	public async Task<ServiceResponse> DeleteAnswer(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var oldAnswer = await _repository.GetAsync(new AnswerSpec(id), cancellationToken);
		if (oldAnswer == null)
			return ServiceResponse.FromError(CommonErrors.AnswerNotFound);

		if (requestingUser.Id != oldAnswer.UserId && requestingUser.Role != UserRoleEnum.Admin)
			return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);

		await _repository.DeleteAsync<Answer>(id, cancellationToken);

		return ServiceResponse.ForSuccess();
	}
}