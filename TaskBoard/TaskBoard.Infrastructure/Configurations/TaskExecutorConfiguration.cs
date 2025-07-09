using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoard.Domain.Entities;

namespace TaskBoard.Infrastructure.Configurations;

public class TaskExecutorConfiguration:IEntityTypeConfiguration<TaskExecutor>
{
    public void Configure(EntityTypeBuilder<TaskExecutor> builder)
    {
        builder.HasKey(te => new { te.TaskId, te.UserId });

        builder.HasOne(te => te.Task)
            .WithMany(t => t.Executors)
            .HasForeignKey(te => te.TaskId);
        
        builder.HasOne(te => te.User)
            .WithMany(u=>u.AssignedTasks)
            .HasForeignKey(te => te.UserId);
    }
}