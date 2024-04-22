using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class QuestionController : AuthorizedController
{
	private readonly IQuestionService _questionService;

	public QuestionController(IUserService userService, IQuestionService questionService) : base(userService)
	{
		_questionService = questionService;
	}
	
    [Authorize]
	[HttpGet("{questionId:guid}")]
	public async Task<ActionResult<RequestResponse<QuestionDTO>>> GetQuestion([FromRoute] Guid questionId)
	{
		var currentUser = await GetCurrentUser();

		return this.FromServiceResponse(await _questionService.GetQuestion(questionId, currentUser.Result));
	}
    
	[Authorize]
	[HttpGet]
	public async Task<ActionResult<RequestResponse<PagedResponse<QuestionDTO>>>> GetQuestions([FromQuery] PaginationSearchQueryParams pagination)
	{
		var currentUser = await GetCurrentUser();
		
		var result = await _questionService.GetQuestions(pagination, currentUser.Result);
		return this.FromServiceResponse(result);

	}
	
	[Authorize]
	[HttpGet("{questionId:guid}")]
	public async Task<ActionResult<RequestResponse<List<AnswerGetDTO>>>> GetQuestionAnswers([FromRoute] Guid questionId)
	{
		var currentUser = await GetCurrentUser();

		return this.FromServiceResponse(await _questionService.GetQuestionAnswers(questionId, currentUser.Result));
	}
	
	[Authorize]
	[HttpPost]
	public async Task<ActionResult<RequestResponse>> Add([FromBody] QuestionAddDTO question)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
			this.FromServiceResponse(await _questionService.AddQuestion(question, currentUser.Result)) : 
			this.ErrorMessageResult(currentUser.Error);
	}

	[Authorize]
	[HttpPut]
	public async Task<ActionResult<RequestResponse>> Update([FromBody] QuestionUpdateDTO question)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
			this.FromServiceResponse(await _questionService.UpdateQuestion(question, currentUser.Result)) : 
			this.ErrorMessageResult(currentUser.Error);
	}
}