using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Application;
using TaskBoard.Application.DTO;

namespace TaskBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController:ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _projectService.CreateProjectAsync(dto, userId);
        return Ok("Проект создан");
    }

    [HttpDelete("{projectId}")]
    [Authorize]
    public async Task<IActionResult> DeleteProject(int projectId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _projectService.DeleteProjectAsync(projectId, userId);
        return Ok("Проект удалён");
    }

    [HttpPost("assign")]
    [Authorize]
    public async Task<IActionResult> AssignUserToProject([FromBody] AssignRoleDto dto)
    {
        var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _projectService.AssignUserToProjectAsync(dto, currentUserId);
        return Ok("Пользователь добавлен");
    }
}