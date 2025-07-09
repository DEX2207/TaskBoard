using TaskBoard.Application.DTO;

namespace TaskBoard.Application.Interfaces;

public interface ISprintService
{
    Task<SprintDto> CreateSprintAsync(CreateSprintDto dto, int currentUserId);
    Task DeleteSprintAsync(int sprintId, int currentUserId);
    Task<List<SprintDto>> GetSprintsByProjectAsync(int projectId, int currentUserId);
    Task<List<SprintDto>> GetUserSprintsAsync(int userId);
}