using AutoMapper;
using TaskBoard.Application.DTO;
using TaskBoard.Application.Interfaces;
using TaskBoard.Domain.Entities;
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
    public async Task AssignUserToTaskAsync(AssignTaskDto dto)
    {
        var user = await _unitOfWork.Users.FindAsync(u =>
            (dto.UserName != null && u.UserName == dto.UserName) ||
            (dto.Email != null && u.Email == dto.Email));

        var targetUser = user.FirstOrDefault();
        if (targetUser == null)
            throw new Exception("User not found");

        var task = await _unitOfWork.Tasks.GetByIdAsync(dto.TaskId);
        if (task == null)
            throw new Exception("Task not found");

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
}