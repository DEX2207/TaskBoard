using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TaskBoard.Domain.Entities;
using TaskBoard.Infrastructure.Configurations;

namespace TaskBoard.Infrastructure;

public class AppDbContext: DbContext
{
    public DbSet<Files> Files { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Sprint> Sprints { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<TaskComment> TaskComments { get; set; }
    public IConfiguration Configuration { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        EnumConfigurations.ConfigureEnums(modelBuilder);
        modelBuilder.ApplyConfiguration(new FileConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new SprintConfiguration());
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.ApplyConfiguration(new TaskCommentConfiguration());
        modelBuilder.ApplyConfiguration(new TaskExecutorConfiguration());
    }
}