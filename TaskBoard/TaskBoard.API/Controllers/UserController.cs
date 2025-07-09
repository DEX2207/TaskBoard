using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Application;
using TaskBoard.Application.DTO;

namespace TaskBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController:ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register/init")]
    public async Task<IActionResult> InitRegistration([FromBody] CreateUserDto dto)
    {
        await _userService.InitRegistrationAsync(dto);
        return Ok("Код отправлен на email");
    }

    [HttpPost("register/confirm")]
    public async Task<IActionResult> ConfirmRegistration([FromBody] ConfirmRegistrationDto dto)
    {
        await _userService.ConfirmRegistrationAsync(dto.Email, dto.Code);
        return Ok("Регистрация подтверждена");
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto dto)
    {
        var response = await _userService.LoginAsync(dto.UserName, dto.Password);
        return Ok(response);
    }

    [HttpPost("reset/init")]
    public async Task<IActionResult> InitPasswordReset([FromBody] string email)
    {
        await _userService.InitPasswordResetAsync(email);
        return Ok("Код для сброса пароля отправлен");
    }

    [HttpPost("reset/confirm")]
    public async Task<IActionResult> ConfirmPasswordReset([FromBody] string email, string code, string newPassword)
    {
        await _userService.ConfirmPasswordResetAsync( email, code, newPassword);
        return Ok("Пароль изменён");
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _userService.GetUserByIdAsync(userId);
        return Ok(user);
    }
}