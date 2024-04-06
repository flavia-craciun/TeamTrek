using System.Net;
using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
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

    public async Task<ServiceResponse<List<UserDTO>>> GetTeamMembers(Guid teamId, CancellationToken cancellationToken = default)
    {
        var result = await _repository.ListAsync(new TeamMembershipSpec(teamId), cancellationToken);

        if (result != null)
        {
            var userIds = result.Select(tm => tm.UserId).ToList();

            var users = await _repository.ListAsync(new UserSpec(userIds), cancellationToken);

            if (users != null)
            {
                var userDTOs = users.Select(u => new UserDTO
                {
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role
                }).ToList();

                return ServiceResponse<List<UserDTO>>.ForSuccess(userDTOs);
            }
            else
            {
                return ServiceResponse<List<UserDTO>>.FromError(CommonErrors.MembersNotFound);
            }
        }
        else
        {
            return ServiceResponse<List<UserDTO>>.FromError(CommonErrors.TeamNotFound);
        }
    }

    public Task<ServiceResponse> UpdateTeam(TeamUpdateDTO team, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    public Task<ServiceResponse> AddTeam(TeamUpdateDTO team, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}