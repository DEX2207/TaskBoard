namespace TaskBoard.Application.DTO;

public class CreateCommentDto
{
    public int SprintId { get; set; }
    public int? UserId { get; set; }
    public int? TaskId { get; set; }
    public string Comment { get; set; }
}