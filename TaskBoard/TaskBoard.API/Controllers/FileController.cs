using Microsoft.AspNetCore.Mvc;
using TaskBoard.Application.DTO;
using TaskBoard.Application.Interfaces;

namespace TaskBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController:ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(CreateFileDto dto)
    {
        var uploadedFile = await _fileService.UploadFileAsync(dto);
        return Ok(uploadedFile);
    }

    [HttpGet("task/{taskId}")]
    public async Task<IActionResult> GetFilesForTask(int taskId)
    {
        var files = await _fileService.GetFilesByTaskIdAsync(taskId);
        return Ok(files);
    }

    [HttpGet("sprint/{sprintId}")]
    public async Task<IActionResult> GetFilesForSprint(int sprintId)
    {
        var files = await _fileService.GetFilesBySprintIdAsync(sprintId);
        return Ok(files);
    }
}