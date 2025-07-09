using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Application.DTO;
using TaskBoard.Application.Interfaces;

namespace TaskBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController: ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _taskService.CreateTaskAsync(dto, userId);
        return Ok("Задача создана");
    }

    [HttpDelete("{taskId}")]
    [Authorize]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _taskService.DeleteTaskAsync(taskId, userId);
        return Ok("Задача удалена");
    }

    [HttpPost("assign")]
    [Authorize]
    public async Task<IActionResult> AssignTask([FromBody] AssignTaskDto dto)
    {
        //var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _taskService.AssignUserToTaskAsync(dto);
        return Ok("Пользователь назначен на задачу");
    }

    [HttpGet("sprint/{sprintId}")]
    [Authorize]
    public async Task<IActionResult> GetTasksBySprint(int sprintId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var tasks = await _taskService.GetTasksForSprintAsync(sprintId, userId);
        return Ok(tasks);
    }
}