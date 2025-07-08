using TaskBoard.Domain.Enum;

namespace TaskBoard.Domain.Entities;

public class Role
{
    public int ProjectId { get; set; }
    public int UserId { get; set; }
    public Roles Roles { get; set; }
    
    public Project Project { get; set; }
    public User User { get; set; }
}