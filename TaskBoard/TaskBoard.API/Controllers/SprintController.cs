using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Application.DTO;
using TaskBoard.Application.Interfaces;

namespace TaskBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SprintController: ControllerBase
{
    private readonly ISprintService _sprintService;

    public SprintController(ISprintService sprintService)
    {
        _sprintService = sprintService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateSprint([FromBody] CreateSprintDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _sprintService.CreateSprintAsync(dto, userId);
        return Ok("Спринт создан");
    }

    [HttpDelete("{sprintId}")]
    [Authorize]
    public async Task<IActionResult> DeleteSprint(int sprintId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _sprintService.DeleteSprintAsync(sprintId, userId);
        return Ok("Спринт удалён");
    }

    [HttpGet("project/{projectId}")]
    [Authorize]
    public async Task<IActionResult> GetSprintsByProject(int projectId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var sprints = await _sprintService.GetSprintsByProjectAsync(projectId, userId);
        return Ok(sprints);
    }
}