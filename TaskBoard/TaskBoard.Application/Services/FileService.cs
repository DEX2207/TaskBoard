using AutoMapper;
using TaskBoard.Application.DTO;
using TaskBoard.Application.Interfaces;
using TaskBoard.Domain.Entities;

namespace TaskBoard.Application.Services;

public class FileService: IFileService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorage _fileStorage;

    public FileService(IUnitOfWork unitOfWork, IMapper mapper, IFileStorage fileStorage)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorage = fileStorage;
    }

    public async Task<FileDto> UploadFileAsync(CreateFileDto dto)
    {
        var path = await _fileStorage.SaveFileAsync(dto.File, "uploads");

        var fileEntity = new Files
        {
            SprintId = dto.SprintId,
            TaskId = dto.TaskId,
            UserId = dto.UserId,
            FileName = dto.File.FileName,
            Path = path,
            Type = Path.GetExtension(dto.File.FileName),
            UploadDate = DateTime.UtcNow
        };

        await _unitOfWork.Files.AddAsync(fileEntity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<FileDto>(fileEntity);
    }

    public async Task DeleteFileAsync(int fileId)
    {
        var file = await _unitOfWork.Files.GetByIdAsync(fileId);
        if (file == null) throw new Exception("Файл не найден");

        await _fileStorage.DeleteFileAsync(file.Path);
        _unitOfWork.Files.Delete(file);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<FileDto>> GetFilesBySprintIdAsync(int sprintId)
    {
        var files = await _unitOfWork.Files.FindAsync(f => f.SprintId == sprintId);
        return _mapper.Map<IEnumerable<FileDto>>(files);
    }

    public async Task<IEnumerable<FileDto>> GetFilesByTaskIdAsync(int taskId)
    {
        var files = await _unitOfWork.Files.FindAsync(f => f.TaskId == taskId);
        return _mapper.Map<IEnumerable<FileDto>>(files);
    }
}