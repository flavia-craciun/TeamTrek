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

public class TeamService : ITeamService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    
    public TeamService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    
    public async Task<ServiceResponse<TeamDTO>> GetTeam(Guid teamId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new TeamProjectionSpec(teamId), cancellationToken);

        return result != null ? 
            ServiceResponse<TeamDTO>.ForSuccess(result) : 
            ServiceResponse<TeamDTO>.FromError(CommonErrors.TeamNotFound); // Pack the result or error into a ServiceResponse.
    }
    
    public async Task<ServiceResponse<PagedResponse<TeamDTO>>> GetTeams(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new TeamProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<TeamDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<List<MemberDTO>>> GetTeamMembers(Guid teamId, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Id: {teamId}");
        var result = await _repository.ListAsync(new TeamMembershipSpec(teamId), cancellationToken);
        Console.WriteLine($"Memberships: {result.Count}");

        if (result == null) 
            return ServiceResponse<List<MemberDTO>>.FromError(CommonErrors.TeamNotFound);
        
        var userIds = result.Select(tm => tm.UserId).ToList();
        var users = await _repository.ListAsync(new UserSpec(userIds), cancellationToken);

        if (users == null) 
            return ServiceResponse<List<MemberDTO>>.FromError(CommonErrors.MembersNotFound);
            
        var userDTOs = users.Select(u => new MemberDTO
        {
            Name = u.Name,
            Email = u.Email,
            Role = u.Role
        }).ToList();

        return ServiceResponse<List<MemberDTO>>.ForSuccess(userDTOs);

    }
    
    public async Task<ServiceResponse> AddTeam(TeamAddDTO team, CancellationToken cancellationToken = default)
    {
        var teamLeader = await _repository.GetAsync(new UserSpec(team.TeamLeaderId), cancellationToken);
        if (teamLeader == null || teamLeader.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only an admin can add a new team!", ErrorCodes.CannotAdd));
        }
        
        // FIXME: add condition to check if the currentUser already has an existing team

        var existingTeam = await _repository.GetAsync(new TeamSpec(team.TeamName), cancellationToken);
        if (existingTeam != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The team name is already in use!", ErrorCodes.TeamAlreadyExists));
        }

        await _repository.AddAsync(new Team
        {
            TeamName = team.TeamName,
            TeamLeaderId = team.TeamLeaderId
        }, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse> UpdateTeam(TeamUpdateDTO team, CancellationToken cancellationToken = default)
    {
        var teamLeader = await _repository.GetAsync(new UserSpec(team.TeamLeaderId), cancellationToken);
        if (teamLeader == null || teamLeader.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the team leader can edit the team!", ErrorCodes.CannotUpdate));
        }

        var oldTeam = await _repository.GetAsync(new TeamSpec(team.TeamId), cancellationToken);
        if (oldTeam == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "The team doesn't exist!", ErrorCodes.EntityNotFound));
        }

        oldTeam.TeamName = team.TeamName;
        
        await _repository.UpdateAsync(oldTeam, cancellationToken);
        
        return ServiceResponse.ForSuccess();
    }
}