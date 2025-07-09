using TaskBoard.Application.DTO;

namespace TaskBoard.Application;

public interface IUserService
{
    Task InitRegistrationAsync(CreateUserDto dto);

    Task ConfirmRegistrationAsync(string email, string code);
    
    Task<LoginResponseDto> LoginAsync(string email, string password);
    
    Task InitPasswordResetAsync(string email);

    Task ConfirmPasswordResetAsync(string email, string code, string newPassword);

    Task<UserDto?> GetUserByIdAsync(int id);

    Task<UserDto?> GetUserByEmailAsync(string email);
}