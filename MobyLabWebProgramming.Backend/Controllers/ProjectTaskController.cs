using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.TaskAPI;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProjectTaskController : AuthorizedController
{
	private readonly ITaskService _projectTaskService;

	public ProjectTaskController(IUserService userService, ITaskService projectTaskService) : base(userService)
	{
		_projectTaskService = projectTaskService;
	}
	
	[Authorize]
	[HttpGet("{taskId:guid}")]
	public async Task<ActionResult<RequestResponse<ProjectTaskDTO>>> GetProjectTask([FromRoute] Guid taskId)
	{
		var currentUser = await GetCurrentUser();

		return currentUser.Result != null ? 
			this.FromServiceResponse(await _projectTaskService.GetTask(taskId, currentUser.Result)) : 
			this.ErrorMessageResult<ProjectTaskDTO>(currentUser.Error);
	}
	
	[Authorize(Roles="Admin")]
	[HttpPost]
	public async Task<ActionResult<RequestResponse>> Add([FromBody] TaskAddDTO projectTask)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
			this.FromServiceResponse(await _projectTaskService.AddTask(projectTask, currentUser.Result)) : 
			this.ErrorMessageResult(currentUser.Error);
	}

	[Authorize]
	[HttpPut]
	public async Task<ActionResult<RequestResponse>> Update([FromBody] ProjectTaskUpdateDTO projectTask)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
			this.FromServiceResponse(await _projectTaskService.UpdateTask(projectTask, currentUser.Result)) : 
			this.ErrorMessageResult(currentUser.Error);
	}
	
	[Authorize]
	[HttpDelete("{taskId:guid}")]
	public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid taskId)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
			this.FromServiceResponse(await _projectTaskService.DeleteTask(taskId, currentUser.Result)) : 
			this.ErrorMessageResult(currentUser.Error);
	}
}