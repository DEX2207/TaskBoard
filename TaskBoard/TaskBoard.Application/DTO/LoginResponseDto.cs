namespace TaskBoard.Application.DTO;

public class LoginResponseDto
{
    public string Token { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}