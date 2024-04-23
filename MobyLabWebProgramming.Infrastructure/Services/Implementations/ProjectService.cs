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

public class ProjectService : IProjectService
{
	private readonly IRepository<WebAppDatabaseContext> _repository;

	public ProjectService(IRepository<WebAppDatabaseContext> repository)
	{
		_repository = repository;
	}
	
	public async Task<ServiceResponse<ProjectDTO>> GetProject(Guid projectId, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var membership = await _repository.GetAsync(new ProjectMembershipProjectionSpec(projectId, requestingUser.Id), cancellationToken);
		if (membership == null)
			return ServiceResponse<ProjectDTO>.FromError(CommonErrors.AccessNotAllowed);

		var project = await _repository.GetAsync(new ProjectProjectionSpec(projectId), cancellationToken);
		
		return project == null ? 
			ServiceResponse<ProjectDTO>.FromError(CommonErrors.ProjectNotFound) :
			ServiceResponse<ProjectDTO>.ForSuccess(project);
	}

	public async Task<ServiceResponse<PagedResponse<ProjectDTO>>> GetProjects(PaginationSearchQueryParams pagination, UserDTO requestingUser,
		CancellationToken cancellationToken = default)
	{
		var projectMemberships = await _repository.ListAsync(new ProjectMembershipSpec(requestingUser.Id), cancellationToken);
		var projectIds = projectMemberships.Select(pm => pm.ProjectId).ToList();
		
		var result = await _repository.PageAsync(pagination, new ProjectProjectionSpec(pagination.Search, projectIds), cancellationToken);
		
		return result.TotalCount != 0
			? ServiceResponse<PagedResponse<ProjectDTO>>.ForSuccess(result)
			: ServiceResponse<PagedResponse<ProjectDTO>>.FromError(CommonErrors.AccessNotAllowed);
	}

	public async Task<ServiceResponse<List<TaskGetDTO>>> GetProjectTasks(Guid projectId, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var membership = await _repository.GetAsync(new ProjectMembershipProjectionSpec(projectId, requestingUser.Id), cancellationToken);
		if (membership == null)
			return ServiceResponse<List<TaskGetDTO>>.FromError(CommonErrors.AccessNotAllowed);
		
		var project = await _repository.GetAsync(new ProjectSpec(projectId), cancellationToken);

		if (project == null) 
			return ServiceResponse<List<TaskGetDTO>>.FromError(CommonErrors.ProjectNotFound);
        
		var tasks = await _repository.ListAsync(new TaskSpec(projectId, requestingUser.Id), cancellationToken);

		if (tasks.Count == 0) 
			return ServiceResponse<List<TaskGetDTO>>.FromError(CommonErrors.TasksNotFound);
		
		var taskDTOs = tasks.Select(pt => new TaskGetDTO
		{
			Description = pt.Description,
			TaskName = pt.TaskName,
			Status = pt.Status,
		}).ToList();

		return ServiceResponse<List<TaskGetDTO>>.ForSuccess(taskDTOs);	
	}

	public async Task<ServiceResponse> AddProject(ProjectAddDTO project, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
		if (currentUser == null)
			return ServiceResponse.FromError(CommonErrors.UserNotFound);

		if (currentUser.Role != UserRoleEnum.Admin)
			return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);
			
		await _repository.AddAsync(new Project
		{
			ProjectName = project.ProjectName,
			Description = project.Description,
			CreatedByUserId = requestingUser.Id
		}, cancellationToken);
        
		return ServiceResponse.ForSuccess();
	}

	public async Task<ServiceResponse> UpdateProject(ProjectUpdateDTO project, UserDTO requestingUser, CancellationToken cancellationToken = default)
	{
		var membership = await _repository.GetAsync(new ProjectMembershipProjectionSpec(project.ProjectId, requestingUser.Id), cancellationToken);
		if (membership == null)
			return ServiceResponse<List<TaskGetDTO>>.FromError(CommonErrors.AccessNotAllowed);
		
		if (requestingUser.Role != UserRoleEnum.Admin)
			return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);
		
		var oldProject = await _repository.GetAsync(new ProjectSpec(project.ProjectId), cancellationToken);
		if (oldProject == null)
			return ServiceResponse.FromError(CommonErrors.ProjectNotFound);
		
		if (project.Description != null)
			oldProject.Description = project.Description;
		
		if (project.ProjectName != null)
			oldProject.ProjectName = project.ProjectName;
        
		await _repository.UpdateAsync(oldProject, cancellationToken);
        
		return ServiceResponse.ForSuccess();
	}

	public async Task<ServiceResponse> AddMembers(MembersDTO members, UserDTO requestingUser,
		CancellationToken cancellationToken = default)
	{
		var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
		if (currentUser == null)
			return ServiceResponse.FromError(CommonErrors.UserNotFound);

		if (currentUser.Role != UserRoleEnum.Admin)
			return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);
    
		foreach (var userId in members.UserIds)
		{
			await _repository.AddAsync(new ProjectMembership
			{
				ProjectId = members.ProjectId,
				UserId = userId,
			}, cancellationToken);
		}
    
		return ServiceResponse.ForSuccess();
	}
	
	public async Task<ServiceResponse> DeleteMembers(MembersDTO members, UserDTO requestingUser,
		CancellationToken cancellationToken = default)
	{
		var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
		if (currentUser == null)
			return ServiceResponse.FromError(CommonErrors.UserNotFound);

		if (currentUser.Role != UserRoleEnum.Admin)
			return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);

		foreach (var userId in members.UserIds)
		{
			var membershipsToDelete = await _repository.GetAsync(new ProjectMembershipProjectionSpec(members.ProjectId, userId), cancellationToken);
			if (membershipsToDelete == null)
				return ServiceResponse.FromError(CommonErrors.ProjectMemberNotFound);
			
			await _repository.DeleteAsync<Answer>(membershipsToDelete.Id, cancellationToken);
		}

		return ServiceResponse.ForSuccess();
	}

}