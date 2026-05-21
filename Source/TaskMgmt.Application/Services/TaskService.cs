using AutoMapper;
using TaskMgmt.Application.DTOs;
using TaskMgmt.Application.Exceptions;
using TaskMgmt.Application.Interfaces;
using TaskMgmt.Domain.Entities;

namespace TaskMgmt.Application.Services;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskResponse>> GetUserTasksAsync(Guid userId, int? taskStatusId)
    {
        var tasks = await _unitOfWork.Repository<TaskItem>().FindAsync(t => t.UserId == userId);
        if (taskStatusId.HasValue)
            tasks = tasks.Where(t => (int)t.Status == taskStatusId);

        return _mapper.Map<IEnumerable<TaskResponse>>(tasks);
    }

    public async Task<TaskResponse> GetTaskByIdAsync(Guid userId, Guid taskId)
    {
        var task = await GetTaskAndValidateOwnership(userId, taskId);
        return _mapper.Map<TaskResponse>(task);
    }

    public async Task<TaskResponse> CreateTaskAsync(Guid userId, CreateTaskRequest request)
    {
        var task = _mapper.Map<TaskItem>(request);
        task.UserId = userId;

        await _unitOfWork.Repository<TaskItem>().AddAsync(task);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TaskResponse>(task);
    }

    public async Task<TaskResponse> UpdateTaskAsync(Guid userId, Guid taskId, UpdateTaskRequest request)
    {
        var task = await GetTaskAndValidateOwnership(userId, taskId);

        _mapper.Map(request, task);
        task.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Repository<TaskItem>().Update(task);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<TaskResponse>(task);
    }

    public async Task DeleteTaskAsync(Guid userId, Guid taskId)
    {
        var task = await GetTaskAndValidateOwnership(userId, taskId);
        _unitOfWork.Repository<TaskItem>().Remove(task);
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task<TaskItem> GetTaskAndValidateOwnership(Guid userId, Guid taskId)
    {
        var task = await _unitOfWork.Repository<TaskItem>().GetByIdAsync(taskId);

        if (task == null)
            throw new NotFoundException(nameof(TaskItem), taskId);

        if (task.UserId != userId)
            throw new UnauthorizedException("You do not have permission to access this task.");

        return task;
    }
}
