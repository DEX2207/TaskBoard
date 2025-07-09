using TaskBoard.Domain.Entities;
using TaskBoard.Domain.Interfaces;
using Task = TaskBoard.Domain.Entities.Task;

namespace TaskBoard.Application;

public interface IUnitOfWork:IAsyncDisposable
{
    IRepository<Project> Projects { get; }
    IRepository<Sprint> Sprints { get; }
    IRepository<Task> Tasks { get; }
    IRepository<User> Users { get; }
    
    IRepository<TaskComment> Comments { get; }
    IRepository<Files> Files { get; }
    IRepository<Role> Roles { get; }
    IRepository<TaskExecutor> TaskExecutors { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}