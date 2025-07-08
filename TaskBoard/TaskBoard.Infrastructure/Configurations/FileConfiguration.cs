using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoard.Domain.Entities;
using Task = TaskBoard.Domain.Entities.Task;

namespace TaskBoard.Infrastructure.Configurations;

public class FileConfiguration: IEntityTypeConfiguration<Files>
{
    public void Configure(EntityTypeBuilder<Files> builder)
    {
        builder.HasKey(f=>f.Id);
        
        builder.HasOne(f=>f.Sprint)
            .WithMany(s => s.Files)
            .HasForeignKey(f => f.SprintId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(f => f.Task)
            .WithMany(t => t.Files)
            .HasForeignKey(f => f.TaskId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(f => f.User)
            .WithMany(u => u.Files)
            .HasForeignKey(f => f.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
    }
}