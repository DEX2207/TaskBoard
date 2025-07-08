using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskBoard.Domain.Entities;

namespace TaskBoard.Infrastructure.Configurations;

public class SprintConfiguration:IEntityTypeConfiguration<Sprint>
{
    public void Configure(EntityTypeBuilder<Sprint> builder)
    {
        builder.HasKey(s => s.Id);
        
        builder.HasOne(s => s.Project)
            .WithMany(p => p.Sprints)
            .HasForeignKey(sprint => sprint.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}