namespace TaskBoard.Domain.Entities;

public class TaskComment
{
    public int Id { get; set; }
    public int SprintId { get; set; }
    public int? UserId { get; set; }
    public int? TaskId { get; set; }
    public string Comment { get; set; }
    public DateTime CommentDate { get; set; }
    
    public Sprint Sprint { get; set; }
    public User? User { get; set; }
    public Tasks? Task { get; set; }
}