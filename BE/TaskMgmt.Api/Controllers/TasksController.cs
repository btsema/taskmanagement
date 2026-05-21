using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMgmt.Application.DTOs;
using TaskMgmt.Application.Interfaces;

namespace TaskMgmt.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    private Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("List")]
    public async Task<ActionResult<IEnumerable<TaskResponse>>> List(GetTasksRequest request)
    {
        return Ok(await _taskService.GetUserTasksAsync(UserId, request.TaskStatusId));
    }

    [HttpGet("Detail/{id}")]
    public async Task<ActionResult<TaskResponse>> GetById(Guid id)
    {
        return Ok(await _taskService.GetTaskByIdAsync(UserId, id));
    }

    [HttpPost("Create")]
    public async Task<ActionResult<TaskResponse>> Create(CreateTaskRequest request)
    {
        var task = await _taskService.CreateTaskAsync(UserId, request);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("Update")]
    public async Task<ActionResult<TaskResponse>> Update(UpdateTaskRequest request)
    {
        return Ok(await _taskService.UpdateTaskAsync(UserId, request.Id, request));
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _taskService.DeleteTaskAsync(UserId, id);
        return NoContent();
    }
}
