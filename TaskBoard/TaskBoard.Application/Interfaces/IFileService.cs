using TaskBoard.Application.DTO;

namespace TaskBoard.Application.Interfaces;

public interface IFileService
{
    Task<FileDto> UploadFileAsync(CreateFileDto dto);
    Task DeleteFileAsync(int fileId);
    Task<IEnumerable<FileDto>> GetFilesBySprintIdAsync(int sprintId);
    Task<IEnumerable<FileDto>> GetFilesByTaskIdAsync(int taskId);
}