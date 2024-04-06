using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
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
    // [Authorize]
	[HttpGet("{id:guid}")]
	public async Task<ActionResult<RequestResponse<TeamDTO>>> GetTeam([FromRoute] Guid id)
	{
		// var currentUser = await GetCurrentUser();
		return this.FromServiceResponse(await _teamService.GetTeam(id));

		// return currentUser.Result != null ? 
		//     this.FromServiceResponse(await UserService.GetUser(id)) : 
		//     this.ErrorMessageResult<TeamDTO>(currentUser.Error);
	}
    
	// [Authorize]
	[HttpGet]
	public async Task<ActionResult<RequestResponse<PagedResponse<TeamDTO>>>> GetTeams([FromQuery] PaginationSearchQueryParams pagination)
	{
		// var currentUser = await GetCurrentUser();
		//
		// return currentUser.Result != null ?
		// 	this.FromServiceResponse(await _teamService.GetTeams(pagination)) :
		// 	this.ErrorMessageResult<PagedResponse<TeamDTO>>(currentUser.Error);

		return this.FromServiceResponse(await _teamService.GetTeams(pagination));

	}
	
	// [Authorize]
	[HttpGet("{teamId}/members")]
	public async Task<ActionResult<RequestResponse<List<UserDTO>>>> GetTeamMembers([FromRoute] Guid teamId)
	{
		// var currentUser = await GetCurrentUser();
		//
		// return currentUser.Result != null ?
		// 	this.FromServiceResponse(await _teamService.GetTeams(pagination)) :
		// 	this.ErrorMessageResult<PagedResponse<TeamDTO>>(currentUser.Error);
		
		return this.FromServiceResponse(await _teamService.GetTeamMembers(teamId));
	}
}