using AutoMapper;
using TaskBoard.Application.DTO;
using TaskBoard.Application.Interfaces;
using TaskBoard.Domain.Entities;
using TaskBoard.Domain.Enum;
using Task = System.Threading.Tasks.Task;

namespace TaskBoard.Application.Services;

public class ProjectService: IProjectService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto dto, int userId)
    {
        var project = _mapper.Map<Project>(dto);
        await _unitOfWork.Projects.AddAsync(project);
        await _unitOfWork.SaveChangesAsync();

        var role = new Role
        {
            ProjectId = project.Id,
            UserId = userId,
            Roles = Roles.Administrator
        };

        await _unitOfWork.Roles.AddAsync(role);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProjectDto>(project);
    }

    public async Task DeleteProjectAsync(int projectId, int userId)
    {
        var role = await _unitOfWork.Roles.FindAsync(r => r.ProjectId == projectId && r.UserId == userId);
        if (!role.Any() || role.First().Roles != Roles.Administrator)
            throw new UnauthorizedAccessException("Только администратор может удалить проект");

        var project = await _unitOfWork.Projects.GetByIdAsync(projectId);
        if (project is null)
            throw new KeyNotFoundException("Проект не найден");

        _unitOfWork.Projects.Delete(project);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ProjectDto?> GetProjectByIdAsync(int projectId)
    {
        var project = await _unitOfWork.Projects.GetByIdAsync(projectId);
        return project is null ? null : _mapper.Map<ProjectDto>(project);
    }

    public async Task<List<ProjectDto>> GetProjectsByUserAsync(int userId)
    {
        var roles = await _unitOfWork.Roles.FindAsync(r => r.UserId == userId);
        var projectIds = roles.Select(r => r.ProjectId).Distinct();

        var projects = await _unitOfWork.Projects.FindAsync(p => projectIds.Contains(p.Id));
        return projects.Select(_mapper.Map<ProjectDto>).ToList();
    }

    public async Task AssignUserToProjectAsync(AssignRoleDto dto, int currentUserId)
    {
        var currentUserRole = await _unitOfWork.Roles.FindAsync(r =>
            r.ProjectId == dto.ProjectId && r.UserId == currentUserId);

        if (!currentUserRole.Any())
            throw new UnauthorizedAccessException("Вы не состоите в проекте.");

        var isAdmin = currentUserRole.First().Roles == Roles.Administrator;
        var isManager = currentUserRole.First().Roles == Roles.Manager;

        if (!isAdmin && !isManager)
            throw new UnauthorizedAccessException("У вас нет прав добавлять пользователей в проект.");

        IQueryable<User> userQuery = _unitOfWork.Users.FindAsync(u =>
            (!string.IsNullOrEmpty(dto.Email) && u.Email == dto.Email) ||
            (!string.IsNullOrEmpty(dto.UserName) && u.UserName == dto.UserName)).Result.AsQueryable();

        var targetUser = userQuery.FirstOrDefault();
        if (targetUser == null)
            throw new KeyNotFoundException("Пользователь не найден.");
        
        var existingRole = await _unitOfWork.Roles.FindAsync(r =>
            r.ProjectId == dto.ProjectId && r.UserId == targetUser.Id);

        if (existingRole.Any())
        {
            if (isManager)
                throw new InvalidOperationException("Пользователь уже состоит в проекте. Менеджер не может изменить его роль.");

            if (isAdmin)
            {
                var role = existingRole.First();
                role.Roles = dto.Role;
                _unitOfWork.Roles.Update(role);
            }
        }
        else
        {
            var assignedRole = isManager ? Roles.User : dto.Role;

            var newRole = new Role
            {
                ProjectId = dto.ProjectId,
                UserId = targetUser.Id,
                Roles = assignedRole
            };

            await _unitOfWork.Roles.AddAsync(newRole);
        }

        await _unitOfWork.SaveChangesAsync();
    }
}