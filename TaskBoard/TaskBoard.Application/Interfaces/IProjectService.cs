using TaskBoard.Application.DTO;

namespace TaskBoard.Application;

public interface IProjectService
{
    Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto, int userId);
    Task DeleteProjectAsync(int projectId, int userId);
    Task<ProjectDto?> GetProjectByIdAsync(int projectId);
    Task<List<ProjectDto>> GetProjectsByUserAsync(int userId);
    
    Task AssignUserToProjectAsync(AssignRoleDto dto, int currentUserId);
}