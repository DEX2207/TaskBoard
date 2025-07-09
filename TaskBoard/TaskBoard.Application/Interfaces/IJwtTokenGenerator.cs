namespace TaskBoard.Application;

public interface IJwtTokenGenerator
{
    string GenerateToken(int userId, string email, string role);
}