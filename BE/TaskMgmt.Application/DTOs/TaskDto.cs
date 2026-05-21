using TaskMgmt.Domain.Enums;
using TaskStatus = TaskMgmt.Domain.Enums.TaskStatus;

namespace TaskMgmt.Application.DTOs;

public record TaskResponse(
    Guid Id, 
    string Title, 
    string Description, 
    DateTime? DueDate, 
    Priority Priority, 
    TaskStatus Status, 
    DateTime CreatedAt
);

public record CreateTaskRequest(
    string Title, 
    string Description,
    TaskStatus Status,
    DateTime? DueDate, 
    Priority Priority
);

public record UpdateTaskRequest(
    Guid Id,
    string Title, 
    string Description, 
    DateTime? DueDate, 
    Priority? Priority, 
    TaskStatus? Status
);

public record GetTasksRequest(
    int? TaskStatusId
);