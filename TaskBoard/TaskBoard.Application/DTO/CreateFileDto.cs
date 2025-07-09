using Microsoft.AspNetCore.Http;

namespace TaskBoard.Application.DTO;

public class CreateFileDto
{
    public IFormFile File { get; set; }
    public int SprintId { get; set; } 
    public int? TaskId { get; set; }    
    public int? UserId { get; set; }
}