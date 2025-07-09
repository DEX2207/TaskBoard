using TaskStatus = TaskBoard.Domain.Enum.TaskStatus;

namespace TaskBoard.Application.DTO;

public class CreateTaskDto
{
    public int SprintId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
}