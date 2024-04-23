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
public class ProjectController : AuthorizedController
{
	private readonly IProjectService _projectService;

	public ProjectController(IUserService userService, IProjectService projectService) : base(userService)
	{
		_projectService = projectService;
	}
	
	[Authorize]
	[HttpGet("{projectId:guid}")]
	public async Task<ActionResult<RequestResponse<ProjectDTO>>> GetProject([FromRoute] Guid projectId)
	{
		var currentUser = await GetCurrentUser();

		return this.FromServiceResponse(await _projectService.GetProject(projectId, currentUser.Result));
	}
    
	[Authorize]
	[HttpGet]
	public async Task<ActionResult<RequestResponse<PagedResponse<ProjectDTO>>>> GetProjects([FromQuery] PaginationSearchQueryParams pagination)
	{
		var currentUser = await GetCurrentUser();
		
		var result = await _projectService.GetProjects(pagination, currentUser.Result);
		return this.FromServiceResponse(result);

	}
	
	[Authorize]
	[HttpGet("{projectId:guid}")]
	public async Task<ActionResult<RequestResponse<List<TaskGetDTO>>>> GetProjectTasks([FromRoute] Guid projectId)
	{
		var currentUser = await GetCurrentUser();

		return this.FromServiceResponse(await _projectService.GetProjectTasks(projectId, currentUser.Result));
	}
	
	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<ActionResult<RequestResponse>> Add([FromBody] ProjectAddDTO project)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
			this.FromServiceResponse(await _projectService.AddProject(project, currentUser.Result)) : 
			this.ErrorMessageResult(currentUser.Error);
	}

	[Authorize(Roles = "Admin")]
	[HttpPut]
	public async Task<ActionResult<RequestResponse>> Update([FromBody] ProjectUpdateDTO project)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
			this.FromServiceResponse(await _projectService.UpdateProject(project, currentUser.Result)) : 
			this.ErrorMessageResult(currentUser.Error);
	}
	
	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<ActionResult<RequestResponse>> AddMember([FromBody] MembersDTO members)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
			this.FromServiceResponse(await _projectService.AddMembers(members, currentUser.Result)) : 
			this.ErrorMessageResult(currentUser.Error);
	}
	
	[Authorize(Roles = "Admin")]
	[HttpDelete]
	public async Task<ActionResult<RequestResponse>> DeleteMember([FromBody] MembersDTO members)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ? 
			this.FromServiceResponse(await _projectService.DeleteMembers(members, currentUser.Result)) : 
			this.ErrorMessageResult(currentUser.Error);
	}
}