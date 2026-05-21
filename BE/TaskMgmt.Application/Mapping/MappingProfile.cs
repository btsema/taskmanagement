using AutoMapper;
using TaskMgmt.Application.DTOs;
using TaskMgmt.Domain.Entities;

namespace TaskMgmt.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, AuthResponse>()
            .ForCtorParam("Token", opt => opt.MapFrom(_ => string.Empty));
        
        CreateMap<TaskItem, TaskResponse>();
        CreateMap<CreateTaskRequest, TaskItem>();
        CreateMap<UpdateTaskRequest, TaskItem>();
    }
}
