using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.TeamAPI;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TeamController : AuthorizedController
{
	private readonly ITeamService _teamService;

	public TeamController(IUserService userService, ITeamService teamService) : base(userService)
	{
		_teamService = teamService;
	}

    /// <summary>
    ///  This method implements the Read operation (R from CRUD) on a team.
    /// </summary>
    [Authorize]
	[HttpGet("{teamId:guid}")]
	public async Task<ActionResult<RequestResponse<TeamDTO>>> GetTeam([FromRoute] Guid id)
	{
		var currentUser = await GetCurrentUser();

		return currentUser.Result != null ? 
		    this.FromServiceResponse(await _teamService.GetTeam(id, currentUser.Result)) : 
		    this.ErrorMessageResult<TeamDTO>(currentUser.Error);
	}
    
	[Authorize]
	[HttpGet]
	public async Task<ActionResult<RequestResponse<PagedResponse<TeamDTO>>>> GetTeams([FromQuery] PaginationSearchQueryParams pagination)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ?
			this.FromServiceResponse(await _teamService.GetTeams(pagination, currentUser.Result)) :
			this.ErrorMessageResult<PagedResponse<TeamDTO>>(currentUser.Error);
	}
	
	[Authorize]
	[HttpGet("{teamId:guid}")]
	public async Task<ActionResult<RequestResponse<List<MemberDTO>>>> GetTeamMembers([FromRoute] Guid teamId)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ?
			this.FromServiceResponse(await _teamService.GetTeamMembers(teamId, currentUser.Result)) :
			this.ErrorMessageResult<List<MemberDTO>>(currentUser.Error);
	}
	
	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<ActionResult<RequestResponse>> Add([FromBody] TeamAddDTO team)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ?
			this.FromServiceResponse(await _teamService.AddTeam(team, currentUser.Result)) :
			this.ErrorMessageResult(currentUser.Error);
	}

	[Authorize(Roles = "Admin")]
	[HttpPut]
	public async Task<ActionResult<RequestResponse>> Update([FromBody] TeamUpdateDTO team)
	{
		var currentUser = await GetCurrentUser();
		
		return currentUser.Result != null ?
			this.FromServiceResponse(await _teamService.UpdateTeam(team, currentUser.Result)) :
			this.ErrorMessageResult(currentUser.Error);
	}
}