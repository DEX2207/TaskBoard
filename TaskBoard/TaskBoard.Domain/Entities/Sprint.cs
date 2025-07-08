namespace TaskBoard.Domain.Entities;

public class Sprint
{
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public Project Project { get; set; }
    public ICollection<Task> Tasks { get; set; }
    public ICollection<TaskComment> Comments { get; set; }
    public ICollection<Files> Files { get; set; }
}