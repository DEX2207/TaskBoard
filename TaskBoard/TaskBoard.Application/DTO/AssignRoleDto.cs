using TaskBoard.Domain.Enum;

namespace TaskBoard.Application.DTO;

public class AssignRoleDto
{
    public int ProjectId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public Roles Role { get; set; }
}