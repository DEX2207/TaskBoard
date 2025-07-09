using TaskBoard.Application.DTO;

namespace TaskBoard.Application.Interfaces;

public interface ICommentService
{
    Task<CommentDto> AddCommentAsync(CreateCommentDto dto, int currentUserId);
    Task<IEnumerable<CommentDto>> GetCommentsBySprintAsync(int sprintId);
    Task<IEnumerable<CommentDto>> GetCommentsByTaskAsync(int taskId);
}