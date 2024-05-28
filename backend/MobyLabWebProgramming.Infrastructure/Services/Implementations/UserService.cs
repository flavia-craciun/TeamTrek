using System.Net;
using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
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

public class UserService : IUserService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly ILoginService _loginService;
    private readonly IMailService _mailService;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public UserService(IRepository<WebAppDatabaseContext> repository, ILoginService loginService, IMailService mailService)
    {
        _repository = repository;
        _loginService = loginService;
        _mailService = mailService;
    }

    public async Task<ServiceResponse<UserDTO>> GetUser(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new UserProjectionSpec(id), cancellationToken); // Get a user using a specification on the repository.

        return result != null ? 
            ServiceResponse<UserDTO>.ForSuccess(result) : 
            ServiceResponse<UserDTO>.FromError(CommonErrors.UserNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<UserDTO>>> GetUsers(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new UserProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<UserDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<LoginResponseDTO>> Login(LoginDTO login, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new UserSpec(login.Email), cancellationToken);

        if (result == null) // Verify if the user is found in the database.
        {
            return ServiceResponse<LoginResponseDTO>.FromError(CommonErrors.UserNotFound);
        }

        if (result.Password != login.Password)
        {
            return ServiceResponse<LoginResponseDTO>.FromError(new(HttpStatusCode.BadRequest, "Wrong password!", ErrorCodes.WrongPassword));
        }

        var user = new UserDTO
        {
            Id = result.Id,
            Email = result.Email,
            Name = result.Name,
            Role = result.Role,
            TeamId = result.TeamId
        };

        return ServiceResponse<LoginResponseDTO>.ForSuccess(new()
        {
            User = user,
            Token = _loginService.GetToken(user, DateTime.UtcNow, new(7, 0, 0, 0)) // Get a JWT for the user issued now and that expires in 7 days.
        });
    }

    public async Task<ServiceResponse<int>> GetUserCount(CancellationToken cancellationToken = default) => 
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<User>(cancellationToken)); // Get the count of all user entities in the database.

    public async Task<ServiceResponse> AddUser(UserAddDTO user, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        // if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        // {
        //     return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);
        // }

        var result = await _repository.GetAsync(new UserSpec(user.Email), cancellationToken);
        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The user already exists!", ErrorCodes.UserAlreadyExists));
        }
        
        var newUser = new User
        {
            Email = user.Email,
            Name = user.Name,
            Role = user.Role,
            Password = user.Password,
            TeamId = new Guid("1cf9e328-b368-41c7-89b6-bc9378dfddc2")
        };

        await _repository.AddAsync(newUser, cancellationToken);

        await _mailService.SendMail(user.Email, "Welcome!", MailTemplates.UserAddTemplate(user.Name), true, "My App", cancellationToken); // You can send a notification on the user email. Change the email if you want.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateUser(UserUpdateDTO user, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != user.Id) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);
        }

        var entity = await _repository.GetAsync(new UserSpec(user.Id), cancellationToken); 

        if (entity != null)
        {
            entity.Name = user.Name ?? entity.Name;
            entity.Password = user.Password ?? entity.Password;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteUser(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != id) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);
        }

        await _repository.DeleteAsync<User>(id, cancellationToken); // Delete the entity.

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

        var team = await _repository.GetAsync(new TeamSpec(members.TeamName), cancellationToken);
        if (team == null)
            return ServiceResponse.FromError(CommonErrors.TeamNotFound);
        
        foreach (var memberEmail in members.Email)
        {
            var user = await _repository.GetAsync(new UserSpec(memberEmail), cancellationToken);
        
            if (user == null)
                continue;

            user.TeamId = team.Id;
            await _repository.UpdateAsync(user, cancellationToken);
        }
        
        return ServiceResponse.ForSuccess();
    }
    
    public async Task<ServiceResponse> RemoveMembers(MembersDTO members, UserDTO requestingUser,
        CancellationToken cancellationToken = default)
    {
        var currentUser = await _repository.GetAsync(new UserSpec(requestingUser.Id), cancellationToken);
        if (currentUser == null)
            return ServiceResponse.FromError(CommonErrors.UserNotFound);

        if (currentUser.Role != UserRoleEnum.Admin)
            return ServiceResponse.FromError(CommonErrors.AccessNotAllowed);

        var team = await _repository.GetAsync(new TeamSpec(members.TeamName), cancellationToken);
        if (team == null)
            return ServiceResponse.FromError(CommonErrors.TeamNotFound);
        
        foreach (var memberEmail in members.Email)
        {
            var user = await _repository.GetAsync(new UserSpec(memberEmail), cancellationToken);
        
            if (user == null)
                continue;

            user.TeamId = Guid.Empty;
            await _repository.UpdateAsync(user, cancellationToken);
        }
        
        return ServiceResponse.ForSuccess();
    }


}
