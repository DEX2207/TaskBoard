namespace TaskBoard.Application.DTO;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
}