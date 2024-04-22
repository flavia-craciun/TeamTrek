namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

public interface IAnswerService
{

    public Task<ServiceResponse<AnswerDTO>> GetAnswer(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse<int>> GetAnswerCount(CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddAnswer(AnswerAddDTO answer, UserDTO requestingUser, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateAnswer(AnswerUpdateDTO answer, UserDTO requestingUser, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteAnswer(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
}
