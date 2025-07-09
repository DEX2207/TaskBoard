namespace TaskBoard.Domain.Entities;

public class TaskExecutor
{
    public int TaskId { get; set; }
    public int UserId { get; set; }
    
    public Tasks Tasks { get; set; }
    public User User { get; set; }
}