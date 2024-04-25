using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.TaskAPI;
using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ITaskService
{
	public Task<ServiceResponse<ProjectTaskDTO>> GetTask(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
    
	public Task<ServiceResponse<int>> GetTaskCount(CancellationToken cancellationToken = default);

	public Task<ServiceResponse> AddTask(TaskAddDTO answer, UserDTO requestingUser, CancellationToken cancellationToken = default);

	public Task<ServiceResponse> UpdateTask(ProjectTaskUpdateDTO answer, UserDTO requestingUser, CancellationToken cancellationToken = default);

	public Task<ServiceResponse> DeleteTask(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
}