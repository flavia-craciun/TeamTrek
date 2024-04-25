using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.TeamAPI;
using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ITeamService
{ 
       public Task<ServiceResponse<TeamDTO>> GetTeam(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
       public Task<ServiceResponse<PagedResponse<TeamDTO>>> GetTeams(PaginationSearchQueryParams pagination, UserDTO requestingUser, CancellationToken cancellationToken = default);
       public Task<ServiceResponse<List<MemberDTO>>> GetTeamMembers(Guid teamId, UserDTO requestingUser, CancellationToken cancellationToken = default);
       public Task<ServiceResponse> AddTeam(TeamAddDTO user, UserDTO requestingUser, CancellationToken cancellationToken = default);
       public Task<ServiceResponse> UpdateTeam(TeamUpdateDTO team, UserDTO requestingUser, CancellationToken cancellationToken = default);

}
