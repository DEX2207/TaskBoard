using AutoMapper;
using TaskBoard.Application.DTO;
using TaskBoard.Application.Interfaces;
using TaskBoard.Domain.Entities;
using TaskBoard.Domain.Enum;
using Task = System.Threading.Tasks.Task;

namespace TaskBoard.Application.Services;

public class SprintService:ISprintService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SprintService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<SprintDto> CreateSprintAsync(CreateSprintDto dto, int currentUserId)
    {
        var role = await _unitOfWork.Roles.FindAsync(r =>
            r.ProjectId == dto.ProjectId && r.UserId == currentUserId);

        if (!role.Any())
            throw new UnauthorizedAccessException("Вы не состоите в этом проекте.");

        var userRole = role.First().Roles;

        if (userRole != Roles.Administrator && userRole != Roles.Manager)
            throw new UnauthorizedAccessException("У вас нет прав создавать спринты.");

        var sprint = _mapper.Map<Sprint>(dto);
        await _unitOfWork.Sprints.AddAsync(sprint);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<SprintDto>(sprint);
    }
    public async Task DeleteSprintAsync(int sprintId, int currentUserId)
    {
        var sprint = await _unitOfWork.Sprints.GetByIdAsync(sprintId)
                     ?? throw new KeyNotFoundException("Спринт не найден.");

        var role = await _unitOfWork.Roles.FindAsync(r =>
            r.ProjectId == sprint.ProjectId && r.UserId == currentUserId);

        if (!role.Any())
            throw new UnauthorizedAccessException("Вы не состоите в этом проекте.");

        var userRole = role.First().Roles;
        if (userRole != Roles.Administrator && userRole != Roles.Manager)
            throw new UnauthorizedAccessException("У вас нет прав удалять спринты.");

        _unitOfWork.Sprints.Delete(sprint);
        await _unitOfWork.SaveChangesAsync();
    }
    public async Task<List<SprintDto>> GetSprintsByProjectAsync(int projectId, int currentUserId)
    {
        var role = await _unitOfWork.Roles.FindAsync(r =>
            r.ProjectId == projectId && r.UserId == currentUserId);

        if (!role.Any())
            throw new UnauthorizedAccessException("Вы не состоите в этом проекте.");

        var sprints = await _unitOfWork.Sprints.FindAsync(s => s.ProjectId == projectId);
        return _mapper.Map<List<SprintDto>>(sprints);
    }
    public async Task<List<SprintDto>> GetUserSprintsAsync(int userId)
    {
        var taskAssignments = await _unitOfWork.TaskExecutors.FindAsync(ta => ta.UserId == userId);

        var sprintIds = taskAssignments
            .Select(ta => ta.Task.SprintId)
            .Distinct()
            .ToList();

        if (!sprintIds.Any())
            return new List<SprintDto>();

        var sprints = await _unitOfWork.Sprints.FindAsync(s => sprintIds.Contains(s.Id));
        return _mapper.Map<List<SprintDto>>(sprints);
    }
}