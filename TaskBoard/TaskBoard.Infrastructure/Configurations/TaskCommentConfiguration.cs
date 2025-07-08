using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoard.Domain.Entities;

namespace TaskBoard.Infrastructure.Configurations;

public class TaskCommentConfiguration:IEntityTypeConfiguration<TaskComment>
{
    public void Configure(EntityTypeBuilder<TaskComment> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
        
        builder.HasOne(c => c.Task)
            .WithMany(t=>t.Comments)
            .HasForeignKey(c => c.TaskId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(c => c.Sprint)
            .WithMany(s => s.Comments)
            .HasForeignKey(c => c.SprintId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}