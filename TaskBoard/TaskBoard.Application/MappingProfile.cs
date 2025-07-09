using AutoMapper;
using TaskBoard.Application.DTO;
using TaskBoard.Domain.Entities;
using Task = TaskBoard.Domain.Entities.Task;

namespace TaskBoard.Application;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Project, ProjectDto>();
        CreateMap<CreateProjectDto,Project>()
            .ForMember(p=>p.Id,o=>o.Ignore());
        
        CreateMap<Sprint, SprintDto>();
        CreateMap<CreateSprintDto,Sprint>()
            .ForMember(s=>s.Id,o=>o.Ignore());

        CreateMap<Task, TaskDto>();
        CreateMap<CreateTaskDto,Task>()
            .ForMember(t=>t.Id,o=>o.Ignore());
        
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>()
            .ForMember(u => u.Id, o => o.Ignore());

        CreateMap<TaskComment, CommentDto>();
        CreateMap<CreateCommentDto,TaskComment>()
            .ForMember(c => c.Id, o => o.Ignore())
            .ForMember(c => c.TaskId, o => o.Ignore())
            .ForMember(c => c.UserId, o => o.Ignore())
            .ForMember(c => c.SprintId, o => o.Ignore())
            .ForMember(c => c.CommentDate, o => o.MapFrom(src=>DateTime.UtcNow));

        CreateMap<Files, FileDto>();
        CreateMap<CreateFileDto,Files>()
            .ForMember(f=>f.Id,o=>o.Ignore())
            .ForMember(f=>f.TaskId, o=>o.Ignore())
            .ForMember(f=>f.UserId, o=>o.Ignore())
            .ForMember(f=>f.SprintId, o=>o.Ignore())
            .ForMember(f=>f.Path, o=>o.Ignore())
            .ForMember(f=>f.Type, o=>o.Ignore())
            .ForMember(f=>f.UploadDate, o=>o.MapFrom(src=>DateTime.UtcNow))
            .ForMember(f=>f.FileName, o=>o.MapFrom(src=> src.File.FileName));
    }
}