using AutoMapper;
using TaskBoard.Application.DTO;
using TaskBoard.Application.Interfaces;
using TaskBoard.Domain.Entities;

namespace TaskBoard.Application.Services;

public class CommentService: ICommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CommentDto> AddCommentAsync(CreateCommentDto dto, int currentUserId)
    {
        var entity = _mapper.Map<TaskComment>(dto);
        entity.UserId = currentUserId;
        entity.CommentDate = DateTime.UtcNow;

        await _unitOfWork.Comments.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<CommentDto>(entity);
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsBySprintAsync(int sprintId)
    {
        var comments = await _unitOfWork.Comments.FindAsync(c => c.SprintId == sprintId);
        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }

    public async Task<IEnumerable<CommentDto>> GetCommentsByTaskAsync(int taskId)
    {
        var comments = await _unitOfWork.Comments.FindAsync(c => c.TaskId == taskId);
        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }
}