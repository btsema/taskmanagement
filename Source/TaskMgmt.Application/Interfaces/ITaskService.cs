using TaskMgmt.Application.DTOs;

namespace TaskMgmt.Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskResponse>> GetUserTasksAsync(Guid userId, int? taskStatusId);
    Task<TaskResponse> GetTaskByIdAsync(Guid userId, Guid taskId);
    Task<TaskResponse> CreateTaskAsync(Guid userId, CreateTaskRequest request);
    Task<TaskResponse> UpdateTaskAsync(Guid userId, Guid taskId, UpdateTaskRequest request);
    Task DeleteTaskAsync(Guid userId, Guid taskId);
}
