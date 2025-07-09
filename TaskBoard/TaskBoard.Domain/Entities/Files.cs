namespace TaskBoard.Domain.Entities;

public class Files
{
    public int Id { get; set; }
    public int SprintId { get; set; }
    public int? TaskId { get; set; }
    public int? UserId { get; set; }
    public string FileName { get; set; }
    public string Path { get; set; }
    public string Type { get; set; }
    public DateTime UploadDate { get; set; }
    
    public Sprint Sprint { get; set; }
    public Tasks? Task { get; set; }
    public User? User { get; set; }
}