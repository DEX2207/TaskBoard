namespace TaskBoard.Application.DTO;

public class CommentDto
{
    public int Id { get; set; }
    public int SprintId { get; set; }
    public int? UserId { get; set; }
    public int? TaskId { get; set; }
    public string Comment { get; set; }
    public DateTime CommentDate { get; set; }
}