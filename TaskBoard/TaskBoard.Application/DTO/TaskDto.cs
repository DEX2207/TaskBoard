using TaskStatus = TaskBoard.Domain.Enum.TaskStatus;

namespace TaskBoard.Application.DTO;

public class TaskDto
{
    public int Id { get; set; }
    public int SprintId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
}