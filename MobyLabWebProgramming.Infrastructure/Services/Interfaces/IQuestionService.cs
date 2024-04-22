using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IQuestionService
{
	public Task<ServiceResponse<QuestionDTO>> GetQuestion(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
	public Task<ServiceResponse<PagedResponse<QuestionDTO>>> GetQuestions(PaginationSearchQueryParams pagination, UserDTO requestingUser, CancellationToken cancellationToken = default);
	public Task<ServiceResponse<List<AnswerGetDTO>>> GetQuestionAnswers(Guid questionId, UserDTO requestingUser, CancellationToken cancellationToken = default);
	public Task<ServiceResponse> AddQuestion(QuestionAddDTO question, UserDTO requestingUser, CancellationToken cancellationToken = default);
	public Task<ServiceResponse> UpdateQuestion(QuestionUpdateDTO question, UserDTO requestingUser, CancellationToken cancellationToken = default);
}