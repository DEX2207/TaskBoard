namespace TaskBoard.Application.DTO;

public class CreateUserDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public DateTime RegistrationDate { get; set; }
}