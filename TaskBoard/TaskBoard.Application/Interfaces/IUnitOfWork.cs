using TaskBoard.Domain.Entities;
using TaskBoard.Domain.Interfaces;

namespace TaskBoard.Application.Interfaces;

public interface IUnitOfWork:IAsyncDisposable
{
    IRepository<Project> Projects { get; }
    IRepository<Sprint> Sprints { get; }
    IRepository<Tasks> Tasks { get; }
    IRepository<User> Users { get; }
    
    IRepository<TaskComment> Comments { get; }
    IRepository<Files> Files { get; }
    IRepository<Role> Roles { get; }
    IRepository<TaskExecutor> TaskExecutors { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}