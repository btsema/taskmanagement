using TaskMgmt.Domain.Enums;
using TaskStatus = TaskMgmt.Domain.Enums.TaskStatus;

namespace TaskMgmt.Domain.Entities;

public class TaskItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public TaskStatus Status { get; set; } = TaskStatus.Todo;
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}
