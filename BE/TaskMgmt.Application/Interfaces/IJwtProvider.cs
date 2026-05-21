using TaskMgmt.Domain.Entities;

namespace TaskMgmt.Application.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(User user);
}
