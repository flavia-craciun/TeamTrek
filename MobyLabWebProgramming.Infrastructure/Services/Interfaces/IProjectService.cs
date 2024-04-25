using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.ProjectAPI;
using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IProjectService
{
	public Task<ServiceResponse<ProjectDTO>> GetProject(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
	public Task<ServiceResponse<PagedResponse<ProjectDTO>>> GetProjects(PaginationSearchQueryParams pagination, UserDTO requestingUser, CancellationToken cancellationToken = default);
	public Task<ServiceResponse<List<TaskGetDTO>>> GetProjectTasks(Guid projectId, UserDTO requestingUser, CancellationToken cancellationToken = default);
	public Task<ServiceResponse> AddProject(ProjectAddDTO project, UserDTO requestingUser, CancellationToken cancellationToken = default);
	public Task<ServiceResponse> UpdateProject(ProjectUpdateDTO project, UserDTO requestingUser, CancellationToken cancellationToken = default);
	public Task<ServiceResponse> AddMembers(MembersDTO members, UserDTO requestingUser, CancellationToken cancellationToken = default);
	public Task<ServiceResponse> DeleteMembers(MembersDTO members, UserDTO requestingUser, CancellationToken cancellationToken = default);

}