namespace TaskBoard.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public ICollection<Sprint> Sprints { get; set; }
    public ICollection<Role> Roles { get; set; }
}