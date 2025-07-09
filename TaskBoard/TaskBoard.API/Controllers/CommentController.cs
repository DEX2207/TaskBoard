using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.Application.DTO;
using TaskBoard.Application.Interfaces;

namespace TaskBoard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentController:ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddComment([FromBody] CreateCommentDto comment)
    {
        if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            return Unauthorized("Invalid token payload");
        await _commentService.AddCommentAsync(comment, userId);
        return Ok();
    }

    [HttpGet("task/{taskId}")]
    public async Task<IActionResult> GetCommentForTask(int taskId)
    {
        var comments = await _commentService.GetCommentsByTaskAsync(taskId);
        return Ok(comments);
    }

    [HttpGet("sprint/{sprintId}")]
    public async Task<IActionResult> GetCommentsForSprint(int sprintId)
    {
        var comments=await _commentService.GetCommentsBySprintAsync(sprintId);
        return Ok(comments);
    }
}