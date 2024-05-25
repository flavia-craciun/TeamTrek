using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.TaskAPI;
using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class TaskService : ITaskService
{
	private readonly IRepository<WebAppDatabaseContext> _repository;
	
	public TaskService(IRepository<WebAppDatabaseContext> repository)
	{
		_repository = repository;
	}
	
	public async Task<ServiceResponse<ProjectTaskDTO>> GetTask(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
		if (currentUser == null)
			return ServiceResponse<ProjectTaskDTO>.FromError(CommonErrors.UserNotFound);

		var task = await _repository.GetAsync(new TaskProjectionSpec(id), cancellationToken);
		if (task == null)
			return ServiceResponse<ProjectTaskDTO>.FromError(CommonErrors.TaskNotFound);
		
		var membership = await _repository.GetAsync(new ProjectMembershipProjectionSpec(requestingUser.Id, task.Project.ProjectId), cancellationToken);

		return membership != null
			? ServiceResponse<ProjectTaskDTO>.ForSuccess(task)
			: ServiceResponse<ProjectTaskDTO>.FromError(CommonErrors.AccessNotAllowed);
	}

	public async Task<ServiceResponse<int>> GetTaskCount(CancellationToken cancellationToken = default)
	{
		return ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<ProjectTask>(cancellationToken));
	}

	public async Task<ServiceResponse> AddTask(TaskAddDTO task, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
		if (currentUser == null)
			return ServiceResponse.FromError(CommonErrors.UserNotFound);
		
		var membership = await _repository.GetAsync(new ProjectMembershipProjectionSpec(requestingUser.Id, task.ProjectId), cancellationToken);
		if (membership == null || currentUser.Role != UserRoleEnum.Admin)
			return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);
        
		await _repository.AddAsync(new ProjectTask
		{
			TaskName = task.TaskName,
			Description = task.Description,
			Status = task.Status,
			AssignedToUserId = task.AssignedToUserId,
			ProjectId = task.ProjectId
		}, cancellationToken);
        
		return ServiceResponse.ForSuccess();        
	}

	public async Task<ServiceResponse> UpdateTask(ProjectTaskUpdateDTO task, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
		if (currentUser == null)
			return ServiceResponse.FromError(CommonErrors.UserNotFound);

		var oldTask = await _repository.GetAsync(new TaskSpec(task.TaskId), cancellationToken);
		if (oldTask == null)
			return ServiceResponse.FromError(CommonErrors.TaskNotFound);
		
		var membership = await _repository.GetAsync(new ProjectMembershipProjectionSpec(requestingUser.Id, oldTask.ProjectId), cancellationToken);
		if (membership == null)
			return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);

		if (currentUser.Role == UserRoleEnum.Admin)
		{
			if (task.Description != null)
				oldTask.Description = task.Description;

			if (task.TaskName != null)
				oldTask.TaskName = task.TaskName;
		}

		if (task.Status != null)
			oldTask.Status = task.Status;
        
		await _repository.UpdateAsync(oldTask, cancellationToken);
        
		return ServiceResponse.ForSuccess();
	}

	public async Task<ServiceResponse> DeleteTask(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
		if (currentUser == null)
			return ServiceResponse.FromError(CommonErrors.UserNotFound);

		var oldTask = await _repository.GetAsync(new TaskSpec(id), cancellationToken);
		if (oldTask == null)
			return ServiceResponse.FromError(CommonErrors.TaskNotFound);
		
		var membership = await _repository.GetAsync(new ProjectMembershipProjectionSpec(requestingUser.Id, oldTask.ProjectId), cancellationToken);
		if (membership == null || currentUser.Role != UserRoleEnum.Admin)
			return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);

		await _repository.DeleteAsync<ProjectTask>(id, cancellationToken);

		return ServiceResponse.ForSuccess();	
	}
}