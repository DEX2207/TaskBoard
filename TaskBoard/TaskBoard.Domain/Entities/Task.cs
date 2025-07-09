using TaskStatus = TaskBoard.Domain.Enum.TaskStatus;

namespace TaskBoard.Domain.Entities;

public class Task
{
    public int Id { get; set; }
    public int SprintId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    
    public Sprint Sprint { get; set; }
    public ICollection<Files> Files { get; set; }
    public ICollection<TaskComment> Comments { get; set; }
    
    public ICollection<TaskExecutor> Executors { get; set; }
}