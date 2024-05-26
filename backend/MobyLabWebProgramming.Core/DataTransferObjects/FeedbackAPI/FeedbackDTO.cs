using MobyLabWebProgramming.Core.DataTransferObjects.QuestionAPI;
using MobyLabWebProgramming.Core.DataTransferObjects.UserAPI;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class FeedbackDTO
{
    public Guid FeedbackId { get; set; }
    public int Rating { get; set; } = default!;
    public string FrequentedSection { get; set; } = default!;
    public string Suggestion { get; set; } = default!;
    public int ResponseWanted { get; set; } = default!;
    public UserDTO GivenBy { get; set; } = default!;

}
