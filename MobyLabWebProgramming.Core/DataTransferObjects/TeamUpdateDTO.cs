namespace MobyLabWebProgramming.Core.DataTransferObjects;
public record TeamUpdateDTO(Guid TeamId, string? TeamName = default, UserDTO? TeamLeader = default);