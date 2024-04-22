using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ITeamService
{ 
       public Task<ServiceResponse<TeamDTO>> GetTeam(Guid id, CancellationToken cancellationToken = default);
       public Task<ServiceResponse<PagedResponse<TeamDTO>>> GetTeams(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
       public Task<ServiceResponse<List<MemberDTO>>> GetTeamMembers(Guid teamId, CancellationToken cancellationToken = default);
       public Task<ServiceResponse> AddTeam(TeamAddDTO user, CancellationToken cancellationToken = default);
       public Task<ServiceResponse> UpdateTeam(TeamUpdateDTO team, CancellationToken cancellationToken = default);

}
