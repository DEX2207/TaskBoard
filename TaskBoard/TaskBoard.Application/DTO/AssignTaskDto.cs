namespace TaskBoard.Application.DTO;

public class AssignTaskDto
{
    public int TaskId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
}