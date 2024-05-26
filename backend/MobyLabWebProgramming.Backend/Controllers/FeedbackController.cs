using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.FeedbackAPI;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FeedbackController : AuthorizedController
{
	private readonly IFeedbackService _feedbackService;

	public FeedbackController(IUserService userService, IFeedbackService feedbackService) : base(userService)
	{
		_feedbackService = feedbackService;
	}
	
    [Authorize]
	[HttpGet("{feedbackId:guid}")]
	public async Task<ActionResult<RequestResponse<FeedbackDTO>>> GetFeedback([FromRoute] Guid feedbackId)
	{
		var currentUser = await GetCurrentUser();

		return currentUser.Result != null ? 
		    this.FromServiceResponse(await _feedbackService.GetFeedback(feedbackId, currentUser.Result)) : 
		    this.ErrorMessageResult<FeedbackDTO>(currentUser.Error);
	}
	
	[Authorize]
	[HttpPost]
	public async Task<ActionResult<RequestResponse>> Add([FromBody] FeedbackAddDTO feedback)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
		    this.FromServiceResponse(await _feedbackService.AddFeedback(feedback, currentUser.Result)) : 
		    this.ErrorMessageResult(currentUser.Error);
	}

	[Authorize]
	[HttpPut]
	public async Task<ActionResult<RequestResponse>> Update([FromBody] FeedbackUpdateDTO feedback)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
			this.FromServiceResponse(await _feedbackService.UpdateFeedback(feedback, currentUser.Result)) : 
			this.ErrorMessageResult(currentUser.Error);
	}
	
	[Authorize]
	[HttpDelete("{feedbackId:guid}")]
	public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid feedbackId)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
			this.FromServiceResponse(await _feedbackService.DeleteFeedback(feedbackId, currentUser.Result)) : 
			this.ErrorMessageResult(currentUser.Error);
	}
}