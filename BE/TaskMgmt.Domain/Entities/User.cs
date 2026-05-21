namespace TaskMgmt.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    
    // Navigation Property
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
