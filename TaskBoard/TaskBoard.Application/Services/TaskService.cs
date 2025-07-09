using AutoMapper;
using TaskBoard.Application.DTO;
using TaskBoard.Application.Interfaces;
using TaskBoard.Domain.Entities;
using TaskBoard.Domain.Enum;
using Task = System.Threading.Tasks.Task;

namespace TaskBoard.Application.Services;

public class TaskService:ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<TaskDto> CreateTaskAsync(CreateTaskDto dto,int currentUserId)
    {
        var TaskSprint=await _unitOfWork.Sprints.FindAsync(s => s.Id == dto.SprintId);
        var currentSprint=TaskSprint.FirstOrDefault();
        var role = await _unitOfWork.Roles.FindAsync(r =>
            r.ProjectId == currentSprint.ProjectId && r.UserId == currentUserId);
        var isAdmin = role.First().Roles == Roles.Administrator;
        var isManager = role.First().Roles == Roles.Manager;
        if (!isAdmin && !isManager)
            throw new UnauthorizedAccessException("Только админ или менеджер может задавать задачи");

        var sprint = await _unitOfWork.Sprints.GetByIdAsync(dto.SprintId);
        if (sprint == null)
            throw new Exception("Спринт не найден");

        var task = _mapper.Map<Tasks>(dto);
        await _unitOfWork.Tasks.AddAsync(task);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TaskDto>(task);
    }
    public async Task DeleteTaskAsync(int taskId,int currentUserId)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(taskId);
        if (task == null)
            throw new Exception("Задача не найдена");
        var TaskSprint=await _unitOfWork.Sprints.FindAsync(s => s.Id == task.SprintId);
        var currentSprint=TaskSprint.FirstOrDefault();
        var role = await _unitOfWork.Roles.FindAsync(r =>
            r.ProjectId == currentSprint.ProjectId && r.UserId == currentUserId);
        var isAdmin = role.First().Roles == Roles.Administrator;
        var isManager = role.First().Roles == Roles.Manager;
        if (!isAdmin && !isManager)
            throw new UnauthorizedAccessException("Только администратор и модератор могут удалять задачи");
        _unitOfWork.Tasks.Delete(task);
        await _unitOfWork.SaveChangesAsync();
    }
    public async Task AssignUserToTaskAsync(AssignTaskDto dto)
    {
        var user = await _unitOfWork.Users.FindAsync(u =>
            (dto.UserName != null && u.UserName == dto.UserName) ||
            (dto.Email != null && u.Email == dto.Email));

        var targetUser = user.FirstOrDefault();
        if (targetUser == null)
            throw new Exception("Пользователь не найден");

        var task = await _unitOfWork.Tasks.GetByIdAsync(dto.TaskId);
        if (task == null)
            throw new Exception("Задача не найдена");

        var alreadyAssigned = await _unitOfWork.TaskExecutors.FindAsync(ta =>
            ta.TaskId == task.Id && ta.UserId == targetUser.Id);

        if (!alreadyAssigned.Any())
        {
            await _unitOfWork.TaskExecutors.AddAsync(new TaskExecutor
            {
                TaskId = task.Id,
                UserId = targetUser.Id
            });
        }

        await _unitOfWork.SaveChangesAsync();
    }
    public async Task<List<TaskDto>> GetUserTasksInSprintAsync(int sprintId, int userId)
    {
        var sprint = await _unitOfWork.Sprints.GetByIdAsync(sprintId);
        if (sprint == null || !await IsUserInProject(sprint.ProjectId, userId))
            throw new UnauthorizedAccessException("Вы не авторизованы в этом спринте");

        var tasks = await _unitOfWork.Tasks.FindAsync(t => t.SprintId == sprintId);
        var userTasks = tasks.Where(t => t.Executors.Any(te => te.UserId == userId))
            .ToList();

        return _mapper.Map<List<TaskDto>>(userTasks);
    }
    public async Task<List<TaskDto>> GetTasksForSprintAsync(int sprintId,int currentUserId)
    {
        var sprint = await _unitOfWork.Sprints.GetByIdAsync(sprintId);
        if (sprint == null)
            throw new UnauthorizedAccessException("Спринт не найден");
        var currentRole = await _unitOfWork.Roles.FindAsync(r => r.ProjectId == sprint.ProjectId && r.UserId == currentUserId);
        var isAdmin = currentRole.First().Roles == Roles.Administrator;
        var isManager = currentRole.First().Roles == Roles.Manager;
        if (!isAdmin && !isManager)
            throw new UnauthorizedAccessException("Только администратор и менеджер может просматривать все задачи в спринте");

        var tasks = await _unitOfWork.Tasks.FindAsync(t => t.SprintId == sprintId);
        return _mapper.Map<List<TaskDto>>(tasks);
    }
    private async Task<bool> IsUserInProject(int projectId, int userId)
    {
        var projectUsers = await _unitOfWork.Roles.FindAsync(r => r.ProjectId == projectId && r.UserId == userId);
        return projectUsers.Any();
    }
}