namespace TaskMgmt.Application.DTOs;

public record RegisterRequest(string Email, string FullName, string Password);
public record LoginRequest(string Email, string Password);
public record AuthResponse(string Email, string FullName, string Token);
