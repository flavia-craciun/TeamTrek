using System.Net;
using MobyLabWebProgramming.Core.DataTransferObjects;
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
	private readonly ILoginService _loginService;
	
	public AnswerService(IRepository<WebAppDatabaseContext> repository, ILoginService loginService)
	{
		_repository = repository;
		_loginService = loginService;
	}
	
	public async Task<ServiceResponse<AnswerDTO>> GetAnswer(Guid id, CancellationToken cancellationToken = default)
	{
		var result = await _repository.GetAsync(new AnswerProjectionSpec(id), cancellationToken);

		return result != null ? 
			ServiceResponse<AnswerDTO>.ForSuccess(result) : 
			ServiceResponse<AnswerDTO>.FromError(CommonErrors.AnswerNotFound);
	}

	public async Task<ServiceResponse<int>> GetAnswerCount(CancellationToken cancellationToken = default)
	{
		return ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Answer>(cancellationToken));
	}

	public async Task<ServiceResponse> AddAnswer(AnswerAddDTO answer, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var existingQuestion = await _repository.GetAsync(new QuestionSpec(answer.QuestionId), cancellationToken);
		if (existingQuestion == null)
		{
			return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "This question doesn't exist", ErrorCodes.EntityNotFound));
		}

		await _repository.AddAsync(new Answer
		{
			Description = answer.Description,
			QuestionId = answer.QuestionId,
			UserId = requestingUser.Id
		}, cancellationToken);
        
		return ServiceResponse.ForSuccess();	}

	public async Task<ServiceResponse> UpdateAnswer(AnswerUpdateDTO answer, UserDTO requestingUser,
		CancellationToken cancellationToken = default)
	{
		var oldAnswer = await _repository.GetAsync(new AnswerSpec(answer.AnswerId), cancellationToken);
		if (oldAnswer == null)
		{
			return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "The answer doesn't exist!", ErrorCodes.EntityNotFound));
		}

		if (requestingUser.Id != oldAnswer.UserId || requestingUser.Role != UserRoleEnum.Admin)
		{
			return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the owner user can update the answer!", ErrorCodes.CannotUpdate));
		}

		oldAnswer.Description = answer.Description;
        
		await _repository.UpdateAsync(oldAnswer, cancellationToken);
        
		return ServiceResponse.ForSuccess();
	}

	public async Task<ServiceResponse> DeleteAnswer(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		if (requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != id)
		{
			return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the owner user can delete the answer!", ErrorCodes.CannotDelete));
		}

		await _repository.DeleteAsync<Answer>(id, cancellationToken);

		return ServiceResponse.ForSuccess();
	}
}