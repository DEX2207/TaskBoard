using System.Security.Cryptography;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using TaskBoard.Application.DTO;
using TaskBoard.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskBoard.Application.Services;

public class UserService: IUserService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private readonly IEmailSendler _emailSendler;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    public UserService(IUnitOfWork uow, IMapper mapper, IMemoryCache cache, IEmailSendler emailSender, IJwtTokenGenerator jwtTokenGenerator)
    {
        _uow = uow;
        _mapper = mapper;
        _cache = cache;
        _emailSendler = emailSender;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task InitRegistrationAsync(CreateUserDto dto)
    {
        var existingUser = await _uow.Users.FindAsync(u => u.Email == dto.Email);
        if (existingUser.Any())
            throw new InvalidOperationException("Пользователь с таким email уже существует");

        var code = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        
        var cacheEntry = new CachedRegistration
        {
            UserName = dto.UserName,
            Email = dto.Email,
            PasswordHash = passwordHash,
            Code = code,
            ExpirationTime = DateTime.UtcNow.AddMinutes(15)
        };

        _cache.Set(dto.Email, cacheEntry, TimeSpan.FromMinutes(15));
        
        await _emailSendler.SendAsync(dto.Email, code);
    }
    
    public async Task ConfirmRegistrationAsync(string email, string code)
    {
        if (!_cache.TryGetValue(email, out PendingRegistration? pending))
            throw new Exception("Код устарел или не найден");

        if (pending.Code != code)
            throw new Exception("Неверный код подтверждения");

        // Создание пользователя
        var user = _mapper.Map<User>(pending.UserDto);
        user.Password = BCrypt.Net.BCrypt.HashPassword(pending.UserDto.Password);
        user.RegistrationDate = DateTime.UtcNow;

        await _uow.Users.AddAsync(user);
        await _uow.SaveChangesAsync();

        _cache.Remove(email);
    }
    public async Task<LoginResponseDto> LoginAsync(string email, string password)
    {
        var user = await _uow.Users.FindAsync(u => u.Email == email);

        if (user == null || !user.Any())
            throw new UnauthorizedAccessException("Пользователь не найден.");

        var existingUser = user.First();

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, existingUser.Password);

        if (!isPasswordValid)
            throw new UnauthorizedAccessException("Неверный пароль.");

        var role = await _uow.Roles
            .FindAsync(r => r.UserId == existingUser.Id);

        string userRole = role.FirstOrDefault()?.Roles.ToString() ?? "user";

        var token = _jwtTokenGenerator.GenerateToken(existingUser.Id, existingUser.Email, userRole);

        return new LoginResponseDto
        {
            Token = token,
            Email = existingUser.Email,
            UserName = existingUser.UserName,
            Role = userRole
        };
    }
    public async Task InitPasswordResetAsync(string email)
    {
        var user = await _uow.Users.FindAsync(u => u.Email == email);
        if (user is null || !user.Any())
            throw new Exception("Пользователь с таким email не найден");

        string code = new Random().Next(100000, 999999).ToString();

        _cache.Set($"password_reset_{email}", code, TimeSpan.FromMinutes(10));

        await _emailSendler.SendAsync(email, code);
    }
    public async Task ConfirmPasswordResetAsync(string email, string code, string newPassword)
    {
        if (!_cache.TryGetValue($"password_reset_{email}", out string? cachedCode) || cachedCode != code)
            throw new Exception("Неверный или просроченный код подтверждения");

        var user = (await _uow.Users.FindAsync(u => u.Email == email)).FirstOrDefault();
        if (user is null)
            throw new Exception("Пользователь не найден");

        user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);

        _uow.Users.Update(user);
        await _uow.SaveChangesAsync();

        _cache.Remove($"password_reset_{email}");
    }
    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _uow.Users.GetByIdAsync(id);
        return user is null ? null : _mapper.Map<UserDto>(user);
    }
    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = (await _uow.Users.FindAsync(u => u.Email == email)).FirstOrDefault();
        return user is null ? null : _mapper.Map<UserDto>(user);
    }
}
internal class CachedRegistration
{
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string Code { get; set; }
    public DateTime ExpirationTime { get; set; }
}
public class PendingRegistration
{
    public string Code { get; set; }
    public CreateUserDto UserDto { get; set; }
}