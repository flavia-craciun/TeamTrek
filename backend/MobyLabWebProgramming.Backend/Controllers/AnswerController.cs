using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.AnswerAPI;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AnswerController : AuthorizedController
{
	private readonly IAnswerService _answerService;

	public AnswerController(IUserService userService, IAnswerService answerService) : base(userService)
	{
		_answerService = answerService;
	}
	
    [Authorize]
	[HttpGet("{answerId:guid}")]
	public async Task<ActionResult<RequestResponse<AnswerDTO>>> GetAnswer([FromRoute] Guid answerId)
	{
		var currentUser = await GetCurrentUser();

		return currentUser.Result != null ? 
		    this.FromServiceResponse(await _answerService.GetAnswer(answerId, currentUser.Result)) : 
		    this.ErrorMessageResult<AnswerDTO>(currentUser.Error);
	}
	
	[Authorize]
	[HttpPost]
	public async Task<ActionResult<RequestResponse>> Add([FromBody] AnswerAddDTO answer)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
		    this.FromServiceResponse(await _answerService.AddAnswer(answer, currentUser.Result)) : 
		    this.ErrorMessageResult(currentUser.Error);
	}

	[Authorize]
	[HttpPut]
	public async Task<ActionResult<RequestResponse>> Update([FromBody] AnswerUpdateDTO answer)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
			this.FromServiceResponse(await _answerService.UpdateAnswer(answer, currentUser.Result)) : 
			this.ErrorMessageResult(currentUser.Error);
	}
	
	[Authorize]
	[HttpDelete("{answerId:guid}")]
	public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid answerId)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
			this.FromServiceResponse(await _answerService.DeleteAnswer(answerId, currentUser.Result)) : 
			this.ErrorMessageResult(currentUser.Error);
	}
}