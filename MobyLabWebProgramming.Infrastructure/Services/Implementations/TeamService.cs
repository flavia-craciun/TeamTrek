using System.Net;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.TeamAPI;
using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;
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

public class TeamService : ITeamService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    
    public TeamService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    
    public async Task<ServiceResponse<TeamDTO>> GetTeam(Guid teamId, UserDTO requestingUser, CancellationToken cancellationToken = default)
        
    {
        var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
		
        if (currentUser == null)
            return ServiceResponse<TeamDTO>.FromError(CommonErrors.UserNotFound);
		
        var userMembership = await _repository.GetAsync(new TeamMembershipSpec(currentUser.MembershipId), cancellationToken);
		
        if (userMembership == null || userMembership.TeamId != teamId)
            return ServiceResponse<TeamDTO>.FromError(CommonErrors.AccessNotAllowed);
        
        var result = await _repository.GetAsync(new TeamProjectionSpec(teamId), cancellationToken);

        return result != null
            ? ServiceResponse<TeamDTO>.ForSuccess(result)
            : ServiceResponse<TeamDTO>.FromError(CommonErrors.TeamNotFound);
    }
    
    public async Task<ServiceResponse<PagedResponse<TeamDTO>>> GetTeams(PaginationSearchQueryParams pagination, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
		
        if (currentUser == null)
            return ServiceResponse<PagedResponse<TeamDTO>>.FromError(CommonErrors.UserNotFound);
		
        var userMembership = await _repository.GetAsync(new TeamMembershipSpec(currentUser.MembershipId), cancellationToken);
        if (userMembership == null)
            return ServiceResponse<PagedResponse<TeamDTO>>.FromError(CommonErrors.AccessNotAllowed);

        // FIXME: same team check
        var result = await _repository.PageAsync(pagination, new TeamProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<TeamDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<List<MemberDTO>>> GetTeamMembers(Guid teamId, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
        if (currentUser == null)
            return ServiceResponse<List<MemberDTO>>.FromError(CommonErrors.UserNotFound);
        
        // FIXME: Poate fi modificat dupa ce elimin tabela de membership
        var userMembership = await _repository.GetAsync(new TeamMembershipSpec(currentUser.MembershipId), cancellationToken);
        if (userMembership == null || userMembership.TeamId != teamId)
            return ServiceResponse<List<MemberDTO>>.FromError(CommonErrors.AccessNotAllowed);

        var result = await _repository.ListAsync(new TeamMembershipSpec(teamId), cancellationToken);

        if (result.Count == 0) 
            return ServiceResponse<List<MemberDTO>>.FromError(CommonErrors.TeamNotFound);
        
        var userIds = result.Select(tm => tm.UserId).ToList();
        var users = await _repository.ListAsync(new UserSpec(userIds), cancellationToken);

        if (users.Count == 0) 
            return ServiceResponse<List<MemberDTO>>.FromError(CommonErrors.MembersNotFound);
            
        var userDTOs = users.Select(u => new MemberDTO
        {
            Name = u.Name,
            Email = u.Email,
            Role = u.Role
        }).ToList();

        return ServiceResponse<List<MemberDTO>>.ForSuccess(userDTOs);

    }
    
    public async Task<ServiceResponse> AddTeam(TeamAddDTO team, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        var teamLeader = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
        if (teamLeader == null || teamLeader.Role != UserRoleEnum.Admin)
            return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);
        
        // FIXME: Poate fi scoasa dupa ce elimin tabela de membership
        var userMembership = await _repository.GetAsync(new TeamMembershipSpec(teamLeader.MembershipId), cancellationToken);
        if (userMembership == null || userMembership.TeamId != null)
            return ServiceResponse<TeamDTO>.FromError(CommonErrors.AccessNotAllowed);
        
        var existingTeam = await _repository.GetAsync(new TeamSpec(team.TeamName), cancellationToken);
        if (existingTeam != null)
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The team name is already in use!", ErrorCodes.TeamAlreadyExists));

        await _repository.AddAsync(new Team
        {
            TeamName = team.TeamName,
            TeamLeaderId = requestingUser.Id
        }, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse> UpdateTeam(TeamUpdateDTO team, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        var teamLeader = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
        if (teamLeader == null || teamLeader.Role != UserRoleEnum.Admin)
            return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);
        
        // FIXME: Poate fi modificat dupa ce elimin tabela de membership
        var userMembership = await _repository.GetAsync(new TeamMembershipSpec(teamLeader.MembershipId), cancellationToken);
        if (userMembership == null || userMembership.TeamId != team.TeamLeaderId)
            return ServiceResponse<TeamDTO>.FromError(CommonErrors.AccessNotAllowed);

        var oldTeam = await _repository.GetAsync(new TeamSpec(team.TeamId), cancellationToken);
        if (oldTeam == null)
            return ServiceResponse.FromError(CommonErrors.TeamNotFound);

        oldTeam.TeamName = team.TeamName;
        
        await _repository.UpdateAsync(oldTeam, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
}