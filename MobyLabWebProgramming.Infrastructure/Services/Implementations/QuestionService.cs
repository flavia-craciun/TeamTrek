using System.Net;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class QuestionService : IQuestionService
{
	private readonly IRepository<WebAppDatabaseContext> _repository;
    
	public QuestionService(IRepository<WebAppDatabaseContext> repository)
	{
		_repository = repository;
	}
	public async Task<ServiceResponse<QuestionDTO>> GetQuestion(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var question = await _repository.GetAsync(new QuestionProjectionSpec(id), cancellationToken);

		if (question == null)
			return ServiceResponse<QuestionDTO>.FromError(CommonErrors.QuestionNotFound);
		
		var askingUser = await _repository.GetAsync(new UserSpec(question.AskingUser.Id), cancellationToken);
		var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);

		if (currentUser == null || askingUser == null)
			return ServiceResponse<QuestionDTO>.FromError(CommonErrors.UserNotFound);
		
		var askingUserMembership = await _repository.GetAsync(new TeamMembershipSpec(askingUser.MembershipId), cancellationToken);
		var currentUserMembership = await _repository.GetAsync(new TeamMembershipSpec(currentUser.MembershipId), cancellationToken);
		
		if (askingUserMembership == null || currentUserMembership == null)
			return ServiceResponse<QuestionDTO>.FromError(CommonErrors.AccessNotAllowed);

		return askingUserMembership.TeamId == currentUserMembership.TeamId
			? ServiceResponse<QuestionDTO>.ForSuccess(question)
			: ServiceResponse<QuestionDTO>.FromError(CommonErrors.AccessNotAllowed);

	}

	public async Task<ServiceResponse<PagedResponse<QuestionDTO>>> GetQuestions(PaginationSearchQueryParams pagination, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
		
		if (currentUser == null)
			return ServiceResponse<PagedResponse<QuestionDTO>>.FromError(CommonErrors.UserNotFound);
		
		var userMembership = await _repository.GetAsync(new TeamMembershipSpec(currentUser.MembershipId), cancellationToken);
		
		if (userMembership == null)
			return ServiceResponse<PagedResponse<QuestionDTO>>.FromError(CommonErrors.AccessNotAllowed);
		
		var result = await _repository.PageAsync(pagination, new QuestionProjectionSpec(pagination.Search, userMembership.TeamId), cancellationToken);
		
		return result.TotalCount != 0
			? ServiceResponse<PagedResponse<QuestionDTO>>.ForSuccess(result)
			: ServiceResponse<PagedResponse<QuestionDTO>>.FromError(CommonErrors.AccessNotAllowed);
	}

	public async Task<ServiceResponse<List<AnswerGetDTO>>> GetQuestionAnswers(Guid questionId, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var question = await _repository.GetAsync(new QuestionSpec(questionId), cancellationToken);

		if (question == null) 
			return ServiceResponse<List<AnswerGetDTO>>.FromError(CommonErrors.QuestionNotFound);
        
		var answers = await _repository.ListAsync(new AnswerSpec(questionId), cancellationToken);

		if (answers.Count == 0) 
			return ServiceResponse<List<AnswerGetDTO>>.FromError(CommonErrors.AnswersNotFound);
		
		var firstAnswer = answers.FirstOrDefault();
		
		var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
		var answerUser = await _repository.GetAsync(new UserSpec(firstAnswer.UserId), cancellationToken);
		
		if (currentUser == null || answerUser == null)
			return ServiceResponse<List<AnswerGetDTO>>.FromError(CommonErrors.UserNotFound);
		
		var userMembership = await _repository.GetAsync(new TeamMembershipSpec(currentUser.MembershipId), cancellationToken);
		var answerUserMembership = await _repository.GetAsync(new TeamMembershipSpec(answerUser.MembershipId), cancellationToken);
		
		if (userMembership == null || answerUserMembership == null)
			return ServiceResponse<List<AnswerGetDTO>>.FromError(CommonErrors.UserNotFound);

		if (userMembership.TeamId != answerUserMembership.TeamId)
			return ServiceResponse<List<AnswerGetDTO>>.FromError(CommonErrors.AccessNotAllowed);
		
		var answerDTOs = answers.Select(a => new AnswerGetDTO
		{
			Description = a.Description,
			RespondingUser = new UserGetDTO
			{
				Name = a.User.Name,
				Role = a.User.Role
			},
			UpdatedAt = a.UpdatedAt
		}).ToList();

		return ServiceResponse<List<AnswerGetDTO>>.ForSuccess(answerDTOs);
	}

	public async Task<ServiceResponse> AddQuestion(QuestionAddDTO question, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		
		await _repository.AddAsync(new Question
		{
			Title = question.Title,
			Description = question.Description,
			UserId = requestingUser.Id
		}, cancellationToken);
        
		return ServiceResponse.ForSuccess();
	}

	public async Task<ServiceResponse> UpdateQuestion(QuestionUpdateDTO question, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var oldQuestion = await _repository.GetAsync(new QuestionSpec(question.QuestionId), cancellationToken);
		if (oldQuestion == null)
			return ServiceResponse.FromError(CommonErrors.QuestionNotFound);

		if (requestingUser.Id != oldQuestion.UserId && requestingUser.Role != UserRoleEnum.Admin)
			return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);
		
		if (question.Description != null)
			oldQuestion.Description = question.Description;
		
		if (question.Title != null)
			oldQuestion.Title = question.Title;
        
		await _repository.UpdateAsync(oldQuestion, cancellationToken);
        
		return ServiceResponse.ForSuccess();	
	}
}