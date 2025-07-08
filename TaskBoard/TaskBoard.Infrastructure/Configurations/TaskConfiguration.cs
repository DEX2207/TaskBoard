using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoard.Domain.Entities;
using Task = TaskBoard.Domain.Entities.Task;


namespace TaskBoard.Infrastructure.Configurations;

public class TaskConfiguration:IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasOne(t => t.Sprint)
            .WithMany(s => s.Tasks)
            .HasForeignKey(t=>t.SprintId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(t => t.Status)
            .HasColumnType("task_status")
            .IsRequired();
    }
}