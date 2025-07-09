using TaskBoard.Application.DTO;

namespace TaskBoard.Application.Interfaces;

public interface ITaskService
{
    Task<TaskDto> CreateTaskAsync(CreateTaskDto dto,int currentUserId);
    Task DeleteTaskAsync(int taskId,int currentUserId);
    Task AssignUserToTaskAsync(AssignTaskDto dto);
    Task<List<TaskDto>> GetUserTasksInSprintAsync(int sprintId, int userId);
    Task<List<TaskDto>> GetTasksForSprintAsync(int sprintId,int currentUserId);
}