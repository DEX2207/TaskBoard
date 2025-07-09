using TaskBoard.Application.DTO;

namespace TaskBoard.Application.Interfaces;

public interface ITaskService
{
    Task<TaskDto> CreateTaskAsync(CreateTaskDto dto);
    Task DeleteTaskAsync(int taskId);
    Task AssignUserToTaskAsync(AssignTaskDto dto);
    Task<List<TaskDto>> GetUserTasksInSprintAsync(int sprintId, int userId);
    Task<TaskDto?> GetTaskByIdAsync(int taskId);
    Task<List<TaskDto>> GetTasksForSprintAsync(int sprintId);
}