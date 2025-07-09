using TaskBoard.Application;
using TaskBoard.Domain.Entities;
using TaskBoard.Domain.Interfaces;
using Task = TaskBoard.Domain.Entities.Task;

namespace TaskBoard.Infrastructure.Repository;

public class UnitOfWork:IUnitOfWork
{
    private readonly AppDbContext _context;

    public IRepository<Project> Projects { get; }
    public IRepository<Sprint> Sprints { get; }
    public IRepository<Task> Tasks { get; }
    public IRepository<User> Users { get; }
    public IRepository<TaskComment> Comments { get; } 
    public IRepository<Files> Files { get; }
    public IRepository<Role> Roles { get; }
    public IRepository<TaskExecutor> TaskExecutors { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Projects = new Repository<Project>(_context);
        Sprints = new Repository<Sprint>(_context);
        Tasks = new Repository<Task>(_context);
        Users = new Repository<User>(_context);
        Comments = new Repository<TaskComment>(_context);
        Files = new Repository<Files>(_context);
        Roles = new Repository<Role>(_context);
        TaskExecutors = new Repository<TaskExecutor>(_context);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}