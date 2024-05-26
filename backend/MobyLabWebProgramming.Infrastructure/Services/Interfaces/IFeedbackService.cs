using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.DataTransferObjects.FeedbackAPI;
using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IFeedbackService
{
    public Task<ServiceResponse<FeedbackDTO>> GetFeedback(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse> AddFeedback(FeedbackAddDTO feedback, UserDTO requestingUser, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateFeedback(FeedbackUpdateDTO feedback, UserDTO requestingUser, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteFeedback(Guid id, UserDTO requestingUser, CancellationToken cancellationToken = default);
}
