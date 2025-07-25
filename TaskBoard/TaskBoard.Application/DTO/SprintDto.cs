namespace TaskBoard.Application.DTO;

public class SprintDto
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}