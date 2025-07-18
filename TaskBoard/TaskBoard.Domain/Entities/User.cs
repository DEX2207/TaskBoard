
namespace TaskBoard.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
    
    public ICollection<Files> Files { get; set; }
    public ICollection<Role> Roles { get; set; }
    public ICollection<TaskComment> Comments { get; set; }
    
    public ICollection<TaskExecutor> AssignedTasks { get; set; }
}